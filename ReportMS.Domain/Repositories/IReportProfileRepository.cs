using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现此接口的类为报表配置仓储
    /// </summary>
    public interface IReportProfileRepository : IRepository<ReportProfile>
    {
    }
}
