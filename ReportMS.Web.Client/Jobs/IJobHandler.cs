namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// 表示实现此接口的类为 Job 处理者
    /// </summary>
    public interface IJobHandler
    {
        /// <summary>
        /// 执行 Job
        /// </summary>
        void Execute();
    }
}
