using Gear.Infrastructure.Configurations.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Configurations
{
    [TestClass]
    public class CacheConfigurationTest
    {
        [TestMethod]
        public void Configuration_Test()
        {
            var config = CacheConfigurator.Default;

            var absoluteExpiration = config.CacheElement.AbsoluteExpiration;
            var slidingExpiration = config.CacheElement.SlidingExpiration;

            var error = string.Format("absoluteExpiration:{0}; slidingExpiration:{1}", absoluteExpiration, slidingExpiration);

            Assert.IsTrue(absoluteExpiration != 0, error);
        }

        [TestMethod]
        public void Configuration2_Test()
        {
            var config = CacheConfigurator.Default;

            Assert.IsNull(config);
        }
    }
}
