using System;
using Gear.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Application.Services
{
    [TestClass]
    public class ReportProfileServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            BootStrapper.Start();
        }

        [TestMethod]
        public void AddProfileFields_Test()
        {
            var profileId = new Guid("B9FC81F7-72F0-C98F-8234-08D33E54320A");
            var fields = new[] { "SNO", "Name", "ProductionDate" };

            using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                service.SetProfileFields(profileId, fields);
            }
        }
    }
}
