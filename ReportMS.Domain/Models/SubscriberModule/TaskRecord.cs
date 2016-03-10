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
        /// 获取任务执行的时间
        /// </summary>
        public DateTime ExecuteTime { get; private set; }

        /// <summary>
        /// 获取任务的执行结果. True 表示执行成功；否则为 False 
        /// </summary>
        public bool ExecuteResult { get; private set; }

        /// <summary>
        /// 获取任务执行出错消息，若执行成功，则为 null.
        /// </summary>
        public string ErrorMessage { get; private set; }

        #endregion

        #region Ctor

        public TaskRecord()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>TaskRecord</c>实例
        /// </summary>
        /// <param name="topicTaskId">主题任务 Id</param>
        /// <param name="executeResult">执行结果. True 表示执行成功；否则为 False </param>
        /// <param name="errorMessage">执行出错消息</param>
        public TaskRecord(Guid topicTaskId, bool executeResult, string errorMessage = null)
        {
            TopicTaskId = topicTaskId;
            ExecuteTime = DateTime.Now;
            ExecuteResult = executeResult;
            ErrorMessage = errorMessage;

            this.GenerateNewIdentity();
        }

        #endregion
    }
}
