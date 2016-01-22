using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Gear.Infrastructure.Caching
{
    /// <summary>
    /// 基于 .NET Framework 基类库中的缓存，由 System.Runtime.Caching 类提供
    /// 关于此缓存的配置，可以在配置文件中的 <system.runtime.caching /> 节点中配置
    /// </summary>
    public class AspNetCacheProvider : ICacheProvider
    {
        #region Private Fields

        private readonly ObjectCache cacheManager = MemoryCache.Default;

        #endregion

        #region ICacheProvider Members

        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Add(string key, string valKey, object value)
        {
            Dictionary<string, object> dict;
            if (this.cacheManager.Contains(key))
            {
                dict = (Dictionary<string, object>)this.cacheManager[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object> { { valKey, value } };
            }

            var cachePolicy = this.BuildCachePolicy();
            this.cacheManager.Set(key, dict, cachePolicy);
        }

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Put(string key, string valKey, object value)
        {
            this.Add(key, valKey, value);
        }

        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <returns>被缓存的对象。</returns>
        public object Get(string key, string valKey)
        {
            if (this.cacheManager.Contains(key))
            {
                var dict = (Dictionary<string, object>)this.cacheManager[key];
                if (dict != null && dict.ContainsKey(valKey))
                    return dict[valKey];

                return null;
            }
            return null;
        }

        /// <summary>
        /// 从缓存中移除对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        public void Remove(string key)
        {
            this.cacheManager.Remove(key);
        }

        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key)
        {
            return this.cacheManager.Contains(key);
        }

        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值和缓存值键的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <param name="valKey">缓存值键。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key, string valKey)
        {
            return this.cacheManager.Contains(key) &&
                   ((Dictionary<string, object>)this.cacheManager[key]).ContainsKey(valKey);
        }

        #endregion

        #region Private Methods

        private CacheItemPolicy BuildCachePolicy()
        {
            var cachePolicy = new CacheItemPolicy();

            var absoluteExpiration = CacheOptions.AbsoluteExpiration;
            var slidingExpiration = CacheOptions.SlidingExpiration;

            if (absoluteExpiration > 0)
                cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(absoluteExpiration);
            if (slidingExpiration > 0)
                cachePolicy.SlidingExpiration = TimeSpan.FromHours(slidingExpiration);

            return cachePolicy;
        }

        #endregion
    }
}
