using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure;
using Gear.Infrastructure.Net.Dns;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// 表示 Job 处理者基类
    /// </summary>
    public abstract class JobHandler : IJobHandler
    {
       private readonly DateTime jobStartTime = DateTime.Now;

       public abstract void Execute();

        /// <summary>
        /// 记录文档
        /// </summary>
        /// <param name="taskIds">任务 Id</param>
        /// <param name="status">状态</param>
        /// <param name="errorMessage">错误消息</param>
        public void Log(IEnumerable<Guid> taskIds, bool status, string errorMessage)
        {
            if (taskIds == null || !taskIds.Any())
                return;

            var hostName = DnsUtility.GetLocalHostName();
            var jobEndTime = DateTime.Now;
            var taskRecords = (from taskId in taskIds
                select new TaskRecordDto
                {
                    TopicTaskId = taskId,
                    ExecuteStartTime = jobStartTime,
                    ExecuteEndTime = jobEndTime,
                    HostName = hostName,
                    ExecutedResult = status,
                    ErrorMessage = errorMessage
                });

            using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
            {
                foreach (var taskRecord in taskRecords)
                    service.LogTaskRecord(taskRecord);
            }
        }
    }
}
