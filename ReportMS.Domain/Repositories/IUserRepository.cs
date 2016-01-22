using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示接口实现类为用户仓储
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
    }
}
