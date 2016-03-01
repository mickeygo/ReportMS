using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为报表配置服务。
    /// </summary>
    [ServiceContract]
    public interface IReportProfileService : IApplicationService
    {
        /// <summary>
        /// 查找所有的有效的报表报表配置
        /// </summary>
        /// <returns>报表报表配置集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<ReportProfileDto> FindAllReportProfile();

        /// <summary>
        /// 查找指定的报表报表配置
        /// </summary>
        /// <param name="reportProfileId">报表配置的 Id</param>
        /// <returns>报表报表配置</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        ReportProfileDto FindReportProfile(Guid reportProfileId);

        /// <summary>
        /// 是否存在指定名称的的报表配置
        /// </summary>
        /// <param name="reportProfileName">报表配置的名称</param>
        /// <returns>True 表示存在，否则为 false</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        bool ExistReportProfile(string reportProfileName);

        /// <summary>
        /// 添加报表配置
        /// </summary>
        /// <param name="profileDto">要添加的报表配置信息</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void AddReportProfile(ReportProfileDto profileDto);

        /// <summary>
        /// 更新报表配置头
        /// </summary>
        /// <param name="profileId">要更新的报表配置 Id</param>
        /// <param name="name">要更新报表配置名</param>
        /// <param name="description">要更新的报表配置描述</param>
        /// <param name="updatedBy">更新人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void UpdateReportProfile(Guid profileId, string name, string description, string updatedBy);

        /// <summary>
        /// 设置配置字段。
        /// 移除已有的字段，然后再添加指定的字段
        /// </summary>
        /// <param name="profileId">报表配置 Id</param>
        /// <param name="fields">要添加的字段集合</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void SetProfileFields(Guid profileId, IEnumerable<string> fields);

        /// <summary>
        /// 移除指定的报表配置
        /// </summary>
        /// <param name="reportProfileId">要移除的报表配置 Id</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemoveReportProfile(Guid reportProfileId);

        /// <summary>
        /// 删除指定报表配置中的指定配置字段
        /// </summary>
        /// <param name="profileId">要删除的报表配置 Id</param>
        /// <param name="profileFieldId">要删除的报表配置字段 Id</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemoveReportProfileField(Guid profileId, Guid profileFieldId);
    }
}
