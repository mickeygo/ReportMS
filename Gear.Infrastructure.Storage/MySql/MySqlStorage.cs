using System.Data;
using MySql.Data.MySqlClient;

namespace Gear.Infrastructure.Storage.MySql
{
    /// <summary>
    /// 表示基于 Oracle MySql 的储存器
    /// </summary>
    public class MySqlStorage : DapperStorage, IStorageProvider
    {
        /// <summary>
        /// 创建数据源连接
        /// </summary>
        /// <returns><see cref="System.Data.Common.DbConnection"/></returns>
        protected override IDbConnection CreateInternalConnection()
        {
            return MySqlClientFactory.Instance.CreateConnection();
        }
    }
}
