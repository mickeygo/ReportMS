using System;
using System.Collections.Generic;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为报表配置服务。
    /// </summary>
    public interface IReportProfileService : IApplicationService
    {
        /// <summary>
        /// 查找所有的有效的报表报表配置
        /// </summary>
        /// <returns>报表报表配置集合</returns>
        IEnumerable<ReportProfileDto> FindAllReportProfile();

        /// <summary>
        /// 查找指定的报表报表配置
        /// </summary>
        /// <param name="reportProfileId">报表配置的 Id</param>
        /// <returns>报表报表配置</returns>
        ReportProfileDto FindReportProfile(Guid reportProfileId);

        /// <summary>
        /// 查找指定的报表报表配置
        /// </summary>
        /// <param name="reportProfileName">报表配置的名称</param>
        /// <returns>报表报表配置</returns>
        ReportProfileDto FindReportProfile(string reportProfileName);
    }
}
