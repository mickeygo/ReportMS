using Gear.Infrastructure.Storage;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gear.Infrastructure.Repository.MongoDB
{
    /// <summary>
    /// Represents the helper (method extender) for the sorting lambda expressions.
    /// </summary>
    internal static class SortExpressionHelper
    {
        #region Private Static Methods

        private static IOrderedQueryable<TAggregateRoot> InvokeOrderBy<TKey, TAggregateRoot>(
            IQueryable<TAggregateRoot> query, Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            SortOrder sortOrder)
            where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            var param = sortPredicate.Parameters[0];
            string propertyName = null;
            Type propertyType = null;
            Expression bodyExpression = null;
            if (sortPredicate.Body is UnaryExpression)
            {
                var unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else
                throw new ArgumentException(
                    "The body of the sort predicate expression should be either UnaryExpression or MemberExpression.", "sortPredicate");

            var memberExpression = (MemberExpression) bodyExpression;
            propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                propertyType = propertyInfo.PropertyType;
            }
            else
                throw new InvalidOperationException(
                    "Cannot evaluate the type of property since the member expression represented by the sort predicate expression does not contain a PropertyInfo object.");

            var funcType = typeof (Func<,>).MakeGenericType(typeof (TAggregateRoot), propertyType);
            var convertedExpression = Expression.Lambda(funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType), param);

            var sortingMethods = typeof (Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod = sortingMethods.First(sm => sm.Name == sortingMethodName &&
                                                           sm.GetParameters().Length == 2);
            return (IOrderedQueryable<TAggregateRoot>) sortingMethod
                .MakeGenericMethod(typeof (TAggregateRoot), propertyType)
                .Invoke(null, new object[] {query, convertedExpression});
        }

        private static string GetSortingMethodName(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return "OrderBy";
                case SortOrder.Descending:
                    return "OrderByDescending";
                default:
                    throw new ArgumentException("Sort Order must be specified as either Ascending or Descending.",
                        "sortOrder");
            }
        }

        #endregion

        #region Internal Method Extensions

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a lambda expression.
        /// </summary>
        /// <typeparam name="TKey">The type of the key of the aggregate root.</typeparam>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="query">A sequence of values to order.</param>
        /// <param name="sortPredicate">The lambda expression which indicates the property for sorting.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the lambda expression.</returns>
        internal static IOrderedQueryable<TAggregateRoot> OrderBy<TKey, TAggregateRoot>(
            this IQueryable<TAggregateRoot> query, Expression<Func<TAggregateRoot, dynamic>> sortPredicate)
            where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            return InvokeOrderBy<TKey, TAggregateRoot>(query, sortPredicate, SortOrder.Ascending);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a lambda expression.
        /// </summary>
        /// <typeparam name="TKey">The type of the key of the aggregate root.</typeparam>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="query">A sequence of values to order.</param>
        /// <param name="sortPredicate">The lambda expression which indicates the property for sorting.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the lambda expression.</returns>
        internal static IOrderedQueryable<TAggregateRoot> OrderByDescending<TKey, TAggregateRoot>(
            this IQueryable<TAggregateRoot> query, Expression<Func<TAggregateRoot, dynamic>> sortPredicate)
            where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            return InvokeOrderBy<TKey, TAggregateRoot>(query, sortPredicate, SortOrder.Descending);
        }

        #endregion
    }
}
