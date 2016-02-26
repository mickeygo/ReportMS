using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为报表分组管理服务类。
    /// 该服务用于对报表分组的管理
    /// </summary>
    [ServiceContract]
    public interface IReportGroupService : IApplicationService
    {
        /// <summary>
        /// 查找所有的有效的报表分组
        /// </summary>
        /// <returns>报表分组 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<ReportGroupDto> FindReportGroups();

        /// <summary>
        /// 查找指定的角色的报表分组
        /// </summary>
        /// <param name="roleId">要查找的报表分组的角色 Id</param>
        /// <returns>报表分组 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<ReportGroupDto> FindReportGroupInRole(Guid roleId);

        /// <summary>
        /// 查找指定的报表分组
        /// </summary>
        /// <param name="reportGroupId">要查找的报表分组 Id</param>
        /// <returns>报表分组 Dto</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        ReportGroupDto FindReportGroup(Guid reportGroupId);

        /// <summary>
        /// 创建报表分组。
        /// 不会创建报表分组中报表集合信息
        /// </summary>
        /// <param name="reportGroupDto">报表分组 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void CreateReportGroup(ReportGroupDto reportGroupDto);

        /// <summary>
        /// 添加报表分组明细信息
        /// </summary>
        /// <param name="reportGroupId">报表分组 Id</param>
        /// <param name="reportProfileIds">报表配置 Id 集合</param>
        void AddReportGroupItems(Guid reportGroupId, IEnumerable<Guid> reportProfileIds);
    }
}
