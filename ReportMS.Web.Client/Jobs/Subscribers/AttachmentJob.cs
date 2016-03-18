using System.Linq;
using Gear.Utility.Schedule;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.Jobs.JobHandlers;

namespace ReportMS.Web.Client.Jobs.Subscribers
{
    /// <summary>
    /// 附件订阅器, 执行频率为 每一小时 00 时分执行
    /// </summary>
    public class AttachmentJob : ISubScriber
    {
        #region ISubScriber Members

        public void Subscribe()
        {
            this.InnerSubscribe();
        }

        public ScheduleCronOptions Schedule
        {
            get { return new ScheduleCronOptions(0); }
        }

        #endregion

        #region Private Methods

        private void InnerSubscribe()
        {
            // Find all attachment topics, then filter the matched these.
            var attachmentTopics = TopicCacheManager.Instance.GetAttachmentTopicCache();
            if (attachmentTopics == null || !attachmentTopics.Any())
                return;

            var calibration = new JobTaskCalibration(attachmentTopics);
            var topics = calibration.FilterTopics;
            if (!topics.Any())
                return;

            // Check the report is enabled, and get the report information by the report id.
            // Execute the sqlstatement and it's paramters.
            foreach (var topic in topics)
            {
                IJobHandler job = new AttachmentJobHandler((AttachmentTopicDto) topic.Key, topic.Value);
                job.Execute();
            }
        }

        #endregion
    }
}
