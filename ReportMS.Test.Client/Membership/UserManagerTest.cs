using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.Test.Common;
using ReportMS.Web.Client.Membership;

namespace ReportMS.Test.Client.Membership
{
    [TestClass]
    public class UserManagerTest
    {
        [TestInitialize]
        public void Init()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }

        [TestMethod]
        public void UpdateUser_Test()
        {
            UserManager.Instance.UpdateUser("Gang.Yang@advantech.com.cn");
        }
    }
}
