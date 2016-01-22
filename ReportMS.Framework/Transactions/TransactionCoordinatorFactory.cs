using System.Linq;

namespace ReportMS.Framework.Transactions
{
    /// <summary>
    /// 事务协调器工厂
    /// </summary>
    public static class TransactionCoordinatorFactory
    {
        /// <summary>
        /// 创建事务协调器
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ITransactionCoordinator Create(params IUnitOfWork[] args)
        {
            var ret = args.Aggregate(true, (current, arg) => current && arg.DistributedTransactionSupported);

            if (ret)
                return new DistributedTransactionCoordinator(args);
            return new SuppressedTransactionCoordinator(args);
        }
    }
}
