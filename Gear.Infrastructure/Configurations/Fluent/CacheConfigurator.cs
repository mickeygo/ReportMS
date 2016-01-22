namespace Gear.Infrastructure.Configurations.Fluent
{
    public class CacheConfigurator : Configurator<CacheSectionConfiguration>
    {
        #region Private Properties

        private static CacheSectionConfiguration cacheConfiguration;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个<c>CacheConfigurator</c>实例。
        /// 注意：默认节点为 gearCache
        /// </summary>
        public CacheConfigurator()
            : this(ConfigurationOptions.Cache)
        {
        }

        /// <summary>
        ///  初始化一个<c>CacheConfigurator</c>实例。
        /// </summary>
        /// <param name="section">要配置的节点名称</param>
        public CacheConfigurator(string section)
            : base(section)
        {
        }

        #endregion

        #region Static Public Methods

        /// <summary>
        /// 获取默认的邮件配置信息。, 若没有设置配置信息，则返回 null .
        /// 默认节点为 gearMail
        /// </summary>
        public static CacheSectionConfiguration Default
        {
            get
            {
                if (cacheConfiguration == null)
                {
                    var configurator = new CacheConfigurator();
                    cacheConfiguration = configurator.GetConfiguration();
                }
                return cacheConfiguration;
            }
        }

        #endregion
    }
}
