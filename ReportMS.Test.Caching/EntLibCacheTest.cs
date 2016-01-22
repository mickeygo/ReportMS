using Gear.Infrastructure.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Test.Common;

namespace ReportMS.Test.Caching
{
    [TestClass]
    public class EntLibCacheTest
    {
        private readonly string cacheKey = "cacheKey";
        private readonly string cacheValKey = "cacheValKey";

        [TestInitialize]
        public void Initialize()
        {
            Helper.Init();
        }

        [TestMethod]
        public void AddCache()
        {
            CacheManager.Instance.Add(this.cacheKey, this.cacheValKey, "gang.yang");
        }

        [TestMethod]
        public void GetCacheValue_Test()
        {
            var cacheValue = (string)CacheManager.Instance.Get(this.cacheKey, this.cacheValKey);

            Assert.IsTrue(cacheValue == "gang.yang");
        }
    }
}
