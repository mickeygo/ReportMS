using System.Collections.Generic;
using System.Data;
using Gear.Infrastructure.Storage.Config;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 储存器管理类
    /// Remark：目前只用来做数据查询
    /// </summary>
    public sealed class StorageManager : IStorage
    {
        #region Private Fields

        private readonly IStorageProvider storageProvider;

        #endregion

        #region Ctor

        private StorageManager()
        {
            this.storageProvider = ServiceLocator.Instance.Resolve<IStorageProvider>();
        }

        private StorageManager(string connectionName)
            : this()
        {
            var config = new DocConnectionConfig(connectionName);
            this.storageProvider.BuildConnection(config);        
        }

        private StorageManager(ConnectionOptions connectionOptions, string providerName)
            : this()
        {
            var config = new SelfConnectionConfig(connectionOptions, providerName);
            this.storageProvider.BuildConnection(config);      
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 创建容器管理实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 连接名</param>
        /// <returns>仓储容器</returns>
        public static IStorage CreateInstance(string connectionName)
        {
            return new StorageManager(connectionName);
        }

        /// <summary>
        /// 创建容器管理实例
        /// </summary>
        /// <param name="connectionOptions">数据源连接配置</param>
        /// <param name="providerName">数据源提供程序</param>
        /// <returns>仓储容器</returns>
        public static IStorage CreateInstance(ConnectionOptions connectionOptions, string providerName)
        {
            return new StorageManager(connectionOptions, providerName);
        }

        /// <summary>
        /// 获取关系型数据库连接测试实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 连接名</param>
        /// <returns>关系型数据库连接测试实例</returns>
        public static RdbmsConnectTest ConnectionTest(string connectionName)
        {
            var storage = new StorageManager(connectionName);
            return new RdbmsConnectTest(storage.GetDbConnection());
        }

        /// <summary>
        /// 获取关系型数据库连接测试实例
        /// </summary>
        /// <param name="connectionOptions">数据源连接配置</param>
        /// <param name="providerName">数据源提供程序</param>
        /// <returns>关系型数据库连接测试实例</returns>
        public static RdbmsConnectTest ConnectionTest(ConnectionOptions connectionOptions, string providerName)
        {
            var storage = new StorageManager(connectionOptions, providerName);
            return new RdbmsConnectTest(storage.GetDbConnection());
        }

        #endregion

        #region Private Methods

        private IDbConnection GetDbConnection()
        {
            return this.storageProvider.Connection;
        }

        #endregion

        #region Interface IStorage Members

        public T SelectFirstOrDefault<T>(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.SelectFirstOrDefault<T>(sqlQuery, param);
        }

        public T SelectFirstOrDefault<T>(string sqlQuery, object param = null)
        {
            return this.storageProvider.SelectFirstOrDefault<T>(sqlQuery, param);
        }

        public IEnumerable<T> Select<T>(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.Select<T>(sqlQuery, param);
        }

        public IEnumerable<T> Select<T>(string sqlQuery, int start, int count, IDictionary<string, object> param)
        {
            return this.storageProvider.Select<T>(sqlQuery, start, count, param);
        }

        public IEnumerable<T> Select<T>(string sqlQuery, object param = null)
        {
            return this.storageProvider.Select<T>(sqlQuery, param);
        }

        public IEnumerable<T> Select<T>(string sqlQuery, int start, int count, object param = null)
        {
            return this.storageProvider.Select<T>(sqlQuery, start, count, param);
        }

        public int GetRecordCount(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.GetRecordCount(sqlQuery, param);
        }

        public int GetRecordCount(string sqlQuery, object param = null)
        {
            return this.storageProvider.GetRecordCount(sqlQuery, param);
        }

        public IDataReader GetDataReader(string sqlQuery, IDictionary<string, object> param)
        {
            return this.storageProvider.GetDataReader(sqlQuery, param);
        }

        public IDataReader GetDataReader(string sqlQuery, int start, int count, IDictionary<string, object> param)
        {
            return this.storageProvider.GetDataReader(sqlQuery, start, count, param);
        }

        public IDataReader GetDataReader(string sqlQuery, object param = null)
        {
            return this.storageProvider.GetDataReader(sqlQuery, param);
        }

        public IDataReader GetDataReader(string sqlQuery, int start, int count, object param = null)
        {
            return this.storageProvider.GetDataReader(sqlQuery, start, count, param);
        }

        #endregion
    }
}
