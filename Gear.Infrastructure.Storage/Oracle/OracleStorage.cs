using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Gear.Infrastructure.Storage.Oracle
{
    /// <summary>
    /// 表示基于 Oracle 的储存器
    /// </summary>
    public class OracleStorage : DapperStorage, IStorageProvider
    {
        /// <summary>
        /// 创建数据源连接
        /// </summary>
        /// <returns><see cref="System.Data.Common.DbConnection"/></returns>
        protected override IDbConnection CreateInternalConnection()
        {
            return OracleClientFactory.Instance.CreateConnection();
        }
    }
}
