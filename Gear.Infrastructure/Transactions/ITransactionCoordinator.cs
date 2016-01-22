using System;

namespace Gear.Infrastructure.Transactions
{
    /// <summary>
    /// 表示实现接口类为事务协调者
    /// </summary>
    public interface ITransactionCoordinator : IUnitOfWork, IDisposable
    {
    }
}
