using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.ReportModule.RdbmsAggregate;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 关系型数据库管理仓储
    /// </summary>
    public class RdbmsRepository : EntityFrameworkRepository<Rdbms>, IRdbmsRepository
    {
        public RdbmsRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
