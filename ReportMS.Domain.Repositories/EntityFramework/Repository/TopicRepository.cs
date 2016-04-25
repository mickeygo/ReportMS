using System.Collections.Generic;
using Gear.Infrastructure.Repositories;
using Gear.Infrastructure.Repository.EntityFramework;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories.EntityFramework.Repository
{
    /// <summary>
    /// 主题仓储
    /// </summary>
    public class TopicRepository : EntityFrameworkRepository<Topic>, ITopicRepository
    {
        public TopicRepository(IRepositoryContext context) : base(context)
        {
        }

        #region ITopicRepository Members

        public void RemoveSubscriber(Subscriber subscriber)
        {
            var context = this.EFContext.Context.Set<Subscriber>();
            context.Remove(subscriber);
        }

        public void RemoveSubscribers(IEnumerable<Subscriber> subscribers)
        {
            var context = this.EFContext.Context.Set<Subscriber>();
            context.RemoveRange(subscribers);
        }

        #endregion
    }
}
