using Gear.Infrastructure.Storage;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// DB 数据读取器
    /// </summary>
    public class DatabaseReader
    {
        private readonly string _connectionName;

        private DatabaseReader(string connectionName)
        {
            this._connectionName = connectionName;
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
           get { return StorageManager.CreateInstance(this._connectionName); }
        }

        #endregion
    }
}
