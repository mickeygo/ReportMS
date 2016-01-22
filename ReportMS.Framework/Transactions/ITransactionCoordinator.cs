using System;

namespace ReportMS.Framework.Transactions
{
    /// <summary>
    /// 表示实现类为事务协调者
    /// </summary>
    public interface ITransactionCoordinator : IUnitOfWork, IDisposable
    {
    }
}
