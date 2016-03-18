using System;
using System.Linq;
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

        public void RemoveSubscriber(Guid topicId, Guid subscriberId, bool disableTopic = false)
        {
            var topic = this.GetByKey(topicId);
            if (topic == null)
                return;

            var subscriber = topic.Subscribers.SingleOrDefault(s => s.ID == subscriberId);
            if (subscriber == null)
                return;

            if (disableTopic)
            {
                if (topic.Subscribers.Count == 1)
                    topic.Disable();
            }

            var context = this.EFContext.Context.Set<Subscriber>();
            context.Remove(subscriber);

            this.Update(topic);
        }

        public void RemoveSubscriber(Guid topicId, string email, bool disableTopic = false)
        {
            var topic = this.GetByKey(topicId);
            if (topic == null)
                return;

            var subscribers = topic.Subscribers.Where(s => s.Email.Equals(email)).ToList();
            if (!subscribers.Any())
                return;

            if (disableTopic)
            {
                if (topic.Subscribers.Count == subscribers.Count)
                    topic.Disable();
            }

            var context = this.EFContext.Context.Set<Subscriber>();
            context.RemoveRange(subscribers);

            this.Update(topic);
        }

        #endregion
    }
}
