namespace ReportMS.Web.Client.Jobs.JobHandlers
{
    /// <summary>
    /// 刷新主题缓存的 Job 处理器
    /// </summary>
    public class RefreshTopicCacheJobHandler : IJobHandler
    {
        #region IJobHandler Members

        public void Execute()
        {
            // attachment topic cache handle.
            TopicCacheManager.Instance.RefreshAttachmentTopicCache();

            // other topic cache handle.
        }

        #endregion
    }
}
