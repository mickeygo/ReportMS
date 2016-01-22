using System;
using System.Runtime.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Caching
{
    [TestClass]
    public class AspNetCachingDirectTest
    {
        private readonly ObjectCache cacheManager = MemoryCache.Default;

        [TestMethod]
        public void Get_Test()
        {
            var key = "Test";
            this.cacheManager.Set(key, "Gang.yang", DateTimeOffset.Now.AddMinutes(20));
            var value = (string) cacheManager[key];

            Assert.IsTrue(value == "Gang.yang", value);
        }
    }
}
