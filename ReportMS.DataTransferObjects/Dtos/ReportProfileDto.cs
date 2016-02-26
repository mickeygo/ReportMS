using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 报表分组明细项 Dto
    /// </summary>
    [DataContract]
    public class ReportProfileDto
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置报表配置名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置报表配置名称描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置报表 ID
        /// </summary>
        [DataMember]
        public Guid ReportId { get; set; }

        /// <summary>
        /// 获取或设置报表
        /// </summary>
        [DataMember]
        public ReportDto Report { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值,表示报表组是否可用
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
        /// 获取或设置报表配置字段集合信息
        /// </summary>
        [DataMember]
        public ICollection<ReportProfileFieldDto> ReportProfileFields { get; set; }
    }
}
