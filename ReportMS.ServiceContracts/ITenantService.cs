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
        /// <param name="tenantName">租户名</param>
        /// <returns>与租户名匹配的租户</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        TenantDto GetTenant(string tenantName);
    }
}
