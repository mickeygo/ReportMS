using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 报表字段 DTO 对象
    /// </summary>
    [DataContract]
    public class ReportFieldDto
    {
        /// <summary>
        /// 获取或设置 ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置报表 ID
        /// </summary>
        [DataMember]
        public Guid ReportId { get; set; }

        /// <summary>
        /// 获取或设置字段名
        /// </summary>
        [DataMember]
        public string FieldName { get; set; }

        /// <summary>
        /// 获取或设置字段显示名
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置字段的数据类型
        /// </summary>
        [DataMember]
        public string DataType { get; set; }

        /// <summary>
        /// 获取或设置字段排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }

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
    }
}
