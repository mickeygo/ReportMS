using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Web.Client.Membership;

namespace ReportMS.Test.Client.Membership
{
    [TestClass]
    public class UserValidationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void ValidationRPC_Test()
        {
            var validation = new UserValidation("gang.yang@advantech.com.cn", "1234");
            var result = validation.ValidateInRPC();

            Assert.IsTrue(result);
        }
    }
}
