using System;
using System.Collections.Generic;
using System.Linq;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 客户端提供程序
    /// </summary>
    public static class StorageProviderSetting
    {
        #region Static Properties

        /// <summary>
        /// 获取 SqlClient 客户端提供程序。
        /// IDictionary, key 为命名空间，value 为实例类型
        /// </summary>
        public static IDictionary<string, string> SqlClient = new Dictionary<string, string>
        {
            {"System.Data.SqlClient", "SqlClientFactory"}
        };

        /// <summary>
        /// 获取 OracleClient 客户端提供程序。
        /// IDictionary, key 为命名空间，value 为实例类型
        /// </summary>
        public static IDictionary<string, string> OracleClient = new Dictionary<string, string>
        {
            {"Oracle.ManagedDataAccess.Client", "OracleClientFactory"}
        };

        /// <summary>
        /// 获取 MySqlClient 客户端提供程序。
        /// IDictionary, key 为命名空间，value 为实例类型
        /// </summary>
        public static IDictionary<string, string> MySqlClient = new Dictionary<string, string>
        {
            {"MySql.Data.MySqlClient", "MySqlClientFactory"}
        };

        /// <summary>
        /// 获取 SQLite 客户端提供程序。
        /// IDictionary, key 为命名空间，value 为实例类型
        /// </summary>
        public static IDictionary<string, string> SQLiteClient = new Dictionary<string, string>
        {
            {"System.Data.SQLite", "SQLiteFactory"}
        };

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 获取指定的数据提供者客户端
        /// </summary>
        /// <param name="providerName">数据提供者名称</param>
        /// <returns>KeyValuePair, key 为命名空间，value 为实例类型</returns>
        public static KeyValuePair<string, string> GetProviderClient(string providerName)
        {
            var clients = SqlClient.Union(OracleClient).Union(MySqlClient).Union(SQLiteClient);
            return clients.SingleOrDefault(c => c.Key == providerName);
        }

        /// <summary>
        /// 获取指定的数据提供者客户端的完全限定名
        /// </summary>
        /// <param name="providerName">数据提供者名称</param>
        /// <returns>限定名</returns>
        public static string GetTypeQualifiedName(string providerName)
        {
            var provider = GetProviderClient(providerName);
            return String.Format("{0}, {1}", provider.Value, provider.Key);
        }

        #endregion
    }
}
