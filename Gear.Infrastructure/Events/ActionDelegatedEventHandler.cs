using System;

namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 表示代理给定的领域事件处理委托的领域事件处理器
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    internal sealed class ActionDelegatedEventHandler<TEvent> : IEventHandler<TEvent> 
        where TEvent : class, IEvent
    {
        #region Private Fields
        private readonly Action<TEvent> eventHandlerDelegate;
        #endregion

        /// <summary>
        /// 初始化一个新的<c>ActionDelegatedEventHandler</c>实例
        /// </summary>
        /// <param name="eventHandlerDelegate">用于当前领域事件处理器所代理的事件处理委托</param>
        public ActionDelegatedEventHandler(Action<TEvent> eventHandlerDelegate)
        {
            this.eventHandlerDelegate = eventHandlerDelegate;
        }

        #region IEventHandler<TEvent> Members

        /// <summary>
        /// 处理给定的事件
        /// </summary>
        /// <param name="e">要处理的事件</param>
        public void Handle(TEvent e)
        {
            this.eventHandlerDelegate(e);
        }

        #endregion

        /// <summary>
        /// 重新 Equals 方法，获取一个<see cref="Boolean"/>值，该值表示当前对象是否与给定的类型相同的另一对象相等。
        /// </summary>
        /// <param name="other">需要比较的与当前对象类型相同的另一对象。</param>
        /// <returns>如果两者相等，则返回true，否则返回false。</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;
            var otherDelegate = other as ActionDelegatedEventHandler<TEvent>;
            if (otherDelegate == null)
                return false;
            // 使用Delegate.Equals方法判定两个委托是否是代理的同一方法。
            return Delegate.Equals(this.eventHandlerDelegate, otherDelegate.eventHandlerDelegate);
        }

        public override int GetHashCode()
        {
            return this.eventHandlerDelegate.GetHashCode();
        }
    }
}
