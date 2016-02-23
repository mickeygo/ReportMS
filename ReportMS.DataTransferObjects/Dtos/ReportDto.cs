using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 报表 DTO 对象
    /// </summary>
    [DataContract]
    public class ReportDto
    {
        /// <summary>
        /// 获取或设置 ID.
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置报表名.
        /// 此报表名为要查询的 Table / View 名
        /// </summary>
        [DataMember]
        public string ReportName { get; set; }

        /// <summary>
        /// 获取或设置报表显示名
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置报表所在的数据库
        /// </summary>
        [DataMember]
        public string Database { get; set; }

        /// <summary>
        /// 获取或设置数据库的 Schema, 一般为 dbo
        /// </summary>
        [DataMember]
        public string Schema { get; set; }

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
        /// 获取或设置字段集合
        /// </summary>
        [DataMember]
        public virtual IEnumerable<ReportFieldDto> Fields { get; set; }
    }
}
