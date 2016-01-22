namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 表示实现接口类为领域事件
    /// </summary>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// 获取领域事件的事件源
        /// </summary>
        IEntity Source { get; }
    }
}
