using Gear.Infrastructure.Configurations.Fluent;

namespace Gear.Infrastructure.Caching
{
    /// <summary>
    /// 缓存选项
    /// </summary>
    public static class CacheOptions
    {
        /// <summary>
        /// 获取缓存的绝对过期时间长度, 单位 hour, 默认为 0
        /// </summary>
        public static int AbsoluteExpiration
        {
            get { return CacheConfigurator.Default.CacheElement.AbsoluteExpiration; }
        }

        /// <summary>
        /// 获取缓存的相对过期时间长度, 单位 hour, 默认为 0
        /// </summary>
        public static int SlidingExpiration
        {
            get { return CacheConfigurator.Default.CacheElement.SlidingExpiration; }
        }
    }
}
