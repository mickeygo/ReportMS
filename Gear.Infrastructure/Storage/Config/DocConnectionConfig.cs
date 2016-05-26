using System.Configuration;

namespace Gear.Infrastructure.Storage.Config
{
    /// <summary>
    /// 从应用程序配置文档中定义数据源的连接配置
    /// </summary>
    public class DocConnectionConfig : ConnectionConfig
    {
        /// <summary>
        /// 初始化一个新的<c>DocConnectionConfig</c>实例
        /// </summary>
        /// <param name="connectionName">App.Config / Web.Config 文件中的 ConnectionStrings 节点名</param>
        public DocConnectionConfig(string connectionName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionName];

            this.ConnectionString = connectionStringSettings.ConnectionString;
            this.ProviderName = connectionStringSettings.ProviderName;
        }
    }
}
