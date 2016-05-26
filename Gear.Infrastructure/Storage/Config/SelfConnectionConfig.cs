namespace Gear.Infrastructure.Storage.Config
{
    /// <summary>
    /// 自定义的数据源连接配置
    /// </summary>
    public class SelfConnectionConfig : ConnectionConfig
    {
        /// <summary>
        /// 初始化一个新的<c>SelfConnectionConfig</c>实例
        /// </summary>
        /// <param name="connectionOptions">数据源连接选项</param>
        /// <param name="providerName">数据库连接程序的提供者</param>
        public SelfConnectionConfig(ConnectionOptions connectionOptions, string providerName)
        {
            this.ConnectionString = connectionOptions.ToString();
            this.ProviderName = providerName;
        }
    }
}
