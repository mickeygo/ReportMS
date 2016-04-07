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
                topic = service.FindAttachmentTopic(new Guid("77061C76-3497-CAC5-B6A9-08D35DF5CE5C"));
            }

            var job = new AttachmentJobHandler(topic, topic.TopicTasks);
            job.Execute();
        }
    }
}
