using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Gear.Infrastructure.Specifications;

namespace Gear.Infrastructure.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TKey">聚合根的标识符</typeparam>
    /// <typeparam name="TAggregateRoot">聚合根对象</typeparam>
    public abstract class Repository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        #region Private Fields

        private readonly IRepositoryContext context;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>Repository</c>实例
        /// </summary>
        /// <param name="context">于仓储的仓储上下文</param>
        protected Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 向仓储中添加聚合
        /// </summary>
        /// <param name="aggregateRoot">要添加的聚合对象</param>
        protected abstract void DoAdd(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 从仓储中获取聚合实例
        /// </summary>
        /// <param name="key">聚合根的标识符</param>
        /// <returns>聚合根</returns>
        protected abstract TAggregateRoot DoGetByKey(TKey key);

        /// <summary>
        /// 查找仓储中所有的聚合根
        /// </summary>
        /// <returns>聚合根</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll()
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, Storage.SortOrder.Unspecified);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>聚合根集合</returns>
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根集合</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification)
        {
            return DoFindAll(specification, null, Storage.SortOrder.Unspecified);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>聚合根集合</returns>
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize);

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, Storage.SortOrder.Unspecified, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, Storage.SortOrder.Unspecified, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

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
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根</returns>
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根</returns>
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 检查是否在仓储中存在满足规约的聚合根
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>True 表示存在，false 表示不存在</returns>
        protected abstract bool DoExists(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 从存储中移除聚合根
        /// </summary>
        /// <param name="aggregateRoot">要移除的聚合根</param>
        protected abstract void DoUpdate(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 更新仓储中的聚合根
        /// </summary>
        /// <param name="aggregateRoot">要更新的聚合根</param>
        protected abstract void DoRemove(TAggregateRoot aggregateRoot);

        #endregion

        #region IRepository<TKey,TAggregateRoot> Members

        /// <summary>
        /// 获取一个已被附加的仓储上下文实例
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        /// <summary>
        /// 添加一个聚合根对象到仓储中
        /// </summary>
        /// <param name="aggregateRoot">被添加到仓储的聚合根</param>
        public void Add(TAggregateRoot aggregateRoot)
        {
            this.DoAdd(aggregateRoot);
        }

        /// <summary>
        /// 获取一个聚合根对象
        /// </summary>
        /// <param name="key">聚合根的键值</param>
        /// <returns>聚合根对象</returns>
        public TAggregateRoot GetByKey(TKey key)
        {
            return this.DoGetByKey(key);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll()
        {
            return this.DoFindAll();
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder)
        {
            return this.DoFindAll(sortPredicate, sortOrder);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>聚合根集合</returns>
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFindAll(specification);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>聚合根集合</returns>
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }

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
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, Storage.SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根</returns>
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification);
        }

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根</returns>
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification, eagerLoadingProperties);
        }

        /// <summary>
        /// 检查是否在仓储中存在满足规约的聚合根
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>True 表示存在，false 表示不存在</returns>
        public bool Exist(ISpecification<TAggregateRoot> specification)
        {
            return this.DoExists(specification);
        }

        /// <summary>
        /// 更新仓储中的聚合根
        /// </summary>
        /// <param name="aggregateRoot">要更新的聚合根</param>
        public void Update(TAggregateRoot aggregateRoot)
        {
            this.DoUpdate(aggregateRoot);
        }

        /// <summary>
        /// 移除仓储中的聚合根
        /// </summary>
        /// <param name="aggregateRoot">要移除的聚合根</param>
        public void Remove(TAggregateRoot aggregateRoot)
        {
            this.DoRemove(aggregateRoot);
        }

        #endregion

    }

    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根</typeparam>
    public abstract class Repository<TAggregateRoot> : Repository<Guid, TAggregateRoot>, IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 初始化<see cref="Repository{TAggregateRoot}"/> 实例.
        /// </summary>
        /// <param name="context">于仓储的仓储上下文</param>
        protected Repository(IRepositoryContext context)
            : base(context)
        { }
    }
}
