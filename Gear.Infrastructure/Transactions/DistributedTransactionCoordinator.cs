using System.Transactions;

namespace Gear.Infrastructure.Transactions
{
    internal class DistributedTransactionCoordinator : TransactionCoordinator
    {
        #region Private Fields
        private readonly TransactionScope scope = new TransactionScope();
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>DistributedTransactionCoordinator</c>实例
        /// </summary>
        /// <param name="unitOfWorks">工作单元集</param>
        public DistributedTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
            
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing">是否显示地释放对象</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.scope.Dispose();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// 提交事务
        /// </summary>
        public override void Commit()
        {
            base.Commit();
            this.scope.Complete();
        }

        #endregion
    }
}
