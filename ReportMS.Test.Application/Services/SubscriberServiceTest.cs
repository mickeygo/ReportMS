using System.Linq;
using Gear.Infrastructure;
using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Application.Services
{
    [TestClass]
    public class SubscriberServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }

        [TestMethod]
        public void FindAttachmentTopics_Test()
        {
            using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
            {
                var attachmentTopics = service.FindAttachmentTopics();
                Assert.IsNull(attachmentTopics, attachmentTopics.Count().ToString());
            }
        }
    }
}
