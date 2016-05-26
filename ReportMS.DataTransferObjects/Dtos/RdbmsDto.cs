using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    [DataContract]
    public class RdbmsDto
    {
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置关系型数据库名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置相关描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置服务器实例（数据源）
        /// </summary>
        [DataMember]
        public string Server { get; set; }

        /// <summary>
        /// 获取或设置数据库实例
        /// </summary>
        [DataMember]
        public string Catalog { get; set; }

        /// <summary>
        /// 获取或设置用户（会加密）
        /// </summary>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置用户密码（会加密）
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示数据库是否是只读
        /// </summary>
        [DataMember]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 获取或设置数据库提供者（SqlServer 或 Oracle）
        /// </summary>
        [DataMember]
        public string Provider { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="System.Boolean"/>值，表示是否此数据有效
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置此记录创建时间
        /// </summary>
        [DataMember]
        public DateTime? CreatedDate { get; set; }
    }
}
