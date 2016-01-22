using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Gear.Infrastructure.Caching
{
    /// <summary>
    /// 表示基于 Microsoft Patterns & Practices - Enterprise Library Caching Application Block 的缓存机制的实现。
    /// 注意：在 Enterprise Library 6 版本中，已经移除了 Cache 组件。
    /// 对于 Cache 可以考虑试用 .net framework 基类库中的 System.Runtime.Caching 类，或是 Windows Server AppFabric 缓存特性．
    /// </summary>
    public class EntLibCacheProvider : ICacheProvider
    {
        #region Private Fields

        private readonly ICacheManager cacheManager = CacheFactory.GetCacheManager();

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
                dict = (Dictionary<string, object>) this.cacheManager[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object> {{valKey, value}};
            }
            this.cacheManager.Add(key, dict);
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
                var dict = (Dictionary<string, object>) this.cacheManager[key];
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
                   ((Dictionary<string, object>) this.cacheManager[key]).ContainsKey(valKey);
        }

        #endregion
    }
}
