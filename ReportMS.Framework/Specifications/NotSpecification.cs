using System;
using System.Linq.Expressions;

namespace ReportMS.Framework.Specifications
{
    /// <summary>
    /// 表示与给定规约的语义相反的规约
    /// </summary>
    /// <typeparam name="T">应用规约的对象类型</typeparam>
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _specification;

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="specification">要解析的规约</param>
        public NotSpecification(ISpecification<T> specification)
        {
            this._specification = specification;
        }

        /// <summary>
        /// 获取当前规约的 LINQ 表达式
        /// </summary>
        /// <returns>LINQ 表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.Not(this._specification.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, this._specification.GetExpression().Parameters);
        }
    }
}
