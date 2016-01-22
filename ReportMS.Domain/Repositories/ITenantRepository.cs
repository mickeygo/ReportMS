using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.TenantModule;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现接口类为租户仓储
    /// </summary>
    public interface ITenantRepository : IRepository<Tenant>
    {
        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns><c>Tenant</c></returns>
        Tenant GetTenant(string tenantName); 
    }
}
