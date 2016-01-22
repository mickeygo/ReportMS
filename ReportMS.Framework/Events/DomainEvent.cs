using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 领域事件
    /// </summary>
    /// <remarks>领域事件是由域模型引发的事件</remarks>
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        #region Ctor

        protected DomainEvent()
        {
        }

        /// <summary>
        /// 初始化<c>DomainEvent</c>实例
        /// </summary>
        /// <param name="source">领域事件的来源实体</param>
        protected DomainEvent(IEntity source)
        {
            this.Source = source;
        }

        #endregion

        #region IDomainEvent Members

        /// <summary>
        /// 获取或设置领域事件的来源实体
        /// </summary>
        /// <remarks>实体数据源不会被序列化</remarks>
        [SoapIgnore, XmlIgnore, IgnoreDataMember]
        public IEntity Source { get; set; }

        /// <summary>
        /// 获取或设置领域事件的版本
        /// </summary>
        public virtual long Version { get; set; }

        #endregion

        #region Public Methods

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.Source.GetHashCode(), this.ID.GetHashCode(), this.Timestamp.GetHashCode(),
                this.Version.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as DomainEvent;
            if (other == null)
                return false;
            return this.ID == other.ID;
        }

        #endregion

        #region IEvent Members

        /// <summary>
        /// 获取或设置事件产生的时间戳
        /// </summary>
        public virtual DateTime Timestamp { get; set; }

        /// <summary>
        /// 获取或设置事件的程序集限定名
        /// </summary>
        public virtual string AssemblyQualifiedEventType { get; set; }

        #endregion

        #region IEntity<Guid> Members

        /// <summary>
        /// 获取或设置当前领域事件的身份标识
        /// </summary>
        public virtual Guid ID { get; set; }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <param name="domainEvent"></param>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            var handlers = ServiceLocator.Instance.ResolveAll<IDomainEventHandler<TDomainEvent>>();

            foreach (var handler in handlers)
            {
                if (handler.GetType().IsDefined(typeof (ParallelExecutionAttribute), false))
                    Task.Factory.StartNew(() => handler.Handle(domainEvent));
                else
                    handler.Handle(domainEvent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <param name="domainEvent"></param>
        /// <param name="callback"></param>
        /// <param name="timeout"></param>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent, Action<TDomainEvent, bool, Exception> callback, TimeSpan? timeout)
            where TDomainEvent : IDomainEvent
        {
            var handlers = ServiceLocator.Instance.ResolveAll<IDomainEventHandler<TDomainEvent>>();

            if (handlers != null && handlers.Any())
            {
                var tasks = new List<Task>();
                try
                {
                    foreach (var handler in handlers)
                    {
                        if (handler.GetType().IsDefined(typeof (ParallelExecutionAttribute), false))
                            tasks.Add(Task.Factory.StartNew(() => handler.Handle(domainEvent)));
                        else
                            handler.Handle(domainEvent);
                    }

                    if (tasks.Count > 0)
                    {
                        if (timeout == null)
                            Task.WaitAll(tasks.ToArray());
                        else
                            Task.WaitAll(tasks.ToArray(), timeout.Value);
                    }

                    callback(domainEvent, true, null);
                }
                catch (Exception ex)
                {
                    callback(domainEvent, false, ex);
                }
            }
            else
                callback(domainEvent, false, null);
        }

        #endregion
    }
}
