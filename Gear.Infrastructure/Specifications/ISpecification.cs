using System;
using System.Linq.Expressions;

namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 表示规约接口
    /// 关于规约模式，参考 http://martinfowler.com/apsupp/spec.pdf.
    /// <typeparam name="T">规约对象类型</typeparam>
    /// </summary>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 是否应用的对象满足规约
        /// </summary>
        /// <param name="obj">应用规约的对象</param>
        /// <returns>True，表示满足规约，否则不满足</returns>
        bool IsSatisfiedBy(T obj);

        /// <summary>
        /// 组合当前规约实例和另外一个规约实例
        /// 返回两者都满足的规约实例
        /// </summary>
        /// <param name="other">要组合的规约实例</param>
        /// <returns>组合的规约实例</returns>
        ISpecification<T> And(ISpecification<T> other);

        /// <summary>
        /// 组合当前规约实例和另外一个规约实例
        /// 返回两者至少有一方满足的规约实例
        /// </summary>
        /// <param name="other">要组合的规约实例</param>
        /// <returns>组合的规约实例</returns>
        ISpecification<T> OrSpecification(ISpecification<T> other);

        /// <summary>
        /// 组合当前规约实例和另外一个规约实例
        /// 返回满足当前的契约而不满足另一个契约的实例
        /// </summary>
        /// <param name="other">要组合的规约实例</param>
        /// <returns>组合的规约实例</returns>
        ISpecification<T> AndNotSpecification(ISpecification<T> other);

        /// <summary>
        /// 逆转当前规约实例并返回一个代表相反的语义规范当前规约
        /// </summary>
        /// <returns>规约实例</returns>
        ISpecification<T> Not();

        /// <summary>
        /// 获取当前的契约
        /// </summary>
        /// <returns>LINQ Expression</returns>
        Expression<Func<T, bool>> GetExpression();
    }
}
