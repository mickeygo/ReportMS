using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using ReportMS.Framework.Specifications;

namespace ReportMS.Framework.Repositories
{
    /// <summary>
    /// 表示仓储的接口
    /// </summary>
    /// <typeparam name="TKey">聚合根键值的类型</typeparam>
    /// <typeparam name="TAggregateRoot">领域聚合根类型</typeparam>
    public interface IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// 获取一个已被附加的仓储上下文实例
        /// </summary>
        IRepositoryContext Context { get; }

        /// <summary>
        /// 添加一个聚合根对象到仓储中
        /// </summary>
        /// <param name="aggregateRoot">被添加到仓储的聚合根</param>
        void Add(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 获取一个聚合根对象
        /// </summary>
        /// <param name="key">聚合根的键值</param>
        /// <returns>聚合根对象</returns>
        TAggregateRoot GetByKey(TKey key);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll();

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            Storage.SortOrder sortOrder);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>聚合根集合</returns>
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            Storage.SortOrder sortOrder, int pageNumber, int pageSize);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>聚合根集合</returns>
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber,
            int pageSize);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            Storage.SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            Storage.SortOrder sortOrder, int pageNumber, int pageSize,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber,
            int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根</returns>
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根</returns>
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 检查是否在仓储中存在满足规约的聚合根
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>True 表示存在，false 表示不存在</returns>
        bool Exist(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 更新仓储中的聚合根
        /// </summary>
        /// <param name="aggregateRoot">要更新的聚合根</param>
        void Update(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 从存储中移除聚合根
        /// </summary>
        /// <param name="aggregateRoot">要移除的聚合根</param>
        void Remove(TAggregateRoot aggregateRoot);
    }

    /// <summary>
    /// 表示仓储的接口
    /// </summary>
    /// <typeparam name="TAggregateRoot">领域聚合根类型</typeparam>
    public interface IRepository<TAggregateRoot> : IRepository<Guid, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {

    }
}


