using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// 表示实现此接口的类为订阅器
    /// </summary>
    public interface ISubScriber
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        void Subscribe();

        /// <summary>
        /// 执行计划
        /// </summary>
        ScheduleCronOptions Schedule { get; }
    }
}
