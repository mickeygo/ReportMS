using System;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 订阅人 Dto 对象
    /// </summary>
    public class SubscriberDto
    {
        /// <summary>
        /// 获取或设置订阅器 Id
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置主题 Id
        /// </summary>
        public Guid TopicId { get; set; }

        /// <summary>
        /// 获取或设置订阅者邮件
        /// </summary>
        public string Email { get; set; }
    }
}
