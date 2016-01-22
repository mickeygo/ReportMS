using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReportMS.Framework.Events;
using ReportMS.Framework.Generators;
using ReportMS.Framework.Snapshots;

namespace ReportMS.Framework
{
    /// <summary>
    /// 支持事件源(Event Source)机制的聚合根
    /// </summary>
    public abstract class SourcedAggregateRoot : ISourcedAggregateRoot
    {
        #region Private Fields

        private long version;
        private long eventVersion;
        private readonly List<IDomainEvent> uncommittedEvents = new List<IDomainEvent>();
        private readonly ConcurrentDictionary<Type, List<object>> domainEventHandlers = new ConcurrentDictionary<Type, List<object>>();
        
        #endregion

        #region Internal Constants
        
        /// <summary>
        /// 更新聚合根版本并清空未提交的事件的方法名称
        /// </summary>
        internal const string UpdateVersionAndClearUncommittedEventsMethodName = @"UpdateVersionAndClearUncommittedEvents";

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>SourcedAggregateRoot</c>实例
        /// </summary>
        protected SourcedAggregateRoot()
            : this((Guid)IdentityGenerator.Instance.Generate())
        {
            
        }

        /// <summary>
        /// 初始化<c>SourcedAggregateRoot</c>实例
        /// </summary>
        /// <param name="aggregateId">聚合根的唯一标识</param>
        protected SourcedAggregateRoot(Guid aggregateId)
        {
            this.ID = aggregateId;
            this.version = Constants.ApplicationRuntime.DefaultVersion;
            this.eventVersion = this.version;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取指定的领域事件的所有有效的领域事件处理者
        /// </summary>
        /// <param name="domainEvent">要检索的领域事件</param>
        /// <returns>领域事件集合</returns>
        private IEnumerable<object> GetDomainEventHandlers(IDomainEvent domainEvent)
        {
            var eventType = domainEvent.GetType();
            if (this.domainEventHandlers.ContainsKey(eventType))
                return this.domainEventHandlers[eventType];

            // firstly create and add all the handler methods defined within the aggregation root.
            var allMethods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var handlerMethods = from method in allMethods
                let returnType = method.ReturnType
                let @params = method.GetParameters()
                let handlerAttributes = method.GetCustomAttributes(typeof(HandlesAttribute), false)
                where returnType == typeof(void) &&
                      @params != null &&
                      @params.Any() &&
                      @params[0].ParameterType == eventType &&
                      handlerAttributes != null &&
                      ((HandlesAttribute)handlerAttributes[0]).DomainEventType == eventType
                select new { MethodInfo = method };

            var handlers = (from handlerMethod in handlerMethods 
                            let inlineDomainEventHandlerType = typeof (InlineDomainEventHandler<>).MakeGenericType(eventType) 
                            select Activator.CreateInstance(inlineDomainEventHandlerType, this, handlerMethod.MethodInfo)).ToList();

            // then read all the registered handlers.
            this.domainEventHandlers.TryAdd(eventType, handlers);
            return handlers;
        }

        /// <summary>
        /// 处理聚合根上的指定的领域域事件
        /// </summary>
        /// <typeparam name="TEvent">领域事件类型</typeparam>
        /// <param name="event">领域事件</param>
        private void HandleEvent<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            var handlers = this.GetDomainEventHandlers(@event);
            foreach (var handler in handlers)
            {
                var handleMethod = handler.GetType().GetMethod("Handle", BindingFlags.Public | BindingFlags.Instance);
                if (handleMethod != null)
                    handleMethod.Invoke(handler, new object[] { @event });
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 发起一个领域事件
        /// </summary>
        /// <typeparam name="TEvent">领域事件的类型</typeparam>
        /// <param name="event">领域事件</param>
        protected virtual void RaiseEvent<TEvent>(TEvent @event) 
            where TEvent : IDomainEvent
        {
            @event.ID = (Guid)IdentityGenerator.Instance.Generate();
            @event.Version = ++eventVersion;
            @event.Source = this;
            @event.AssemblyQualifiedEventType = typeof(TEvent).AssemblyQualifiedName;
            @event.Timestamp = DateTime.UtcNow;
            this.HandleEvent<TEvent>(@event);
            this.uncommittedEvents.Add(@event);
        }

        /// <summary>
        /// 在派生类中重写，从给定的快照实例中建立聚合
        /// </summary>
        /// <param name="snapshot"></param>
        protected abstract void DoBuildFromSnapshot(ISnapshot snapshot);

        /// <summary>
        /// 在派生类中重写，创建基于当前聚合的快照实例
        /// </summary>
        /// <returns></returns>
        protected abstract ISnapshot DoCreateSnapshot();

        #endregion

        #region ISourcedAggregateRoot Members

        /// <summary>
        /// 从历史事件中建立聚合
        /// </summary>
        /// <param name="historicalEvents">聚合建立的历史事件</param>
        public virtual void BuildFromHistory(IEnumerable<IDomainEvent> historicalEvents)
        {
            if (this.uncommittedEvents.Any())
                this.uncommittedEvents.Clear();

            foreach (var de in historicalEvents)
                this.HandleEvent<IDomainEvent>(de);

            this.version = historicalEvents.Last().Version;
            this.eventVersion = this.version;
        }

        /// <summary>
        /// 获取没有提交的事件
        /// </summary>
        public virtual IEnumerable<IDomainEvent> UncommittedEvents
        {
            get { return this.uncommittedEvents; }
        }

        /// <summary>
        /// 获取聚合版本
        /// </summary>
        public virtual long Version
        {
            get { return this.version + this.uncommittedEvents.Count; }
        }

        #endregion

        #region IEntity<Guid> Members

        /// <summary>
        /// 获取或设置聚合根 ID
        /// </summary>
        public virtual Guid ID { get; set; }

        #endregion

        #region ISnapshotOriginator Members

        /// <summary>
        /// 从指定的快照中生成发起者
        /// </summary>
        /// <param name="snapshot">发起者创建的快照</param>
        public void BuildFromSnapshot(ISnapshot snapshot)
        {
            this.version = snapshot.Version;
            this.ID = snapshot.AggregateRootID;
            this.DoBuildFromSnapshot(snapshot);
            this.uncommittedEvents.Clear();
        }

        /// <summary>
        /// 创建快照
        /// </summary>
        /// <returns>基于当前发起者创建的快照</returns>
        public ISnapshot CreateSnapshot()
        {
            var snapshot = this.DoCreateSnapshot();
            snapshot.Version = this.Version;
            snapshot.Timestamp = DateTime.UtcNow;
            snapshot.AggregateRootID = this.ID;

            return snapshot;
        }

        #endregion
    }
}
