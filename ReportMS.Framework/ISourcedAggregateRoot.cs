using System.Collections.Generic;
using ReportMS.Framework.Events;
using ReportMS.Framework.Snapshots;

namespace ReportMS.Framework
{
    /// <summary>
    /// 表示实现类是支持事件源(Event Source)机制的聚合根
    /// </summary>
    public interface ISourcedAggregateRoot : IAggregateRoot, ISnapshotOriginator
    {
        /// <summary>
        /// 从历史事件中建立聚合
        /// </summary>
        /// <param name="historicalEvents">聚合建立的历史事件</param>
        void BuildFromHistory(IEnumerable<IDomainEvent> historicalEvents);

        /// <summary>
        /// 获取所有没有提交的事件
        /// </summary>
        IEnumerable<IDomainEvent> UncommittedEvents { get; }

        /// <summary>
        /// 获取或设置聚合的版本
        /// </summary>
        long Version { get; }
    }
}
