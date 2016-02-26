using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 报表配置字段 Dto 对象
    /// </summary>
    [DataContract]
    public class ReportProfileFieldDto
    {
        /// <summary>
        /// 获取或设置报表配置字段 Id
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置报表配置 Id
        /// </summary>
        public Guid ReportProfileId { get; private set; }

        /// <summary>
        /// 获取或设置字段名
        /// </summary>
        public string FieldName { get; private set; }
    }
}
