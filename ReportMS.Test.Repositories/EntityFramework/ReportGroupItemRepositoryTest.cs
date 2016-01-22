using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;
using Gear.Infrastructure;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class ReportGroupItemRepositoryTest
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
                var reportGroupItems = repositoryContext.Context.Set<ReportGroupItem>().ToList();
                Assert.IsNotNull(reportGroupItems);
            }
        }

        [TestMethod]
        public void DbContext2_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var reportGroupItems = repositoryContext.Context.Set<ReportGroupItemField>().ToList();
                Assert.IsNotNull(reportGroupItems);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IReportGroupItemRepository>();
            var reportGroupItems = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(reportGroupItems);
        }
    }
}
