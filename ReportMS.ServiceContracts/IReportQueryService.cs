using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口类为 Report 查询类
    /// </summary>
    [ServiceContract]
    public interface IReportQueryService : IApplicationQueryService
    {
        /// <summary>
        /// 获取所有的报表数据, 不包含字段
        /// </summary>
        /// <returns>报表数据集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<ReportDto> GetReports();

        /// <summary>
        /// 获取指定的角色所拥有的所有报表，不包含字段
        /// </summary>
        /// <param name="roleId">指定的角色 Id</param>
        /// <returns>报表数据集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<ReportDto> GetReports(Guid roleId);

        /// <summary>
        /// 获取报表数据
        /// </summary>
        /// <param name="reportId">报表 Id</param>
        /// <param name="includeFields">是否包含关联项（Field），默认包含</param>
        /// <returns>报表</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        ReportDto GetReport(Guid reportId, bool includeFields = true);

        /// <summary>
        /// 获取在指定角色中的指定报表。
        /// 会根据不同的角色，指定每个报表所显示的字段
        /// </summary>
        /// <param name="roleId">指定的角色 Id</param>
        /// <param name="reportId">指定的报表 Id</param>
        /// <returns>报表</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        ReportDto GetReport(Guid roleId, Guid reportId);
    }
}
