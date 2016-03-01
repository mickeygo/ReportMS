using System.Collections.Generic;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现此接口的类为报表配置仓储
    /// </summary>
    public interface IReportProfileRepository : IRepository<ReportProfile>
    {
        /// <summary>
        /// 移除报表配置字段
        /// </summary>
        /// <param name="profileField">要移除的报表配置字段</param>
        void RemoveProfileField(ReportProfileField profileField);

        /// <summary>
        /// 移除报表配置字段集合
        /// </summary>
        /// <param name="profileFields">要移除的报表配置字段集合</param>
        void RemoveProfileFields(IEnumerable<ReportProfileField> profileFields);
    }
}
