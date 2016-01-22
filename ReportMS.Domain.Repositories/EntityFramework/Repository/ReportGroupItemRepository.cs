using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 报表分组详细信息仓储
    /// </summary>
    public class ReportGroupItemRepository : EntityFrameworkRepository<ReportGroupItem>, IReportGroupItemRepository
    {
        public ReportGroupItemRepository(IRepositoryContext context) : base(context)
        {
            
        }
    }
}
