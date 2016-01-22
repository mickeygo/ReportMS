namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示实现类为领域事件处理者
    /// </summary>
    /// <typeparam name="TDomainEvent">被当前处理者处理的领域事件类型</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : IEventHandler<TDomainEvent>
       where TDomainEvent : IDomainEvent
    {

    }
}
