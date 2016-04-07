namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// Job 开关.控制 Job 的暂停与运行
    /// </summary>
    public static class JobSwitch
    {
        #region Fields

        private static bool jobStatus;
        private static readonly object sync = new object();

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 获取 Job 当前状态。
        /// true 表示正在运行; false 表示已经暂停
        /// </summary>
        /// <returns>true 表示运行; false 表示暂停</returns>
        public static bool GetStatus()
        {
            return jobStatus;
        }

        /// <summary>
        /// Job 暂停
        /// </summary>
        public static void Pause()
        {
            lock (sync)
            {
                jobStatus = false;
            }
        }

        /// <summary>
        /// Job 运行
        /// </summary>
        public static void Run()
        {
            lock (sync)
            {
                jobStatus = true;
            }
        }

        #endregion
    }
}
