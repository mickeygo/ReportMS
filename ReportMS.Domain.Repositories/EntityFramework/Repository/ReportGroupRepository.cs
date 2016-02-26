using System.Collections.Generic;
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

        public void RemoveReportGroupItem(ReportGroupItem reportGroupItem)
        {
            var context = this.EFContext.Context.Set<ReportGroupItem>();
            context.Remove(reportGroupItem);
        }

        public void RemoveReportGroupItems(IEnumerable<ReportGroupItem> reportGroupItems)
        {
            var context = this.EFContext.Context.Set<ReportGroupItem>();
            context.RemoveRange(reportGroupItems);
        }
    }
}
