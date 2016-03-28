using System;
using Gear.Infrastructure.Services.DomainServices;

namespace ReportMS.Domain.Services
{
    /// <summary>
    /// 表示实现此接口的类为角色领域服务
    /// </summary>
    public interface IRoleDomainService : IDomainService
    {
        /// <summary>
        /// 判断用户在租户中是否只存在一种角色
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="tenantId">租户 Id</param>
        /// <returns>true 表示用户在租户中只存在一种角色；否则为 false</returns>
        bool IsOnlyOneRoleInTenantOfUser(Guid userId, Guid tenantId);
    }
}
