using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 执行任务记录仓储
    /// </summary>
    public class TaskRecordRepository : EntityFrameworkRepository<TaskRecord>, ITaskRecordRepository
    {
        public TaskRecordRepository(IRepositoryContext context) : base(context)
        {

        }
    }
}
