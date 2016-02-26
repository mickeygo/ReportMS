using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 报表分组详细信息
    /// </summary>
    [DataContract]
    public class ReportGroupItemDto
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置报表分组的 ID
        /// </summary>
        [DataMember]
        public Guid ReportGroupId { get; set; }

        /// <summary>
        /// 获取或设置报表配置 Id
        /// </summary>
        [DataMember]
        public Guid ReportProfileId { get; set; }

        /// <summary>
        /// 获取或设置报表配置
        /// </summary>
        [DataMember]
        public ReportProfileDto ReportProfile { get; set; }
    }
}
