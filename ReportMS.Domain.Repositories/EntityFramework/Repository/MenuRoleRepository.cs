using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 菜单角色仓储
    /// </summary>
    public class MenuRoleRepository : EntityFrameworkRepository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(IRepositoryContext context) : base(context)
        { }
    }
}
