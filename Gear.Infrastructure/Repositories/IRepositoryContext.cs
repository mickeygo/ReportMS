using System;

namespace Gear.Infrastructure.Repositories
{
    /// <summary>
    /// 表示实现接口的类是仓储上下文。
    /// 用于 注册新的、要更改的 或 要删除的 数据，并作用于工作单元
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取聚合仓储上下文的唯一标识
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 注册一个新的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要添加的对象</param>
        void RegisterNew<TAggregateRoot>(TAggregateRoot obj) 
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 注册一个要修改的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要修改的对象</param>
        void RegisterModified<TAggregateRoot>(TAggregateRoot obj) 
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要删除的对象</param>
        void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj) 
            where TAggregateRoot : class, IAggregateRoot;
    }
}
