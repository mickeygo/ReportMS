using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// Job 订阅者基类
    /// </summary>
    public abstract class JobSubScriber : ISubScriber
    {
        #region ISubScriber Members

        /// <summary>
        /// 订阅 Job
        /// </summary>
        public void Subscribe()
        {
            if (JobSwitch.GetStatus())
            {
                this.Handle();
            }
        }

        public abstract ScheduleCronOptions Schedule { get; }

        #endregion

        /// <summary>
        /// Job 执行的具体内容
        /// </summary>
        public abstract void Handle();

    }
}
