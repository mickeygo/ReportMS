using System;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 事件处理程序处理的委托方法
    /// </summary>
    /// <typeparam name="TEvent">要处理的事件类型</typeparam>
    public sealed class ActionDelegatedEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : class, IEvent
    {
        #region Private Fields
        private readonly Action<TEvent> action;
        #endregion

        #region Ctor
        
        /// <summary>
        /// 初始化<c>ActionDelegatedEventHandler</c>实例
        /// </summary>
        /// <param name="action">处理事件的<see cref="Action{T}"/> 实例</param>
        public ActionDelegatedEventHandler(Action<TEvent> action)
        {
            this.action = action;
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as ActionDelegatedEventHandler<TEvent>;
            return other != null && Delegate.Equals(this.action, other.action);
        }
 
        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.action.GetHashCode());
        }

        #endregion

        #region IHandler<TEvent> Members

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event">要处理的事件</param>
        public void Handle(TEvent @event)
        {
            this.action(@event);
        }

        #endregion
    }
}
