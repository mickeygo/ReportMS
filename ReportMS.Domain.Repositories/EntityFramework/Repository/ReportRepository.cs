using System;
using System.Collections.Generic;
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

        public void RemoveFiled(Guid fieldId)
        {
            var context = this.EFContext.Context.Set<ReportField>();
            var field = context.Find(fieldId);
            if (field != null)
                context.Remove(field);
        }

        public void RemoveFileds(IEnumerable<ReportField> fields)
        {
            var context = this.EFContext.Context.Set<ReportField>();
            context.RemoveRange(fields);
        }
    }
}
