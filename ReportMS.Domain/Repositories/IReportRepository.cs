using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现接口的类为报表仓储
    /// </summary>
    public interface IReportRepository : IRepository<Report>
    {
    }
}
