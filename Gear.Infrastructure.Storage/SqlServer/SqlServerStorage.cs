using System.Data;
using System.Data.SqlClient;

namespace Gear.Infrastructure.Storage.SqlServer
{
    /// <summary>
    /// 表示基于 Microsoft SQL Server 存储器
    /// </summary>
    public class SqlServerStorage : DapperStorage, IStorageProvider
    {
        /// <summary>
        /// 创建数据源连接
        /// </summary>
        /// <returns><see cref="System.Data.Common.DbConnection"/></returns>
        protected override IDbConnection CreateInternalConnection()
        {
            return SqlClientFactory.Instance.CreateConnection();
        }
    }
}
