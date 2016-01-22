namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示实现类是领域事件
    /// </summary>
    /// <remarks>领域事件是由域模型引发的事件</remarks>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// 获取或设置领域事件的来源实体
        /// </summary>
        IEntity Source { get; set; } 

        /// <summary>
        /// 获取或设置领域事件的版本
        /// </summary>
        long Version { get; set; }
    }
}
