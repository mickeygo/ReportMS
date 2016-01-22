using System;

namespace ReportMS.Framework.Repositories
{
    /// <summary>
    /// 表示实现类是领域仓储
    /// </summary>
    public interface IDomainRepository : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取指定的聚合根实例
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="aggregateRootId">要获取的聚合根 ID</param>
        /// <returns></returns>
        TAggregateRoot Get<TAggregateRoot>(Guid aggregateRootId)
           where TAggregateRoot : class, ISourcedAggregateRoot;

        /// <summary>
        /// 保存指定聚合根中的聚合对象
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="aggregateRoot">聚合根对象</param>
        void Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : class, ISourcedAggregateRoot;
    }
}
