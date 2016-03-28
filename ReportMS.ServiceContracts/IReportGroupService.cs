using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
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
        [FaultContract(typeof (FaultData))]
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
        /// 是否已存在指定的报表分组名
        /// </summary>
        /// <param name="reportGroupName">报表分组名</param>
        /// <returns>True 表示已存在，否则为 false</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        bool ExistReportGroup(string reportGroupName);

        /// <summary>
        /// 创建报表分组。
        /// 不会创建报表分组中报表集合信息
        /// </summary>
        /// <param name="reportGroupDto">报表分组 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void CreateReportGroup(ReportGroupDto reportGroupDto);

        /// <summary>
        /// 更新报表分组 Dto 对象。只更新报表分组头
        /// </summary>
        /// <param name="reportGroupDto">报表分组 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void UpdateReportGroup(ReportGroupDto reportGroupDto);

        /// <summary>
        /// 删除指定的报表分组
        /// </summary>
        /// <param name="reportGroupId">要删除的报表分组 Id</param>
        /// <param name="handler">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemoveReportGroup(Guid reportGroupId, string handler);

        /// <summary>
        /// 设置报表分组明细信息
        /// </summary>
        /// <param name="reportGroupId">报表分组 Id</param>
        /// <param name="reportProfileIds">报表配置 Id 集合</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void SetReportGroupItems(Guid reportGroupId, IEnumerable<Guid> reportProfileIds);

        /// <summary>
        /// 删除报表分组明细项
        /// </summary>
        /// <param name="reportGroupId">报表分组 Id</param>
        /// <param name="reportGroupItemId">要删除的报表明细项 Id</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemoveReportGroupItem(Guid reportGroupId, Guid reportGroupItemId);
    }
}
