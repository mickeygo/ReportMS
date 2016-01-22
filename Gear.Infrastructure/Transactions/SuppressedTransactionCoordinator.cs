namespace Gear.Infrastructure.Transactions
{
    /// <summary>
    /// 事务协调器，简单提交每个被托管的工作单元实例
    /// </summary>
    internal class SuppressedTransactionCoordinator : TransactionCoordinator
    {
        /// <summary>
        /// 初始化<c>SuppressedTransactionCoordinator</c>实例
        /// </summary>
        /// <param name="unitOfWorks">工作单元集</param>
        public SuppressedTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
            
        }
    }
}
