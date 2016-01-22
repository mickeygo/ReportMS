using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 领域事件基类
    /// </summary>
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        #region Private Fields

        private readonly IEntity source;
        private readonly Guid id = Guid.NewGuid();
        private readonly DateTime timeStamp = DateTime.UtcNow;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>DomainEvent</c>实例
        /// </summary>
        protected DomainEvent()
        { }

        /// <summary>
        /// 初始化一个新的<c>DomainEvent</c>实例
        /// </summary>
        /// <param name="source">领域事件的事件源</param>
        protected DomainEvent(IEntity source)
        {
            this.source = source;
        }

        #endregion

        #region IDomainEvent Members

        /// <summary>
        /// 获取领域事件的事件源
        /// </summary>
        public IEntity Source
        {
            get { return this.source; }
        }

        #endregion

        #region IEvent Members

        /// <summary>
        /// 获取领域事件的唯一标识符
        /// </summary>
        public Guid ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// 获取领域事件产生的时间戳
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <typeparam name="TDomainEvent">领域事件类型</typeparam>
        /// <param name="domainEvent">要发布的领域事件对象</param>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : class, IDomainEvent
        {
            var handlers = ServiceLocator.Instance.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                if (handler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                {
                    var handler1 = handler;  // 消除 C# 5.0 以下（不包括 5.0）会产生闭包的情形
                    Task.Factory.StartNew(() => handler1.Handle(domainEvent));
                }
                else
                    handler.Handle(domainEvent);
            }
        }

        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <typeparam name="TDomainEvent">领域事件类型</typeparam>
        /// <param name="domainEvent">要发布的领域事件对象</param>
        /// <param name="callback">回调函数，T2 为 true 表示发布成功； T2 为 false 表示若发布出现错误或无要处理的领域事件，此时T3 为异常信息或null</param>
        /// <param name="timeout">指定的超时时间段</param>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent, Action<TDomainEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TDomainEvent : class, IDomainEvent
        {
            var handlers = ServiceLocator.Instance.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            if (handlers != null && handlers.Any())
            {
                var tasks = new List<Task>();
                try
                {
                    foreach (var handler in handlers)
                    {
                        if (handler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                        {
                            var handler1 = handler; // 消除 C# 5.0 以下（不包括 5.0）会产生闭包的情形
                            tasks.Add(Task.Factory.StartNew(() => handler1.Handle(domainEvent)));
                        }
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
