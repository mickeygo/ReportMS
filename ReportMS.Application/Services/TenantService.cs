using System.Collections.Generic;
using System.Linq;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 租户应用服务
    /// </summary>
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        #region Ctor

        public TenantService(ITenantRepository tenantRepository)
        {
            this._tenantRepository = tenantRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取所有的可用的租户信息
        /// </summary>
        /// <returns>租户集合</returns>
        public IEnumerable<TenantDto> GetAllTenants()
        {
            return this._tenantRepository.FindAll().OrderBy(s => s.CreatedDate).MapAs<TenantDto>();
        }

        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns>与租户名匹配的租户</returns>
        public TenantDto GetTenant(string tenantName)
        {
            return this._tenantRepository.GetTenant(tenantName).MapAs<TenantDto>();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._tenantRepository != null)
                this._tenantRepository.Context.Dispose();
        }

        #endregion
    }
}
