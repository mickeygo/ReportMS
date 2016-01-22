using Gear.Infrastructure.Configurations.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Configurations
{
    [TestClass]
    public class MailConfigurationTest
    {
        [TestMethod]
        public void Configuration_Test()
        {
            var config = MailConfigurator.Default;

            var host = config.EmailClientElement.Host;
            var post =  config.EmailClientElement.Port;
            var userName =  config.EmailClientElement.UserName;
            var password =  config.EmailClientElement.Password;
            var enableSsl =  config.EmailClientElement.EnableSsl;
            var sender =  config.EmailClientElement.Sender;
            var displayName =  config.EmailClientElement.DisplayName;

            var error = string.Format("host:{0}; post:{1}; username:{2}; password:{3}; " +
                                      "enableSsl:{4}; sender:{5}; displayName:{6}", host, post, userName, password, enableSsl, sender, displayName);

            Assert.IsNull(host, error);
        }
    }
}
