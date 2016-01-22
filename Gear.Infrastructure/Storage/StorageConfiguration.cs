using System.Configuration;

namespace Gear.Infrastructure.Storage
{
    /// <summary>
    /// 存储配置
    /// </summary>
    public class StorageConfiguration
    {
        private readonly string _connectionName;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>StorageConfiguration</c>实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 节点名</param>
        public StorageConfiguration(string connectionName)
        {
            this._connectionName = connectionName;
        }

        #endregion

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[this._connectionName].ConnectionString;
        }

        /// <summary>
        /// 重写，获取 App.Config / Web.Config 文件中的 ConnectionStrings 节点的字符串
        /// </summary>
        /// <returns>数据库连接字符串</returns>
        public override string ToString()
        {
            return this.GetConnectionString();
        }
    }
}
