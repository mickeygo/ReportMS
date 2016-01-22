using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 角色 Dto 对象
    /// </summary>
    [ServiceContract]
    public class RoleDto
    {
        /// <summary>
        /// 获取或设置角色 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置角色名
        /// </summary>
        [DataMember]
        public string RoleName { get; set; }

        /// <summary>
        /// 获取或设置角色显示名称
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置角色描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置租户 ID
        /// </summary>
        [DataMember]
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示此角色是否可用
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
        /// 获取或设置更新时间
        /// </summary>
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
    }
}
