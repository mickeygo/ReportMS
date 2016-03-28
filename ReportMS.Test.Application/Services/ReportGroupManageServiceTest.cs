using System;
using Gear.Infrastructure;
using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Application.Services
{
    [TestClass]
    public class ReportGroupManageServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }

        [TestMethod]
        public void SetGroupItem_Test()
        {
            var reportGroupId = new Guid("314249A5-036E-CE85-7FF3-08D33E9050F4");
            var profiles = new[] { new Guid("572B9D34-0BAA-CE0A-D855-08D33E8269FB") };

            using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
            {
                service.SetReportGroupItems(reportGroupId, profiles);
            }
        }
    }
}
