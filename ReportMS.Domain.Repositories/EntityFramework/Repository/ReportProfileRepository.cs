using System.Collections.Generic;
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

        public void RemoveProfileField(ReportProfileField profileField)
        {
            var context = this.EFContext.Context.Set<ReportProfileField>();
            context.Remove(profileField);
        }

        public void RemoveProfileFields(IEnumerable<ReportProfileField> profileFields)
        {
            var context = this.EFContext.Context.Set<ReportProfileField>();
            context.RemoveRange(profileFields);
        }
    }
}
