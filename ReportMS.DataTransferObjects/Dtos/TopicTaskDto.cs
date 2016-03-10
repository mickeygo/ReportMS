using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 执行任务 Dto
    /// </summary>
    [DataContract]
    public class TopicTaskDto
    {
        /// <summary>
        /// 获取或设置主题执行任务 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置主题 Id
        /// </summary>
        [DataMember]
        public Guid TopicId { get; set; }

        /// <summary>
        /// 获取主题
        /// </summary>
        [DataMember]
        public TopicDto Topic { get; private set; }

        /// <summary>
        /// 获取或设置一个<c>TaskSchedule</c>，表示任务的执行计划
        /// </summary>
        [DataMember]
        public TaskScheduleDto TaskSchedule { get; set; }

        /// <summary>
        /// 获取或设置 Task 执行计划的分钟数 [0-59]
        /// </summary>
        [DataMember]
        public int? Minute { get; set; }

        /// <summary>
        /// 获取或设置 Task 执行计划的小时数 [0-23]
        /// </summary>
        [DataMember]
        public int? Hour { get; set; }

        /// <summary>
        /// 获取或设置 Task 执行计划的天数 [1-31]
        /// </summary>
        [DataMember]
        public int? Day { get; set; }

        /// <summary>
        /// 获取或设置 Task 执行计划的周数 [0-6]
        /// </summary>
        [DataMember]
        public DayOfWeek? Week { get; set; }

        /// <summary>
        /// 获取或设置 Task 执行计划的月数 [1-12]
        /// </summary>
        [DataMember]
        public int? Month { get; set; }
    }
}
