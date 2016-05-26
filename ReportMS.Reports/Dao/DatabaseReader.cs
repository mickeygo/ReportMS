using System;
using Gear.Infrastructure.Storage;
using Gear.Infrastructure.Storage.Config;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// DB 数据读取器
    /// </summary>
    public class DatabaseReader
    {
        private readonly string _connectionName;
        private readonly Tuple<ConnectionOptions, string> _connectionConfig;

        private DatabaseReader(string connectionName)
        {
            this._connectionName = connectionName;
        }

        private DatabaseReader(ConnectionOptions connectionOptions, string providerName)
        {
            this._connectionConfig = Tuple.Create(connectionOptions, providerName);
        }

        #region Public Static Methods

        /// <summary>
        /// 创建一个新的<c>DatabaseReader</c>对象
        /// </summary>
        /// <param name="connectionName">DB 连接名</param>
        /// <returns>数据读取器</returns>
        public static DatabaseReader Create(string connectionName)
        {
            return new DatabaseReader(connectionName);
        }

        public static DatabaseReader Create(ConnectionOptions connectionOptions, string providerName)
        {
            return new DatabaseReader(connectionOptions, providerName);
        }

        /// <summary>
        /// 默认的数据读取对象，连接名为 rms
        /// </summary>
        public static DatabaseReader Default 
        {
            get
            {
                var conn = EnvironmentConnectionString.GetDbConnectionName("rms");
                return new DatabaseReader(conn); 
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取数据读取器
        /// </summary>
        public IStorage Reader
        {
            get
            {
                if (_connectionName != null)
                    return StorageManager.CreateInstance(this._connectionName);
                if (_connectionConfig != null)
                    return StorageManager.CreateInstance(_connectionConfig.Item1, _connectionConfig.Item2);

                throw new InvalidOperationException("The connection is not given.");
            }
        }

        #endregion
    }
}
