using System.Threading;
using System.Threading.Tasks;

namespace Gear.Infrastructure
{
    /// <summary>
    /// 表示实现接口类为工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取工作单元是否支持分布式事务（MS-DTC）
        /// </summary>
        bool DistributedTransactionSupported { get; }

        /// <summary>
        /// 获取事务工作单元是否已成功提交
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <param name="cancellationToken">传递操作被取消的通知</param>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}
