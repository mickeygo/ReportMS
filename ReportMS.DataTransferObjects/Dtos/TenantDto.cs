using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 租户 DTO 对象
    /// </summary>
    [DataContract]
    public class TenantDto
    {
        /// <summary>
        /// 获取或设置租户 ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取租户名
        /// </summary>
        [DataMember]
        public string TenantName { get; set; }

        /// <summary>
        /// 获取租户显示名
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取租户的描述信息
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取创建人
        /// </summary>
        [DataMember]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 获取更新人
        /// </summary>
        [DataMember]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
    }
}
