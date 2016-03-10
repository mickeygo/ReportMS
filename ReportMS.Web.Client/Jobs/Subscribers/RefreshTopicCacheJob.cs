using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs.Subscribers
{
    /// <summary>
    /// 更新主题缓存 Job, 每天 01:30 执行
    /// </summary>
    public class RefreshTopicCacheJob : ISubScriber
    {
        #region ISubScriber Members

        public void Subscribe()
        {
            TopicCacheManager.Instance.RefreshAttachmentTopicCache();
        }

        public ScheduleCronOptions Schedule
        {
            get { return new ScheduleCronOptions(1, 30); }
        }

        #endregion
    }
}
