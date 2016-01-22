using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// Action 角色 Dto
    /// </summary>
    [ServiceContract]
    public class ActionRoleDto
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置 Action Id
        /// </summary>
        [DataMember]
        public Guid ActionsId { get; set; }

        /// <summary>
        /// 获取或设置角色 ID
        /// </summary>
        [DataMember]
        public Guid RoleId { get; set; }

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
    }
}
