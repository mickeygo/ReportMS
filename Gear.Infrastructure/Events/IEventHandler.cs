namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 表示实现该接口的类型为事件处理器。
    /// </summary>
    /// <typeparam name="TEvent">事件的类型</typeparam>
    public interface IEventHandler<in TEvent>
        where TEvent : class, IEvent
    {
        /// <summary>
        /// 处理给定的事件
        /// </summary>
        /// <param name="e">要处理的事件</param>
        void Handle(TEvent e);
    }
}
