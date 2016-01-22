using System;
using System.Collections.Generic;

namespace ReportMS.Framework.Events.Storage
{
    /// <summary>
    /// 表示实现类是领域事件存储
    /// </summary>
    public interface IDomainEventStorage : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 保存指定的领域事件到事件存储中
        /// </summary>
        /// <param name="domainEvent">要保存的领域事件</param>
        void SaveEvent(IDomainEvent domainEvent);

        /// <summary>
        /// 从仓储中加载指定聚合根的领域事件
        /// </summary>
        /// <param name="aggregateRootType">聚合根类型</param>
        /// <param name="id">聚合根标识</param>
        /// <returns>领域事件集</returns>
        IEnumerable<IDomainEvent> LoadEvents(Type aggregateRootType, Guid id);

        /// <summary>
        /// 从仓储中加载指定聚合根的领域事件
        /// </summary>
        /// <param name="aggregateRootType">聚合根类型</param>
        /// <param name="id">聚合根标识</param>
        /// <param name="version">版本号</param>
        /// <returns>领域事件集</returns>
        IEnumerable<IDomainEvent> LoadEvents(Type aggregateRootType, Guid id, long version);
    }
}
