using System;
using System.Linq.Expressions;

namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 表示规约基础类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T> : ISpecification<T>
    {
        /// <summary>
        /// Evaluates a LINQ expression to its corresponding specification.
        /// </summary>
        /// <param name="expression">要计算的 LINQ expression</param>
        /// <returns><c>Specification</c></returns>
        public static Specification<T> Eval(Expression<Func<T, bool>> expression)
        {
            return new ExpressionSpecification<T>(expression);
        }

        #region ISpecification<T> Members

        /// <summary>
        /// 返回一个<see cref="System.Boolean"/>值，是否当前给定的对象满足指定的规约
        /// </summary>
        /// <param name="obj">应用于规约的对象</param>
        /// <returns>True 表示满足，False 表示不满足</returns>
        public virtual bool IsSatisfiedBy(T obj)
        {
            return this.GetExpression().Compile()(obj);
        }

        /// <summary>
        /// 组合当前的规约实例和另外一个规约实例，返回两者都必须满足的规约实例
        /// </summary>
        /// <param name="other">被组合的规约实例</param>
        /// <returns>组合的规约实例</returns>
        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        /// <summary>
        /// 一个结合的规约满足给定对象规约中的一个或全部
        /// </summary>
        /// <param name="other">被组合的规约实例</param>
        /// <returns></returns>
        public ISpecification<T> OrSpecification(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        /// <summary>
        /// 一个组合规约，给定的对象满足第一个规约满足，但是不满足第二个规约
        /// </summary>
        /// <param name="other">被组合的规约实例</param>
        /// <returns></returns>
        public ISpecification<T> AndNotSpecification(ISpecification<T> other)
        {
            return new AndNotSpecification<T>(this, other);
        }

        /// <summary>
        /// 与给定规约的语义相反的规约
        /// </summary>
        /// <returns>逆转的规约实例</returns>
        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        /// <summary>
        /// 获取当前规约呈现的 LINQ 表达式
        /// </summary>
        /// <returns>LINQ 表达式</returns>
        public abstract Expression<Func<T, bool>> GetExpression();

        #endregion
    }
}
