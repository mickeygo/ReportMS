using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Gear.Infrastructure;
using Gear.Infrastructure.Net.Dns;
using Gear.Infrastructure.Net.Mail;
using Gear.Utility.IO;
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

            using (var service = ServiceLocator.Instance.Resolve<ITopicService>())
            {
                foreach (var taskRecord in taskRecords)
                    service.LogTaskRecord(taskRecord);
            }
        }

        /// <summary>
        /// 建制 Mail
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="stream">附件流</param>
        /// <returns>mail 管理者</returns>
        protected virtual MailManager BuildMail(TopicDto topic, Stream stream = null)
        {
            this.OnTopicBuilding(topic);

            var fileName = FilePathBuilder.BuildPath(
                string.Format("{0}-{1}", topic.TopicName, DateTime.Now.ToString("yyMMddHHmmss")),
                FileExtension.Excel2007);

            var subject = topic.Subject;
            var body = topic.Body;
            var mailTos = (from subscriber in topic.Subscribers
                select subscriber.Email).ToArray();

            if (stream != null)
            {
                var attachmemts = new List<Tuple<Stream, string>> {Tuple.Create(stream, fileName)};
                return new MailManager(subject, body, mailTos, attachmemts);
            }

            return new MailManager(subject, body, mailTos);
        }

        /// <summary>
        /// 建制 Topic, 发送邮件之前触发
        /// </summary>
        /// <param name="topic">主题</param>
        protected virtual void OnTopicBuilding(TopicDto topic)
        {
            topic.Body = HttpUtility.HtmlDecode(topic.Body);
        }
    }
}
