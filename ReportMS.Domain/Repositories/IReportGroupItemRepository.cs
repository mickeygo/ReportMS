using System;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现接口类为报表分组详细信息仓储
    /// </summary>
    public interface IReportGroupItemRepository : IRepository<ReportGroupItem>
    {
    }
}
