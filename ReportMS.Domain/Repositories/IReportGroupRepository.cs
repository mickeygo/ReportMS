using System.Collections.Generic;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现此接口的类为报表分组仓储
    /// </summary>
    public interface IReportGroupRepository : IRepository<ReportGroup>
    {
        /// <summary>
        /// 移除指定的报表分组项
        /// </summary>
        /// <param name="reportGroupItem">要移除的报表分组项</param>
        void RemoveReportGroupItem(ReportGroupItem reportGroupItem);

        /// <summary>
        /// 移除指定的报表分组项集合
        /// </summary>
        /// <param name="reportGroupItems">要移除的报表分组项集合</param>
        void RemoveReportGroupItems(IEnumerable<ReportGroupItem> reportGroupItems);
    }
}
