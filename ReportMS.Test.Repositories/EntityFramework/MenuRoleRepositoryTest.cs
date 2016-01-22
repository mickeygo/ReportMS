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
    public class MenuRoleRepositoryTest
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
                var users = repositoryContext.Context.Set<UserRole>().ToList();
                Assert.IsNotNull(users);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IMenuRoleRepository>();
            var menuRoles = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(menuRoles);
        }
    }
}
