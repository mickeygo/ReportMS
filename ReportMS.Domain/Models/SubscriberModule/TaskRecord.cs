using System;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.SubscriberModule
{
    /// <summary>
    /// 任务记录聚合根
    /// </summary>
    public class TaskRecord : AggregateRoot
    {
        #region Public Properties

        /// <summary>
        /// 获取主题任务 Id.
        /// 注：没有导航关系，可以不存在 TopicTask
        /// </summary>
        public Guid TopicTaskId { get; private set; }

        /// <summary>
        /// 获取执行任务的开始时间
        /// </summary>
        public DateTime ExecuteStartTime { get; private set; }

        /// <summary>
        /// 获取执行任务的结束时间
        /// </summary>
        public DateTime ExecuteEndTime { get; private set; }

        /// <summary>
        /// 获取执行任务的主机名
        /// </summary>
        public string HostName { get; private set; }

        /// <summary>
        /// 获取任务的执行结果. True 表示执行成功；否则为 False 
        /// </summary>
        public bool ExecutedResult { get; private set; }

        /// <summary>
        /// 获取任务执行出错消息，若执行成功，则为 null.
        /// </summary>
        public string ErrorMessage { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>TaskRecord</c>实例。仅供 EntityFramework 调用
        /// </summary>
        public TaskRecord()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>TaskRecord</c>实例
        /// </summary>
        /// <param name="topicTaskId">主题任务 Id</param>
        /// <param name="execStartTime">执行任务的开始时间</param>
        /// <param name="execEndTime">执行任务的结束时间</param>
        /// <param name="hostName">执行任务的主机名</param>
        /// <param name="executedResult">执行结果. True 表示执行成功；否则为 False </param>
        /// <param name="errorMessage">执行出错消息</param>
        public TaskRecord(Guid topicTaskId, DateTime execStartTime, DateTime execEndTime, string hostName,
            bool executedResult, string errorMessage = null)
        {
            TopicTaskId = topicTaskId;
            ExecuteStartTime = execStartTime;
            ExecuteEndTime = execEndTime;
            HostName = hostName;
            ExecutedResult = executedResult;
            ErrorMessage = errorMessage;

            this.GenerateNewIdentity();
        }

        #endregion
    }
}
