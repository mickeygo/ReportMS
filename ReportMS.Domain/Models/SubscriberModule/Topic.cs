using System;
using System.Collections.Generic;
using Gear.Infrastructure;
using Gear.Infrastructure.Repositories;

namespace ReportMS.Domain.Models.SubscriberModule
{
    /// <summary>
    /// 主题基类，继承此类的都是聚合根对象
    /// </summary>
    public class Topic : AggregateRoot, ISoftDelete
    {
        #region Public Properties

        /// <summary>
        /// 获取主题
        /// </summary>
        public string TopicName { get; protected set; }

        /// <summary>
        /// 获取主题描述
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// 订阅邮件的主题
        /// </summary>
        public string Subject { get; protected set; }

        /// <summary>
        /// 订阅邮件的主体内容
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// 获取创建人
        /// </summary>
        public string CreatedBy { get; protected set; }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        public DateTime? CreatedDate { get; protected set; }

        /// <summary>
        /// 获取更新人
        /// </summary>
        public string UpdatedBy { get; protected set; }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        public DateTime? UpdatedDate { get; protected set; }

        /// <summary>
        /// 获取主题任务
        /// </summary>
        public virtual ICollection<TopicTask> TopicTasks { get; protected set; }

        /// <summary>
        /// 获取订阅者信息
        /// </summary>
        public virtual ICollection<Subscriber> Subscribers { get; protected set; }

        #endregion

        #region ISoftDelete Members

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示主题是否可用
        /// </summary>
        public bool Enabled { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 启用主题
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
                this.Enabled = true;
        }

        /// <summary>
        /// 禁用主题
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
                this.Enabled = false;
        }

        /// <summary>
        /// 设置更新人信息
        /// </summary>
        /// <param name="updatedBy">要设置的更新人</param>
        public void SetUpdatedBy(string updatedBy)
        {
            this.UpdatedBy = updatedBy;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 添加主题任务
        /// </summary>
        /// <param name="tasks">要添加的主题任务集合</param>
        public void AddTopicTasks(params TopicTask[] tasks)
        {
            if (this.TopicTasks == null)
                this.TopicTasks = new HashSet<TopicTask>();

            foreach (var task in tasks)
                this.TopicTasks.Add(task);
        }

        /// <summary>
        /// 添加订阅人
        /// </summary>
        /// <param name="subscribers">要添加的订阅人集合</param>
        public void AddSubscribers(params Subscriber[] subscribers)
        {
            if (this.Subscribers == null)
                this.Subscribers = new HashSet<Subscriber>();

            foreach (var subscriber in subscribers)
                this.Subscribers.Add(subscriber);
        }

        #endregion
    }
}
