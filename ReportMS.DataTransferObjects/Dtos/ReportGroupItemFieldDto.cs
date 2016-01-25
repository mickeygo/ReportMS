using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 报表分组项明细报表中的字段 Dto
    /// </summary>
    [DataContract]
    public class ReportGroupItemFieldDto
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置报表分组 ID
        /// </summary>
        [DataMember]
        public Guid ReportGroupItemId { get; set; }

        /// <summary>
        /// 获取或设置字段名称
        /// </summary>
        [DataMember]
        public string FieldName { get; set; }
    }
}
