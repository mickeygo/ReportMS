using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 报表组角色仓储
    /// </summary>
    public class ReportGroupRoleRepository : EntityFrameworkRepository<ReportGroupRole>, IReportGroupRoleRepository
    {
        public ReportGroupRoleRepository(IRepositoryContext context) : base(context)
        {
            
        }
    }
}
