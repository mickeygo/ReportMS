using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Web.Client.Membership;

namespace ReportMS.Test.Client.Membership
{
    [TestClass]
    public class UserValidatorTest
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void ValidationRPC_Test()
        {
            var validator = new UserValidator("gang.yang@advantech.com.cn", "1234");
            var result = validator.ValidateInRemote();

            Assert.IsTrue(result);
        }
    }
}
