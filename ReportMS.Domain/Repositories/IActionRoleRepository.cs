using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现接口类为系统角色可用的功能仓储
    /// </summary>
    public interface IActionRoleRepository : IRepository<ActionRole>
    {
    }
}
