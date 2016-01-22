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
    public class ReportGroupRoleRepositoryTest
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
                var reportGroupRoles = repositoryContext.Context.Set<ReportGroupRole>().ToList();
                Assert.IsNotNull(reportGroupRoles);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IReportGroupRoleRepository>();
            var reportGroupRoles = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(reportGroupRoles);
        }
    }
}
