using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// Action Dto 
    /// </summary>
    [DataContract]
    public class ActionsDto
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置区域（MVC）
        /// </summary>
        [DataMember]
        public string Area { get; set; }

        /// <summary>
        /// 获取或设置控制器（MVC）
        /// </summary>
        [DataMember]
        public string Controller { get; set; }

        /// <summary>
        /// 获取或设置 Action（MVC）
        /// </summary>
        [DataMember]
        public string Action { get; set; }

        /// <summary>
        /// 获取或设置或设置描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示此 Action 是否可用
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置或设置创建人
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
