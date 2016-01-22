using Gear.Infrastructure.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Test.Common;

namespace ReportMS.Test.Caching
{
    [TestClass]
    public class AspNetCachingTest
    {
        private readonly string cacheKey = "cacheKey";
        private readonly string cacheValKey = "cacheValKey";
        private readonly string cacheValue = "gang.yang";

        [TestInitialize]
        public void Initialize()
        {
            Helper.Init();
        }

        public void AddCache()
        {
            CacheManager.Instance.Add(this.cacheKey, this.cacheValKey, this.cacheValue);
        }

        [TestMethod]
        public void GetCacheValue_Test()
        {
            this.AddCache();
            var cacheVal = (string)CacheManager.Instance.Get(this.cacheKey, this.cacheValKey);

            Assert.IsTrue(cacheVal == this.cacheValue);
        }
    }
}
