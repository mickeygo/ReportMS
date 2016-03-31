using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Gear.Infrastructure.Storage.Oracle
{
    /// <summary>
    /// 表示基于 Oracle 的储存器
    /// </summary>
    public class OracleStorage : DapperStorage, IStorageProvider
    {
        protected override IDbConnection CreateInternalConnection()
        {
            return OracleClientFactory.Instance.CreateConnection();
        }
    }
}
