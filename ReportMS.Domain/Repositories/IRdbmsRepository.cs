using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.ReportModule.RdbmsAggregate;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现此接口的类为关系型数据库仓储
    /// </summary>
    public interface IRdbmsRepository : IRepository<Rdbms>
    {
    }
}
