using System;
using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;
using Gear.Infrastructure;
using System.Data.Entity;

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
                var reportField = reportFields.Single(f => f.ID == new Guid("40AE807E-0CA5-4BEC-ADF3-56DF30A313BB"));
                repositoryContext.Context.SaveChanges();

                Assert.IsNotNull(reportFields);
                Assert.IsNotNull(reportField);
            }
        }

        [TestMethod]
        public void DbContextRemoveField_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var report =
                    repositoryContext.Context.Set<Report>().Include(r => r.Fields)
                        .Single(r => r.ID == new Guid("E84B3AE8-F47B-4012-9E9F-8DEC13FFC096"));

                var reportField = (
                    from field in report.Fields
                    where field.ID == new Guid("40AE807E-0CA5-4BEC-ADF3-56DF30A313BB")
                    select field
                    ).Single();

                report.Fields.Remove(reportField);
                repositoryContext.Context.SaveChanges();

                Assert.IsNotNull(report);
                Assert.IsNotNull(report.Fields);
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
