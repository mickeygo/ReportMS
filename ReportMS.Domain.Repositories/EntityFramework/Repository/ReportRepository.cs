using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 报表仓储
    /// </summary>
    public class ReportRepository : EntityFrameworkRepository<Report>, IReportRepository
    {
        public ReportRepository(IRepositoryContext context)
            : base(context)
        { }
    }
}
