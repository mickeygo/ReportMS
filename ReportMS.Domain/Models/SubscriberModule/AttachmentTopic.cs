using System;

namespace ReportMS.Domain.Models.SubscriberModule
{
    /// <summary>
    /// 附件主题聚合根
    /// </summary>
    public class AttachmentTopic : Topic
    {
        #region Public Properties

        /// <summary>
        /// 获取报表 Id
        /// </summary>
        public Guid ReportId { get; private set; }

        /// <summary>
        /// 获取 Sql 语句
        /// </summary>
        public string SqlStatement { get; private set; }

        /// <summary>
        /// 获取 Sql 参数
        /// </summary>
        public string Parameter { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>AttachmentTopic</c>对象。仅供 EntityFramework 调用
        /// </summary>
        public AttachmentTopic()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>AttachmentTopic</c>对象
        /// </summary>
        /// <param name="topicName">附件主题名</param>
        /// <param name="description">主题描述</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主体</param>
        /// <param name="reportId">报表 Id</param>
        /// <param name="sqlStatement">Sql 语句</param>
        /// <param name="parameter">参数，以 @p1=val1&@p2=val2 表示</param>
        /// <param name="createdBy">创建人</param>

        public AttachmentTopic(string topicName, string description, string subject, string body, Guid reportId, string sqlStatement,
           string parameter, string createdBy)
        {
            this.TopicName = topicName;
            this.Description = description;
            this.Subject = subject;
            this.Body = body;
            this.ReportId = reportId;
            this.SqlStatement = sqlStatement;
            this.Parameter = parameter;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.Now;

            this.GenerateNewIdentity();
            this.Enable();
        }

        #endregion
    }
}
