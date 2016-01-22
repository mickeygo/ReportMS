using System;
using System.Linq.Expressions;

namespace ReportMS.Framework.Specifications
{
    /// <summary>
    /// 表示一个组合规约，给定的对象满足第一个规约满足，但是不满足第二个规约
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        /// <summary>
        /// 创建一个新的对象
        /// </summary>
        /// <param name="left">第一个规约</param>
        /// <param name="right">第二个规约</param>
        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
        { }

        /// <summary>
        /// 获取当前规约的 LINQ 表达式
        /// </summary>
        /// <returns>LINQ 表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            var bodyNot = Expression.Not(this.Right.GetExpression().Body);
            var bodyNotExpression = Expression.Lambda<Func<T, bool>>(bodyNot, Right.GetExpression().Parameters);

            return this.Left.GetExpression().And(bodyNotExpression);
        }
    }
}
