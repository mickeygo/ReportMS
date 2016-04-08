using System.Linq;
using Gear.Utility.Schedule;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.Jobs.JobHandlers;

namespace ReportMS.Web.Client.Jobs.Subscribers
{
    /// <summary>
    /// 附件订阅器, 执行频率为 每一小时 00 时分执行
    /// </summary>
    public class AttachmentJob : JobSubScriber
    {
        #region ISubScriber Members

        /// <summary>
        /// 执行频率为每小时。
        /// 每小时(00 分钟)检查是否有订阅在此时发送附件的数据
        /// </summary>
        public override ScheduleCronOptions Schedule
        {
            get { return new ScheduleCronOptions(0); }
        }

        public override void Handle()
        {
            // Find all attachment topics, then filter the matched these.
            // 若设置从缓存中提取的数据，需要在 RefreshTopicCacheJobHandler 中重新设定
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
