using System.Data;
using System.Data.SQLite;

namespace Gear.Infrastructure.Storage.SQLite
{
    /// <summary>
    /// 表示基于 SQLite 的储存器
    /// </summary>
    public class SQLiteStorage : DapperStorage, IStorageProvider
    {
        /// <summary>
        /// 创建数据源连接
        /// </summary>
        /// <returns><see cref="System.Data.Common.DbConnection"/></returns>
        protected override IDbConnection CreateInternalConnection()
        {
            return SQLiteFactory.Instance.CreateConnection();
        }
    }
}
