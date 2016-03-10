using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 附件主题仓储
    /// </summary>
    public class AttachmentTopicRepository : EntityFrameworkRepository<AttachmentTopic>, IAttachmentTopicRepository
    {
        public AttachmentTopicRepository(IRepositoryContext context)
            : base(context)
        {
        }
    }
}
