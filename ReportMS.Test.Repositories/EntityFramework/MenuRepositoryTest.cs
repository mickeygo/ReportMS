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
    public class MenuRepositoryTest
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
                var menus = repositoryContext.Context.Set<Menu>().ToList();
                Assert.IsNotNull(menus);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IMenuRepository>();
            var menus = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(menus);
        }
    }
}
