using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 菜单 Dto
    /// </summary>
    [DataContract]
    public class MenuDto
    {
        /// <summary>
        /// 获取或设置或设置菜单 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置菜单名
        /// </summary>
        [DataMember]
        public string MenuName { get; set; }

        /// <summary>
        /// 获取或设置菜单显示名
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置菜单描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置父级菜单 ID
        /// </summary>
        [DataMember]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 获取或设置菜单目录级别
        /// </summary>
        [DataMember]
        public int Level { get; set; }

        /// <summary>
        /// 获取或设置菜单的排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }

        /// <summary>
        /// 获取或设置功能行为 ID
        /// </summary>
        [DataMember]
        public Guid? ActionsId { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示此菜单是否可用
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
