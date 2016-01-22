using Gear.Infrastructure.Configurations.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Configurations
{
    [TestClass]
    public class SystemAdminConfigurationTest
    {
        [TestMethod]
        public void Configuration_Test()
        {
            var config = SystemAdminConfigurator.Default;
            var administrator = config.SystemAdminElement.Administrator;

            Assert.IsTrue(administrator == "gang.yang@advantech.com.cn");
        }
    }
}
