using System;
using Gear.Infrastructure;
using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;
using ReportMS.Web.Client.Jobs.JobHandlers;

namespace ReportMS.Test.Client.Jobs
{
    [TestClass]
    public class AttachmentJobTest
    {
        [TestInitialize]
        public void Init()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }

        [TestMethod]
        public void ExecuteAttachmentJob_Test()
        {
            AttachmentTopicDto topic;
            using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
            {
                topic = service.FindAttachmentTopic(new Guid("9AEDF329-CED8-C7EA-CFDD-08D36469B6E1"));
            }

            var job = new AttachmentJobHandler(topic, topic.TopicTasks);
            job.Execute();
        }
    }
}
