using System;
using System.Collections.Generic;
using System.Linq;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.TenantModule;
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

        public IEnumerable<TenantDto> GetAllTenants()
        {
            return this._tenantRepository.FindAll().OrderBy(s => s.CreatedDate).MapAs<TenantDto>();
        }

        public TenantDto GetTenant(Guid tenantId)
        {
            return this._tenantRepository.GetByKey(tenantId).MapAs<TenantDto>();
        }

        public TenantDto GetTenant(string tenantName)
        {
            return this._tenantRepository.GetTenant(tenantName).MapAs<TenantDto>();
        }

        public TenantDto CreateTenant(TenantDto tenantDto)
        {
            var tenant = new Tenant(tenantDto.TenantName, tenantDto.DisplayName, tenantDto.Description, tenantDto.CreatedBy);
            this._tenantRepository.Add(tenant);

            return tenant.MapAs<TenantDto>();
        }

        public TenantDto UpdateTenant(TenantDto tenantDto)
        {
            var tenant = this._tenantRepository.GetByKey(tenantDto.ID);
            tenant.UpdateTenant(tenantDto.DisplayName, tenantDto.Description, tenantDto.UpdatedBy);
            this._tenantRepository.Update(tenant);

            return tenant.MapAs<TenantDto>();
        }

        public void DeleteTenant(Guid tenantId, string disableBy)
        {
            var tenant = this._tenantRepository.GetByKey(tenantId);
            tenant.Disable(disableBy);
            this._tenantRepository.Update(tenant);
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
