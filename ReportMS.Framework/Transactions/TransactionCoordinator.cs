using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMS.Framework.Transactions
{
    /// <summary>
    /// 事务协调器基类
    /// </summary>
    public abstract class TransactionCoordinator : DisposableObject, ITransactionCoordinator
    {
        #region Private Fields
        private readonly List<IUnitOfWork> managedUnitOfWorks = new List<IUnitOfWork>();
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>TransactionCoordinator</c>实例
        /// </summary>
        /// <param name="unitOfWorks">工作单元集合</param>
        protected TransactionCoordinator(params IUnitOfWork[] unitOfWorks)
        {
            if (unitOfWorks == null || !unitOfWorks.Any())
                throw new ArgumentNullException("unitOfWorks");
            this.managedUnitOfWorks.AddRange(unitOfWorks);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing">是否要显示释放</param>
        protected override void Dispose(bool disposing)
        {
            
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// 获取工作单元是否支持分布式事务（MS-DTC）
        /// </summary>
        public virtual bool DistributedTransactionSupported
        {
            get { return false; }
        }

        /// <summary>
        /// 获取事务工作单元是否已成功提交
        /// </summary>
        public virtual bool Committed
        {
            get { return true; }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public virtual void Commit()
        {
            foreach (var uow in this.managedUnitOfWorks)
                uow.Commit();
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        public virtual Task CommitAsync()
        {
            return this.CommitAsync(CancellationToken.None);
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <param name="cancellationToken">传递操作被取消的通知</param>
        /// <returns></returns>
        public virtual async Task CommitAsync(CancellationToken cancellationToken)
        {
            foreach (var uow in managedUnitOfWorks)
                await uow.CommitAsync(cancellationToken);
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public virtual void Rollback()
        { }

        #endregion
    }
}
