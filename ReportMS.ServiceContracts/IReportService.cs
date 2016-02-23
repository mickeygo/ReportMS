using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现类为报表服务
    /// </summary>
    [ServiceContract]
    public interface IReportService : IApplicationService
    {
        /// <summary>
        /// 查找所有有效的报表
        /// </summary>
        /// <returns>报表 Dto 对象集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<ReportDto> FindAllReport();

        /// <summary>
        /// 查找指定有效的报表
        /// </summary>
        /// <param name="reportId">报表 Id</param>
        /// <returns>报表 Dto 对象</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        ReportDto FindReport(Guid reportId);

        /// <summary>
        /// 查找指定有效的报表
        /// </summary>
        /// <param name="reportName">要查找的报表名</param>
        /// <returns>报表 Dto 对象</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        ReportDto FindReport(string reportName);

        /// <summary>
        /// 创建报表
        /// </summary>
        /// <param name="reportDto">报表 Dto 对象</param>
        /// <returns>返回创建的报表对象</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        ReportDto CreateReport(ReportDto reportDto);

        /// <summary>
        /// 更新报表
        /// </summary>
        /// <param name="reportDto">要更新的报表</param>
        /// <returns>返回更新后的报表对象</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        ReportDto UpdateReport(ReportDto reportDto);

        /// <summary>
        /// 删除报表
        /// </summary>
        /// <param name="reportId">要删除的报表 Id</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void DeleteReport(Guid reportId);

        /// <summary>
        /// 移除报表中指定的字段
        /// </summary>
        /// <param name="fieldId">要移除的字段 Id</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void RemoveField(Guid fieldId);

        /// <summary>
        /// 移除报表中所有的字段，然后添加指定的字段到该报表中
        /// </summary>
        /// <param name="reportId">报表 Id</param>
        /// <param name="fieldDtos">要更新的字段</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void RemoveAllThenAddFields(Guid reportId, IEnumerable<ReportFieldDto> fieldDtos);
    }
}
