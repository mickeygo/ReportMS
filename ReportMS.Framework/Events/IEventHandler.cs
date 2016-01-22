namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示实现类是事件处理者
    /// </summary>
    /// <typeparam name="TEvent">要被处理的事件的类型</typeparam>
    public interface IEventHandler<in TEvent> : IHandler<TEvent>
        where TEvent : IEvent
    {
    }
}
