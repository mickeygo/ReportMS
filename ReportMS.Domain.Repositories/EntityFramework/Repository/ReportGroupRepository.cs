using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 报表分组仓储
    /// </summary>
    public class ReportGroupRepository : EntityFrameworkRepository<ReportGroup>, IReportGroupRepository
    {
        public ReportGroupRepository(IRepositoryContext context) : base(context)
        { }
    }
}
