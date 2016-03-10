namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// 表示实现此接口的类为 Job 定时器
    /// </summary>
    public interface IJobClient
    {
        /// <summary>
        /// 启动任务
        /// </summary>
        void Start();
    }
}
