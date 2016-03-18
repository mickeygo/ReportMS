using System;
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
        /// 移除订阅者。
        /// 在 Commit / Dispose 时会提交数据更新
        /// </summary>
        /// <param name="topicId">要移除的订阅者的主题 Id</param>
        /// <param name="subscriberId">要移除的订阅者 Id</param>
        /// <param name="disableTopic">是否在不存在订阅者时，禁用此主题。默认为 false</param>
        void RemoveSubscriber(Guid topicId, Guid subscriberId, bool disableTopic = false);

        /// <summary>
        /// 移除订阅者。
        /// 在 Commit / Dispose 时会提交数据更新
        /// </summary>
        /// <param name="topicId">要移除的订阅者的主题 Id</param>
        /// <param name="email">要移除的订阅者 email</param>
        /// <param name="disableTopic">是否在不存在订阅者时，禁用此主题。默认为 false</param>
        void RemoveSubscriber(Guid topicId, string email, bool disableTopic = false);
    }
}
