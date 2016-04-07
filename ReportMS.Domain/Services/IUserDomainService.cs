using System;
using Gear.Infrastructure.Services.DomainServices;
using ReportMS.Domain.Repositories;

namespace ReportMS.Domain.Services
{
    /// <summary>
    /// 表示实现类为用户的领域服务
    /// </summary>
    public interface IUserDomainService : IDomainService
    {
        /// <summary>
        /// 设置该用户在当前租户中的角色。若角色不存在，表示移除角色
        /// </summary>
        /// <param name="repository">仓储</param>
        /// <param name="userId">用户 Id</param>
        /// <param name="tenantId">租户 Id</param>
        /// <param name="roleId">角色</param>
        /// <param name="handler">操作人</param>
        void SetRoles(IUserRoleRepository repository, Guid userId, Guid tenantId, Guid? roleId, string handler);
    }
}
