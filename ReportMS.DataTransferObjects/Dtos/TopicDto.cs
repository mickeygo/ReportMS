using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    [DataContract]
    public class TopicDto
    {
        /// <summary>
        /// 获取或设置主题 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置主题
        /// </summary>
        [DataMember]
        public string TopicName { get; set; }

        /// <summary>
        /// 获取或设置主题描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置订阅邮件的主题
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// 获取或订阅邮件的主体内容
        /// </summary>
        [DataMember]
        public string Body { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示主题是否可用
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置创建人
        /// </summary>
        [DataMember]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 获取或设置更新人
        /// </summary>
        [DataMember]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// 获取或设置主题任务
        /// </summary>
        [DataMember]
        public ICollection<TopicTaskDto> TopicTasks { get; set; }

        /// <summary>
        /// 获取或设置订阅者信息
        /// </summary>
        [DataMember]
        public ICollection<SubscriberDto> Subscribers { get; set; }
    }
}
