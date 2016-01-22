using System;
using System.Linq.Expressions;

namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 表示一个呈现一致性的 LINQ 表达式规约
    /// </summary>
    /// <typeparam name="T">应用规约的对象类型</typeparam>
    internal sealed class ExpressionSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _expression;

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this._expression = expression;
        }

        /// <summary>
        /// 获取当前规约的 LINQ 表达式
        /// </summary>
        /// <returns>LINQ 表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return this._expression;
        }
    }
}
