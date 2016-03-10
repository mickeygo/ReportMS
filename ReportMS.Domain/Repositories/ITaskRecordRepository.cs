using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现此接口的类为任务执行记录仓储
    /// </summary>
    public interface ITaskRecordRepository : IRepository<TaskRecord>
    {
    }
}
