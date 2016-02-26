using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;
using Gear.Infrastructure;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class ReportProfileRepositoryTest
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
                var reportGroupItems = repositoryContext.Context.Set<ReportProfile>().ToList();
                Assert.IsNotNull(reportGroupItems);
            }
        }

        [TestMethod]
        public void DbContext2_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var reportGroupItems = repositoryContext.Context.Set<ReportProfileField>().ToList();
                Assert.IsNotNull(reportGroupItems);
            }
        }
    }
}
