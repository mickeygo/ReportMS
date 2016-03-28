using ReportMS.Web.Client.Jobs.Subscribers;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// Job 容器
    /// </summary>
    partial class JobClient
    {
        /// <summary>
        /// 容器, 装入要执行的 Job
        /// </summary>
        private void Container()
        {
            JobClient.Register<AttachmentJob>();
            JobClient.Register<RefreshTopicCacheJob>();
        }
    }
}
