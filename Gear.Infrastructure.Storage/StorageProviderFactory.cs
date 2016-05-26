using System;
using System.Data;
using Gear.Infrastructure.Storage.MySql;
using Gear.Infrastructure.Storage.Oracle;
using Gear.Infrastructure.Storage.SqlServer;
using Gear.Infrastructure.Storage.SQLite;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 存储提供程序工厂
    /// </summary>
    public class StorageProviderFactory : DapperStorage, IStorageProvider
    {
        protected override IDbConnection CreateInternalConnection()
        {
            return this.CreateDbConnection(this.ProviderName);
        }

        private IDbConnection CreateDbConnection(string providerName)
        {
            switch (providerName)
            {
                case RdbmsProvider.MSSQL:
                    return new SqlServerStorage().Connection;
                case RdbmsProvider.Oracle:
                    return new OracleStorage().Connection;
                case RdbmsProvider.MySql:
                    return new MySqlStorage().Connection;
                case RdbmsProvider.SQLite:
                    return new SQLiteStorage().Connection;
            }
            
            throw new InvalidOperationException("The provider is invalid.");
        }
    }
}
