using System.Data;
using MySql.Data.MySqlClient;

namespace Gear.Infrastructure.Storage.MySql
{
    /// <summary>
    /// 表示基于 Oracle MySql 的储存器
    /// </summary>
    public class MySqlStorage : DapperStorage, IStorageProvider
    {
        protected override IDbConnection CreateInternalConnection()
        {
            return MySqlClientFactory.Instance.CreateConnection();
        }
    }
}
