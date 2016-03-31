using System.Configuration;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 存储配置
    /// </summary>
    public class StorageConfiguration
    {
        private readonly ConnectionStringSettings _connectionStringSettings;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>StorageConfiguration</c>实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 节点名</param>
        public StorageConfiguration(string connectionName)
        {
            this._connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionName];
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionStringSettings.ConnectionString; }
        }

        /// <summary>
        /// 获取程序提供者名称
        /// </summary>
        public string ProviderName
        {
             get { return this._connectionStringSettings.ProviderName; }
        }

        #endregion

        /// <summary>
        /// 重写，获取 App.Config / Web.Config 文件中的 ConnectionStrings 节点的字符串
        /// </summary>
        /// <returns>数据库连接字符串</returns>
        public override string ToString()
        {
            return this.ConnectionString;
        }
    }
}
