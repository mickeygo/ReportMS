using System;
using Gear.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Report.Services
{
    [TestClass]
    public class ReportQueryServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            BootStrapper.Start();
        }

        [TestMethod]
        public void GetReports_Test()
        {
            var reportQuery = ServiceLocator.Instance.Resolve<IReportQueryService>();
            var reports = reportQuery.GetReports();

            Assert.IsNotNull(reports);
        }

        [TestMethod]
        public void GetOneReport_Test()
        {
            var reportId = new Guid("88BE0A9A-9DBD-41AC-A2BB-69853FF905DF");
            var reportQuery = ServiceLocator.Instance.Resolve<IReportQueryService>();
            var report = reportQuery.GetReport(reportId);

            Assert.IsNotNull(report);
            Assert.IsTrue(report.ReportName == "Students");
        }
    }
}
