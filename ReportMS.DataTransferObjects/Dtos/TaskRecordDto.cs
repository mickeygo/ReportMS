using System;
using System.Runtime.Serialization;

namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 执行任务记录 Dto
    /// </summary>
    [DataContract]
    public class TaskRecordDto
    {
        /// <summary>
        /// 获取或设置执行任务记录 Id
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置主题任务 Id.
        /// 注：没有导航关系，可以不存在 TopicTask
        /// </summary>
        [DataMember]
        public Guid TopicTaskId { get; set; }

        /// <summary>
        /// 获取或设置任务执行的开始时间
        /// </summary>
        [DataMember]
        public DateTime ExecuteStartTime { get; set; }

        /// <summary>
        /// 获取或设置任务执行的结束时间
        /// </summary>
        [DataMember]
        public DateTime ExecuteEndTime { get; set; }

        /// <summary>
        /// 获取或设置执行任务的主机
        /// </summary>
        [DataMember]
        public string HostName { get; set; }

        /// <summary>
        /// 获取或设置任务的执行结果. True 表示执行成功；否则为 False 
        /// </summary>
        [DataMember]
        public bool ExecutedResult { get; set; }

        /// <summary>
        /// 获取或设置任务执行出错消息，若执行成功，则为 null.
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
