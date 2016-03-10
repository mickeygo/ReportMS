using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.SubscriberModule;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class TopicRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Helper.Init();
        }

        [TestMethod]
        public void AttachmentTopic_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var menus = repositoryContext.Context.Set<AttachmentTopic>().ToList();
                Assert.IsNotNull(menus);
            }
        }

        [TestMethod]
        public void TaskRecord_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var menus = repositoryContext.Context.Set<TaskRecord>().ToList();
                Assert.IsNotNull(menus);
            }
        }
    }
}
