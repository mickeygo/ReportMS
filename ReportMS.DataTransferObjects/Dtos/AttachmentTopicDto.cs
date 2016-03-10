using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 附件主题 Dto 对象
    /// </summary>
    [DataContract]
    public class AttachmentTopicDto : TopicDto
    {
        /// <summary>
        /// 获取或设置报表 Id
        /// </summary>
        [DataMember]
        public Guid ReportId { get; set; }

        /// <summary>
        /// 获取或设置 Sql 语句
        /// </summary>
        [DataMember]
        public string SqlStatement { get; set; }

        /// <summary>
        /// 获取或设置 Sql 参数
        /// </summary>
        [DataMember]
        public string Parameter { get; set; }
    }
}
