using System.Data;
using System.Data.SQLite;

namespace Gear.Infrastructure.Storage.SQLite
{
    /// <summary>
    /// 表示基于 SQLite 的储存器
    /// </summary>
    public class SQLiteStorage : DapperStorage, IStorageProvider
    {
        protected override IDbConnection CreateInternalConnection()
        {
            return SQLiteFactory.Instance.CreateConnection();
        }
    }
}
