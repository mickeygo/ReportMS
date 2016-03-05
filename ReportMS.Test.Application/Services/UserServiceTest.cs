using System;
using Gear.Infrastructure;
using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
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
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }

        [TestMethod]
        public void GetUserByName_Test()
        {
            var userName = "gang.yang@advantech.com.cn";
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                var user = userService.FindUser(userName);

                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void FindRolesByUser_Test()
        {
            var userId = new Guid("FA1DE031-CE61-43CF-84C8-ACAD33E32737");
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                var roles = userService.FindRoles(userId);

                Assert.IsNull(roles);
            }
        }

        [TestMethod]
        public void FindRoleByUserAndTenant_Test()
        {
            var userId = new Guid("FA1DE031-CE61-43CF-84C8-ACAD33E32737");
            var tenantId = new Guid("02B32745-1F0E-4FDC-AA20-CAC4FECC3B38");
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                var role = userService.FindRole(userId, tenantId);

                Assert.IsNull(role);
            }
        }
    }
}
