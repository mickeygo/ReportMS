using Gear.Infrastructure;
using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Application.QueryServices
{
    [TestClass]
    public class UserQueryServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }

        [TestMethod]
        public void FindUser_Test()
        {
            var userName = "gang.yang@advantech.com.cn";
            var userService = ServiceLocator.Instance.Resolve<IUserQueryService>();

            var user = userService.Find(userName);

            Assert.IsNull(user, Utils.LookupEntityDetail(user));
        }
    }
}
