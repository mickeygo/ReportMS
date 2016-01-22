using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.EntityFramework;
using Gear.Infrastructure;
using ReportMS.Test.Common;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class ActionRepositoryTest
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
                var actions = repositoryContext.Context.Set<Actions>().ToList();
                Assert.IsNotNull(actions);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IActionRepository>();
            var actions = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(actions);
        }
    }
}
