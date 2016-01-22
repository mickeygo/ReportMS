using System;
using System.Linq.Expressions;

namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 表示一个规约，找给定的任何语境中都被满足
    /// </summary>
    /// <typeparam name="T">应用规约的对象类型</typeparam>
    public class AnySpecification<T> : Specification<T>
    {
        /// <summary>
        /// 获取当前规约的 LINQ 表达式
        /// </summary>
        /// <returns>LINQ 表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return o => true;
        }
    }
}
