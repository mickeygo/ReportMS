namespace Gear.Infrastructure.Storage.Config
{
    /// <summary>
    /// 数据源连接配置基础类
    /// </summary>
    public abstract class ConnectionConfig
    {
        /// <summary>
        /// 获取数据库的连接字符串
        /// </summary>
        public string ConnectionString { get; protected set; }

        /// <summary>
        /// 获取数据库的程序提供者
        /// </summary>
        public string ProviderName { get; protected set; }
    }
}
