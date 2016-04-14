using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Gear.Infrastructure;
using Gear.Infrastructure.Net.Mail;
using Gear.Infrastructure.Storage;
using Gear.Utility.IO;
using Gear.Utility.IO.Excels;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Helpers;

namespace ReportMS.Web.Client.Jobs.JobHandlers
{
    /// <summary>
    /// 附件 Job 处理者
    /// </summary>
    public class AttachmentJobHandler : JobHandler
    {
        private readonly AttachmentTopicDto _attachmentTopic;
        private readonly IEnumerable<TopicTaskDto> _topicTasks;
        private bool _status = true;
        private string _errorMessage;

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

        #region IJobHandler Members

        /// <summary>
        /// 执行 Job
        /// </summary>
        public override void Execute()
        {
            var stream = this.GenerateStreamOfAttachment();

            // 根据主题发送邮件（订阅人附属于主题），同一主题同一时间点只发送一次
            this.SendMail(stream);

            var taskIds = this._topicTasks.Select(t => t.ID);
            this.Log(taskIds, this._status, this._errorMessage);
        }

        #endregion

        #region Private Methods

        // 邮件是否发送成功
        private void SendMail(Stream stream)
        {
            if (stream == null)
            {
                this.SetErrorStatus("Generate attachment stream failure.");
                return;
            }

            var fileName = FilePathBuilder.BuildPath(
                    string.Format("{0}-{1}", this._attachmentTopic.TopicName, DateTime.Now.ToString("yyMMddHHmmss")),
                    FileExtension.Excel2007);
            var attachmemts = new List<Tuple<Stream, string>> {Tuple.Create(stream, fileName)};
            var subject = this._attachmentTopic.Subject;
            var body = this._attachmentTopic.Body;
            var mailTos = (from subscriber in this._attachmentTopic.Subscribers
                select subscriber.Email).ToArray();

            try
            {
                var manager = new MailManager(subject, body, mailTos, attachmemts);
                manager.Send();
            }
            catch (Exception)
            {
                // dispose the stream whether send mail successfully or failure.
                this.SetErrorStatus("Send mail failure.");
            }
        }

        private Stream GenerateStreamOfAttachment()
        {
            MemoryStream ms = null;

            try
            {
                var bytes = this.GetBytesOfAttachment();
                if (bytes == null)
                {
                    this.SetErrorStatus("Get the attachment bytes failure.");
                    return null;
                }

                ms = new MemoryStream(bytes);
            }
            catch (Exception)
            {
                if (ms != null)
                    ms.Close();

                return null;
            }

            return ms;
        }

        private byte[] GetBytesOfAttachment()
        {
            IDataReader dataReader = null;
            try
            {
                dataReader = this.GetDataReaderOfAttachment();
                var sheetname = this._attachmentTopic.TopicName;
                return ExcelFactory.Create(sheetname, dataReader).SaveAsBytes();
            }
            catch (Exception)
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();

                this.SetErrorStatus("Create the dataReader failure.");
                return null;
            }
        }

        private IDataReader GetDataReaderOfAttachment()
        {
            var report = this.GetReport(this._attachmentTopic.ReportId);
            if (report == null)
            {
                this.SetErrorStatus("The report does not find.");
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

        private void SetErrorStatus(string errorMsg)
        {
            if (this._status)
                this._status = false;

            // Only set the original error information.
            if (this._errorMessage == null)
                this._errorMessage = errorMsg;
        }

        #endregion
    }
}
