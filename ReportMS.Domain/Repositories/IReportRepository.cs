using System;
using System.Collections.Generic;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现接口的类为报表仓储
    /// </summary>
    public interface IReportRepository : IRepository<Report>
    {
        /// <summary>
        /// 移除报表中指定的字段
        /// </summary>
        /// <param name="fieldId">要移除的字段 Id</param>
        void RemoveFiled(Guid fieldId);

        /// <summary>
        /// 移除报表中指定的字段集合
        /// </summary>
        /// <param name="fields">要移除的字段集合</param>
        void RemoveFileds(IEnumerable<ReportField> fields);
    }
}
