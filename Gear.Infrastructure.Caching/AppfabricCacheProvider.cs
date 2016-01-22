using System;

namespace Gear.Infrastructure.Caching
{
    /// <summary>
    /// 基于 Windows Server AppFabric 缓存
    /// </summary>
    public class AppfabricCacheProvider : ICacheProvider
    {
        #region ICacheProvider Members

        public void Add(string key, string valKey, object value)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, string valKey, object value)
        {
            throw new NotImplementedException();
        }

        public object Get(string key, string valKey)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key, string valKey)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
