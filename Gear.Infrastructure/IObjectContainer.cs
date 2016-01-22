namespace Gear.Infrastructure
{
    /// <summary>
    /// 表示实现的类是对象容器
    /// </summary>
    public interface IObjectContainer : IServiceLocator
    {
        /// <summary>
        /// 用 application/web 配置文件初始化对象容器
        /// </summary>
        void InitializeFromConfigFile();

        /// <summary>
        /// 用 application/web 配置文件初始化对象容器
        /// </summary>
        /// <param name="configSectionName"> application/web 配置文件中的配置节点</param>
        void InitializeFromConfigFile(string configSectionName);

        /// <summary>
        /// 从自定义的配置文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        void InitializeFromCustomerConfigFile();

        /// <summary>
        /// 从自定义的配置文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        /// <param name="configFileName">配置文件名</param>
        /// <param name="configSectionName">配置节点名</param>
        void InitializeFromCustomerConfigFile(string configFileName, string configSectionName);

        /// <summary>
        /// 获取被包装的容器实例
        /// </summary>
        /// <typeparam name="T">包装的容器类型</typeparam>
        /// <returns>被包装的容器实例</returns>
        T GetWrappedContainer<T>();
    }
}
