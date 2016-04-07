namespace Gear.Infrastructure.Specifications
{
    /// <summary>
    /// 组合规约基类
    /// </summary>
    /// <typeparam name="T">应用于规约的对象类型</typeparam>
    public abstract class CompositeSpecification<T> : Specification<T>, ICompositeSpecification<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="left">左表达式</param>
        /// <param name="rgiht">右表达式</param>
        protected CompositeSpecification(ISpecification<T> left, ISpecification<T> rgiht)
        {
            this.Left = left;
            this.Right = rgiht;
        }

        #region ICompositeSpecification<T> Members

        /// <summary>
        /// 获取规约的左表达式
        /// </summary>
        public ISpecification<T> Left { get; private set; }

        /// <summary>
        /// 获取规约的右表达式
        /// </summary>
        public ISpecification<T> Right { get; private set; }

        #endregion
    }
}
