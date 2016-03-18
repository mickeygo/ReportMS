using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gear.Infrastructure.Repository.EntityFramework.Extensions
{
    /// <summary>
    /// 表示基于Entity Framework的排序扩展类型。该扩展解决了在Entity Framework上针对某些
    /// 原始数据类型执行排序操作时，出现Expression of type A cannot be used for return type B
    /// 错误的问题。
    /// </summary>
    /// <remarks>有关该功能扩展的更多信息，请参考：http://www.cnblogs.com/daxnet/archive/2012/07/23/2605695.html。</remarks>
    internal static class SortByExtension
    {
        #region Internal Methods

        internal static IOrderedQueryable<TAggregateRoot> SortBy<TKey, TAggregateRoot>(
            this IQueryable<TAggregateRoot> query,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate) where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            return InvokeSortBy<TKey, TAggregateRoot>(query, sortPredicate, SortOrder.Ascending);
        }

        internal static IOrderedQueryable<TAggregateRoot> SortByDescending<TKey, TAggregateRoot>(
            this IQueryable<TAggregateRoot> query,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate) where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            return InvokeSortBy<TKey, TAggregateRoot>(query, sortPredicate, SortOrder.Descending);
        }

        #endregion

        #region Private Methods

        private static IOrderedQueryable<TAggregateRoot> InvokeSortBy<TKey, TAggregateRoot>(
            IQueryable<TAggregateRoot> query,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            SortOrder sortOrder) where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            var param = sortPredicate.Parameters[0];
            Type propertyType;
            Expression bodyExpression;
            if (sortPredicate.Body is UnaryExpression)
            {
                var unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else throw new ArgumentException(@"The body of the sort predicate expression should be 
                either UnaryExpression or MemberExpression.", "sortPredicate");

            var memberExpression = (MemberExpression) bodyExpression;
            var propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                propertyType = propertyInfo.PropertyType;
            }
            else throw new InvalidOperationException(@"Cannot evaluate the type of property since the member expression 
                represented by the sort predicate expression does not contain a PropertyInfo object.");

            var funcType = typeof (Func<,>).MakeGenericType(typeof (TAggregateRoot), propertyType);
            var convertedExpression = Expression.Lambda(
                funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType),
                param);

            var sortingMethods = typeof (Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod =
                sortingMethods.First(sm => sm.Name == sortingMethodName && sm.GetParameters().Length == 2);
            return
                (IOrderedQueryable<TAggregateRoot>)
                    sortingMethod.MakeGenericMethod(typeof (TAggregateRoot), propertyType)
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
    }
}
