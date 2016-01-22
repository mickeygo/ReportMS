﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework.Extensions;
using Gear.Infrastructure.Specifications;
using Gear.Infrastructure.Storage;

namespace Gear.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// 表示为 Microsoft EntityFramework 仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public class EntityFrameworkRepository<TAggregateRoot> : Repository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        #region Private Fields

        private readonly IEntityFrameworkRepositoryContext efContext;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>EntityFrameworkRepository</c>实例
        /// </summary>
        /// <param name="context">仓储上下文</param>
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            var repositoryContext = context as IEntityFrameworkRepositoryContext;
            if (repositoryContext != null)
                this.efContext = repositoryContext;
        }

        #endregion

        #region Private Methods

        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("lambda");

            MemberExpression memberExpr = null;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpr = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TAggregateRoot, dynamic>> eagerLoadingProperty)
        {
            var memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            return memberExpressionStr.Replace(parameterName + ".", "");
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取仓储实例<see cref="IEntityFrameworkRepositoryContext"/>.
        /// </summary>
        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return this.efContext; }
        }

        #endregion

        #region Protected Methods
        
        /// <summary>
        /// 从仓储中获取聚合实例
        /// </summary>
        /// <param name="key">聚合根的标识符</param>
        /// <returns>聚合根</returns>
        protected override TAggregateRoot DoGetByKey(Guid key)
        {
            return this.efContext.Context.Set<TAggregateRoot>().First(p => p.ID == key);
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <returns>聚合根集合</returns>
        protected override IQueryable<TAggregateRoot> DoFindAll(
            ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            SortOrder sortOrder)
        {
            var query = this.efContext.Context.Set<TAggregateRoot>().Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate);
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate);
                }
            }
            return query;
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
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var query = this.efContext.Context.Set<TAggregateRoot>().Where(specification.GetExpression());
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = query.SortBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                    if (pagedGroupAscending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = query.SortByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                    if (pagedGroupDescending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
            }

            return null;
        }

        /// <summary>
        /// 从仓储中查找所有的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="sortPredicate">用于排序的排序断言</param>
        /// <param name="sortOrder"><see cref="SortOrder"/>枚举类型，指定如何排序</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根集合</returns>
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = this.efContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Any())
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.SortBy(sortPredicate);
                    case SortOrder.Descending:
                        return queryable.SortByDescending(sortPredicate);
                }
            }
            return queryable;
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
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var dbset = this.efContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Any())
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = queryable.SortBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                    if (pagedGroupAscending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = queryable.SortByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                    if (pagedGroupDescending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
            }

            return null;
        }

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>聚合根</returns>
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            return this.efContext.Context.Set<TAggregateRoot>().Where(specification.IsSatisfiedBy).FirstOrDefault();
        }

        /// <summary>
        /// 从仓储中查找指定的聚合根对象
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <param name="eagerLoadingProperties">聚合根实例需要加载的属性</param>
        /// <returns>聚合根</returns>
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = this.efContext.Context.Set<TAggregateRoot>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Any())
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return dbquery.Where(specification.GetExpression()).FirstOrDefault();
            }

            return dbset.Where(specification.GetExpression()).FirstOrDefault();
        }

        /// <summary>
        /// 检查是否在仓储中存在满足规约的聚合根
        /// </summary>
        /// <param name="specification">聚合根要匹配的规约</param>
        /// <returns>True 表示存在，false 表示不存在</returns>
        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            return this.efContext.Context.Set<TAggregateRoot>().Any(specification.IsSatisfiedBy);
        }

        /// <summary>
        /// 向仓储中添加聚合
        /// </summary>
        /// <param name="aggregateRoot">要添加的聚合对象</param>
        protected override void DoAdd(TAggregateRoot aggregateRoot)
        {
            this.efContext.RegisterNew(aggregateRoot);
        }

        /// <summary>
        /// 更新仓储中的聚合根
        /// </summary>
        /// <param name="aggregateRoot">要移除的聚合根要更新的聚合根</param>
        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            this.efContext.RegisterModified(aggregateRoot);
        }

        /// <summary>
        /// 从存储中移除聚合根
        /// </summary>
        /// <param name="aggregateRoot">要移除的聚合根</param>
        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            this.efContext.RegisterDeleted(aggregateRoot);
        }

        #endregion
    }
}