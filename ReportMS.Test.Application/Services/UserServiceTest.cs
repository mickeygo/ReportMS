using Gear.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Application.Services
{
    [TestClass]
    public class UserServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            BootStrapper.Start();
        }

        [TestMethod]
        public void GetUserByName_Test()
        {
            var userName = "gang.yang@advantech.com.cn";
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                var user = userService.FindUser(userName);

                Assert.IsNull(user);
            }
        }
    }
}
