namespace ReportMS.Web.Client.Jobs.JobHandlers
{
    /// <summary>
    /// 刷新主题缓存的 Job 处理器。
    /// 此类中设置从缓存中提取的数据。
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
