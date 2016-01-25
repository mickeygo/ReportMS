using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 用户角色 Dto 对象
    /// </summary>
    [DataContract]
    public class UserRoleDto
    {
        /// <summary>
        /// 获取或设置用户角色 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置用户 ID
        /// </summary>
        [DataMember]
        public Guid UserId { get; set; }

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
