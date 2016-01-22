namespace ReportMS.Framework.Specifications
{
    /// <summary>
    /// 组合规约
    /// </summary>
    /// <typeparam name="T">规约对象类型</typeparam>
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// 获取规约的左边表达式
        /// </summary>
        ISpecification<T> Left { get; }

        /// <summary>
        /// 获取规约的右边表达式
        /// </summary>
        ISpecification<T> Right { get; }
    }
}
