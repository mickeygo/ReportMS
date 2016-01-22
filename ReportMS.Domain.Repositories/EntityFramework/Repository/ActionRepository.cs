using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// Action 仓储
    /// </summary>
    public class ActionRepository : EntityFrameworkRepository<Actions>, IActionRepository
    {
        public ActionRepository(IRepositoryContext context) : base(context)
        { }
    }
}
