using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;
using Gear.Infrastructure;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class UserRoleRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Helper.Init();
        }

        [TestMethod]
        public void DbContext_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var userRoles = repositoryContext.Context.Set<UserRole>().ToList();
                repositoryContext.Context.Dispose();

                Assert.IsNotNull(userRoles);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IUserRoleRepository>();
            var userRoles = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(userRoles);
        }
    }
}
