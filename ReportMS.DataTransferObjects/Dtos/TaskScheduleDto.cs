using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 任务执行计划 Dto 对象
    /// </summary>
    [DataContract]
    public enum TaskScheduleDto
    {
        /// <summary>
        /// 执行频率为每分钟
        /// </summary>
        [DataMember] Minutely = 0,

        /// <summary>
        /// 执行频率为每小时
        /// </summary>
        [DataMember] Hourly = 1,

        /// <summary>
        /// 执行频率为每天
        /// </summary>
        [DataMember] Daily = 2,

        /// <summary>
        /// 执行频率为每周
        /// </summary>
        [DataMember] Weekly = 3,

        /// <summary>
        /// 执行频率为每月
        /// </summary>
        [DataMember] Monthly = 4,

        /// <summary>
        /// 执行频率为每年
        /// </summary>
        [DataMember] Yearly = 5
    }
}
