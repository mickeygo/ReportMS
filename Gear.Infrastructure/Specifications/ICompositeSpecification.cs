namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 表示实现此接口的类为组合规约
    /// </summary>
    /// <typeparam name="T">规约对象类型</typeparam>
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// 获取规约的左表达式
        /// </summary>
        ISpecification<T> Left { get; }

        /// <summary>
        /// 获取规约的右表达式
        /// </summary>
        ISpecification<T> Right { get; }
    }
}
