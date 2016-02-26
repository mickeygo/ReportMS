using System;
using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 报表配置仓储
    /// </summary>
    public class ReportProfileRepository : EntityFrameworkRepository<ReportProfile>, IReportProfileRepository
    {
        public ReportProfileRepository(IRepositoryContext context)
            : base(context)
        {
            
        }
    }
}
