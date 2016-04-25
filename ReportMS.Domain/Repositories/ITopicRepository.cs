using System.Collections.Generic;
using Gear.Infrastructure.Repositories;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.Domain.Repositories
{
    /// <summary>
    /// 表示实现接口的类为主题仓储
    /// </summary>
    public interface ITopicRepository : IRepository<Topic>
    {
        /// <summary>
        /// 移除订阅者
        /// </summary>
        /// <param name="subscriber">要移除的订阅者</param>
        void RemoveSubscriber(Subscriber subscriber);

        /// <summary>
        /// 移除订阅者集合
        /// </summary>
        /// <param name="subscribers">要移除的订阅者集</param>
        void RemoveSubscribers(IEnumerable<Subscriber> subscribers);
    }
}
