using Gear.Utility.Schedule;
using ReportMS.Web.Client.Jobs.JobHandlers;

namespace ReportMS.Web.Client.Jobs.Subscribers
{
    /// <summary>
    /// 更新主题缓存 Job, 每天 01:30 执行
    /// </summary>
    public class RefreshTopicCacheJob : JobSubScriber
    {
        #region ISubScriber Members

        /// <summary>
        /// 每天 1:30
        /// </summary>
        public override ScheduleCronOptions Schedule
        {
            get { return new ScheduleCronOptions(1, 30); }
        }

        public override void Handle()
        {
            IJobHandler handler = new RefreshTopicCacheJobHandler();
            handler.Execute();
        }

        #endregion
    }
}
