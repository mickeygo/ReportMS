using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;
using Gear.Infrastructure;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class ReportAggregateRepositoryTest
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
                var reports = repositoryContext.Context.Set<Report>().ToList();
                Assert.IsNotNull(reports);
            }
        }

        [TestMethod]
        public void DbContext2_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var reportFields = repositoryContext.Context.Set<ReportField>().ToList();
                Assert.IsNotNull(reportFields);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IReportRepository>();
            var roles = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(roles);
        }
    }
}
