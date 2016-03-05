using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 实现此接口的类为租户服务
    /// </summary>
    [ServiceContract]
    public interface ITenantService : IApplicationService
    {
        /// <summary>
        /// 获取所有的可用的租户信息
        /// </summary>
        /// <returns>租户集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<TenantDto> GetAllTenants();

        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="tenantId">租户名 Id</param>
        /// <returns>与租户名匹配的租户</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        TenantDto GetTenant(Guid tenantId);

        /// <summary>
        /// 是否存在租户
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns>true 表示存在；否则为 false</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        bool ExistTenant(string tenantName);

        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns>与租户名匹配的租户</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        TenantDto GetTenant(string tenantName);

        /// <summary>
        /// 创建租户信息
        /// </summary>
        /// <param name="tenantDto">租户对象</param>
        /// <returns>创建的租户</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        TenantDto CreateTenant(TenantDto tenantDto);

        /// <summary>
        /// 更新租户信息
        /// </summary>
        /// <param name="tenantDto">租户对象</param>
        /// <returns>更新后的租户</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        TenantDto UpdateTenant(TenantDto tenantDto);

        /// <summary>
        /// 更新租户信息
        /// </summary>
        /// <param name="tenantId">要删除的租户 Id</param>
        /// <param name="disableBy">删除此租户的操作人</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void DeleteTenant(Guid tenantId, string disableBy);
    }
}
