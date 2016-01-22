using System;
using System.Linq.Expressions;

namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 组合规约，指定的对象都满足给定的规约
    /// </summary>
    /// <typeparam name="T">应用规约的对象类型</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <param name="left">第一个规约</param>
        /// <param name="right">第二个规约</param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
        { }

        /// <summary>
        /// 获取当前规约的 LINQ 表达式
        /// </summary>
        /// <returns>LINQ 表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return Left.GetExpression().And(Right.GetExpression());
        }
    }
}
