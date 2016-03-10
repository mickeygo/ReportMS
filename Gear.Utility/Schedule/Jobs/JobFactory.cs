using Gear.Utility.Schedule.Jobs.Implements;

namespace Gear.Utility.Schedule.Jobs
{
    /// <summary>
    /// Job 工厂
    /// </summary>
    public static class JobFactory
    {
        /// <summary>
        /// 默认的 Job, 基于 Hangfire 框架
        /// </summary>
        public static IJob Defalut
        {
            get { return new HangfireJob(); }
        }
    }
}
