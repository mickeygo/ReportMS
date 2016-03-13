using System;
using System.Linq;
using System.Linq.Expressions;
using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Specifications;
using MongoDB.Driver.Linq;
using Gear.Infrastructure.Storage;

namespace Gear.Infrastructure.Repository.MongoDB
{
    /// <summary>
    /// Represents the MongoDB repository.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    public class MongoDBRepository<TKey, TAggregateRoot> : Repository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        #region Private Fields

        private readonly IMongoDBRepositoryContext mongoDBRepositoryContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <c>MongoDBRepository[TAggregateRoot]</c> class.
        /// </summary>
        /// <param name="context">The <see cref="IRepositoryContext"/> object for initializing the current repository.</param>
        public MongoDBRepository(IRepositoryContext context) : base(context)
        {
            if (context is IMongoDBRepositoryContext)
                mongoDBRepositoryContext = context as MongoDBRepositoryContext;
            else
                throw new InvalidOperationException("Invalid repository context type.");
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be added to the repository.</param>
        protected override void DoAdd(TAggregateRoot aggregateRoot)
        {
            mongoDBRepositoryContext.RegisterNew(aggregateRoot);
        }

        /// <summary>
        /// Gets the aggregate root instance from repository by a given key.
        /// </summary>
        /// <param name="key">The key of the aggregate root.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected override TAggregateRoot DoGetByKey(TKey key)
        {
            var collection = mongoDBRepositoryContext.GetCollectionForType(typeof (TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().First(p => p.ID.Equals(key));
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>The aggregate roots.</returns>
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof (TAggregateRoot));
            var query = collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.OrderBy(sortPredicate);
                    case SortOrder.Descending:
                        return query.OrderByDescending(sortPredicate);
                }
            }
            return query;
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The number of objects per page.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>The aggregate roots.</returns>
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber,
                    "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize,
                    "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof (TAggregateRoot));
            var query = collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression());
            var skip = (pageNumber - 1)*pageSize;
            var take = pageSize;
            var totalCount = query.Count();
            var totalPages = (totalCount + pageSize - 1)/pageSize;

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedCollectionAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).ToList();
                    return new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber,
                        pagedCollectionAscending);
                case SortOrder.Descending:
                    var pagedCollectionDescending =
                        query.OrderByDescending(sortPredicate).Skip(skip).Take(take).ToList();
                    return new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber,
                        pagedCollectionDescending);
            }
            return null;
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// Finds a single aggregate root from the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof (TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression()).FirstOrDefault();
        }

        /// <summary>
        /// Finds a single aggregate root from the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification);
        }

        /// <summary>
        /// Checkes whether the aggregate root, which matches the given specification, exists in the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>True if the aggregate root exists, otherwise false.</returns>
        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification) != null;
        }

        /// <summary>
        /// Removes the aggregate root from current repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be removed.</param>
        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            mongoDBRepositoryContext.RegisterDeleted(aggregateRoot);
        }

        /// <summary>
        /// Updates the aggregate root in the current repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be updated.</param>
        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            mongoDBRepositoryContext.RegisterModified(aggregateRoot);
        }

        #endregion
    }

    /// <summary>
    /// Represents the MongoDB repository.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    public class MongoDBRepository<TAggregateRoot> : MongoDBRepository<Guid, TAggregateRoot>,
        IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBRepository{TAggregateRoot}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IRepositoryContext" /> object for initializing the current repository.</param>
        public MongoDBRepository(IRepositoryContext context)
            : base(context)
        {
        }
    }
}
