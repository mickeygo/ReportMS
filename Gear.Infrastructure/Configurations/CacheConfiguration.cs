using System.Configuration;

namespace Gear.Infrastructure.Configurations
{
    /// <summary>
    /// 缓存 节点配置类
    /// </summary>
    public class CacheSectionConfiguration : ConfigurationSection
    {
        #region Properties

        internal const string CachePropertyName = "cache";

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置邮件配置元素，元素节点名为 emailClient
        /// </summary>
        [ConfigurationProperty(CachePropertyName, IsRequired = true)]
        public CacheElementConfiguration CacheElement
        {
            get { return (CacheElementConfiguration)this[CachePropertyName]; }
            set { this[CachePropertyName] = value; }
        }

        #endregion
    }

    public class CacheElementConfiguration : ConfigurationElement
    {
        #region Properties

        internal const string AbsoluteExpirationPropertyName = "absoluteExpiration";
        internal const string SlidingExpirationPropertyName = "slidingExpiration";

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置缓存的绝对过期时间
        /// </summary>
        [ConfigurationProperty(AbsoluteExpirationPropertyName, DefaultValue = "0", IsRequired = false, IsKey = false)]
        [IntegerValidator(ExcludeRange = false, MinValue = 0)]
        public int AbsoluteExpiration
        {
            get { return (int) this[AbsoluteExpirationPropertyName]; }
            set { this[AbsoluteExpirationPropertyName] = value; }
        }

        /// <summary>
        /// 获取或设置缓存的相对过期时间
        /// </summary>
        [ConfigurationProperty(SlidingExpirationPropertyName, DefaultValue = "0", IsRequired = false, IsKey = false)]
        [IntegerValidator(ExcludeRange = false, MinValue = 0)]
        public int SlidingExpiration
        {
            get { return (int) this[SlidingExpirationPropertyName]; }
            set { this[SlidingExpirationPropertyName] = value; }
        }

        #endregion
    }
}
