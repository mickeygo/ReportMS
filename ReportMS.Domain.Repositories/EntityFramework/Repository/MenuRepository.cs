using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public class MenuRepository : EntityFrameworkRepository<Menu>, IMenuRepository
    {
        public MenuRepository(IRepositoryContext context) : base(context)
        {

        }
    }
}
