using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 用户角色仓储
    /// </summary>
    public class UserRoleRepository : EntityFrameworkRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IRepositoryContext context) : base(context)
        {
            
        }
    }
}
