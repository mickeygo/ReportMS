using System.Collections.Generic;
using System.Data;

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
            var config = new StorageConfiguration(connectionName);
            this.storageProvider.BuildConnection(config);           
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 创建容器管理实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 连接名</param>
        /// <returns></returns>
        public static IStorage CreateInstance(string connectionName)
        {
            return new StorageManager(connectionName);
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
