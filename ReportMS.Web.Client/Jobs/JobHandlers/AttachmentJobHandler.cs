using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Gear.Infrastructure;
using Gear.Infrastructure.Net.Mail;
using Gear.Infrastructure.Storage;
using Gear.Utility.IO.Excels;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Helpers;

namespace ReportMS.Web.Client.Jobs.JobHandlers
{
    /// <summary>
    /// 附件 Job 处理
    /// </summary>
    public class AttachmentJobHandler
    {
        private readonly AttachmentTopicDto _attachmentTopic;
        private readonly IEnumerable<TopicTaskDto> _topicTasks;
        private string errorMessage;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>AttachmentJobHandler</c>实例
        /// </summary>
        /// <param name="attachmentTopic">附件主题对象</param>
        public AttachmentJobHandler(AttachmentTopicDto attachmentTopic)
        {
            this._attachmentTopic = attachmentTopic;
        }

        /// <summary>
        /// 初始化一个新的<c>AttachmentJobHandler</c>实例
        /// </summary>
        /// <param name="attachmentTopic">附件主题对象</param>
        /// <param name="topicTasks">此时刻该附件正执行的任务集合</param>
        public AttachmentJobHandler(AttachmentTopicDto attachmentTopic, IEnumerable<TopicTaskDto> topicTasks)
        {
            this._attachmentTopic = attachmentTopic;
            this._topicTasks = topicTasks;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 执行 Job
        /// </summary>
        public void Execute()
        {
            var stream = this.GenerateAttachment();
            var status = this.SendMail(stream);

            var taskIds = this._topicTasks.Select(t => t.ID);
            this.Log(taskIds, status, this.errorMessage);
        }

        #endregion

        #region Private Methods

        private Stream GenerateAttachment()
        {
            var stream = new MemoryStream();
            try
            {
                var dataReader = GetDataReaderOfAttachment();
                var sheetname = this._attachmentTopic.TopicName;
                ExcelFactory.Create(sheetname, dataReader).SaveAsStream(stream);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                stream.Close();
            }
            return stream;
        }

        // 邮件是否发送成功
        private bool SendMail(Stream stream)
        {
            if (stream == null)
            {
                this.errorMessage = "Generate attachment stream failure.";
                return false;
            }

            var fileName = this._attachmentTopic.TopicName + DateTime.Now.ToString("yyMMddHHmmss") + ".xlsx";
            var attachmemts = new List<Tuple<Stream, string>> {Tuple.Create(stream, fileName)};
            var subject = this._attachmentTopic.TopicName;
            var body = this._attachmentTopic.Description;
            var mailTos = (from subscriber in this._attachmentTopic.Subscribers
                select subscriber.Email).ToArray();

            try
            {
                var manager = new MailManager(subject, body, mailTos, attachmemts);
                manager.Send();
            }
            catch (Exception)
            {
                stream.Close();
                this.errorMessage = "Send mail failure.";
                return false;
            }

            return true;
        }

        // 批量执行, 状态和消息都相同
        private void Log(IEnumerable<Guid> taskRecordIds, bool status, string errorMsg)
        {
            var taskRecords = (from taskRecordId in taskRecordIds
                select new TaskRecordDto
                {
                    TopicTaskId = taskRecordId,
                    ExecuteResult = status,
                    ErrorMessage = errorMsg
                });

            using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
            {
                foreach (var taskRecord in taskRecords)
                    service.LogTaskRecord(taskRecord);
            }
        }

        private IDataReader GetDataReaderOfAttachment()
        {
            var report = this.GetReport(this._attachmentTopic.ReportId);
            if (report == null)
            {
                this.errorMessage = "The report does not find.";
                return null;
            }

            var connect = report.Database;
            var sqlStatement = this._attachmentTopic.SqlStatement;
            var parameters = this._attachmentTopic.Parameter;

            if (String.IsNullOrWhiteSpace(parameters))
                return StorageManager.CreateInstance(connect).GetDataReader(sqlStatement);

            var parms = StorageParameter.ConvertStringToParameter(parameters);
            return StorageManager.CreateInstance(connect).GetDataReader(sqlStatement, parms);
        }

        private ReportDto GetReport(Guid reportId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportService>())
            {
                return service.FindReport(reportId);
            }
        }

        #endregion
    }
}
