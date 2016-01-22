namespace Gear.Infrastructure.Configurations.Fluent
{
    /// <summary>
    /// 系统管理配置器
    /// </summary>
    public class SystemAdminConfigurator : Configurator<SystemAdminSectionConfiguration>
    {
        #region Private Properties

        private static SystemAdminSectionConfiguration systemAdminConfiguration;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>SystemAdminConfigurator</c>实例
        /// </summary>
        public SystemAdminConfigurator()
            : this(ConfigurationOptions.Administrator)
        {
        }

        /// <summary>
        /// 初始化一个新的<c>SystemAdminConfigurator</c>实例
        /// </summary>
        /// <param name="section">节点名称</param>
        public SystemAdminConfigurator(string section)
            : base(section)
        {
        }

        #endregion

        #region Static Public Methods

        /// <summary>
        /// 获取默认的系统管理配置信息。
        /// 默认节点为 gearAdmin
        /// </summary>
        public static SystemAdminSectionConfiguration Default
        {
            get
            {
                if (systemAdminConfiguration == null)
                {
                    var configurator = new SystemAdminConfigurator();
                    systemAdminConfiguration = configurator.GetConfiguration();
                }
                return systemAdminConfiguration;
            }
        }

        #endregion
    }
}
