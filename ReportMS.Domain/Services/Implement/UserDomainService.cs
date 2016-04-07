using System;
using Gear.Infrastructure.Specifications;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;

namespace ReportMS.Domain.Services.Implement
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class UserDomainService : IUserDomainService
    {
        #region IUserDomainService Members

        public void SetRoles(IUserRoleRepository repository, Guid userId, Guid tenantId, Guid? roleId, string handler)
        {
            var spec = Specification<UserRole>.Eval(u => u.UserId == userId)
                        .And(Specification<UserRole>.Eval(u => u.Role.TenantId == tenantId));

            var userRole = repository.Find(spec);
            if (userRole != null)
                repository.Remove(userRole);

            if (!roleId.HasValue)
                return;

            var addUserRole = new UserRole(userId, roleId.Value, handler);
            repository.Add(addUserRole);
        }

        #endregion
    }
}
