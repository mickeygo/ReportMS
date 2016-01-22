using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using Gear.Infrastructure.Specifications;
using ReportMS.Domain.Models.TenantModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 租户仓储
    /// </summary>
    public class TenantRepository : EntityFrameworkRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(IRepositoryContext context)
            : base(context)
        { }

        #region ITenantRepository Members

        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns>与租户名匹配的租户</returns>
        public Tenant GetTenant(string tenantName)
        {
            var findTenantByNameSpec = Specification<Tenant>.Eval(s => s.TenantName == tenantName);
            return this.DoFind(findTenantByNameSpec);
        }

        #endregion
    }
}
