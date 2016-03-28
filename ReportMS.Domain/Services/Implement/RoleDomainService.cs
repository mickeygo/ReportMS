using System;

namespace ReportMS.Domain.Services.Implement
{
    /// <summary>
    /// 角色领域服务
    /// </summary>
    public class RoleDomainService : IRoleDomainService
    {
        #region IRoleDomainService Members

        public bool IsOnlyOneRoleInTenantOfUser(Guid userId, Guid tenantId)
        {
            // Todo: check whether only one role in tenant of user
            throw new NotImplementedException();
        }

        #endregion
    }
}
