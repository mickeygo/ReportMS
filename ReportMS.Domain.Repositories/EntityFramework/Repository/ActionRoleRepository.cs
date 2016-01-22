using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 系统角色可用的功能仓储
    /// </summary>
    public class ActionRoleRepository : EntityFrameworkRepository<ActionRole>, IActionRoleRepository
    {
        public ActionRoleRepository(IRepositoryContext context) : base(context)
        {
            
        }
    }
}
