using System.Collections.Generic;
using Gear.Infrastructure;
using Gear.Infrastructure.Caching;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// 主题缓存管理
    /// </summary>
    public class TopicCacheManager
    {
        #region Private Fields

        internal const string TopicCache = "__topic.cache";
        internal const string AttachmentTopicCache = "__topic.cache.attchment";

        private static readonly TopicCacheManager _instance = new TopicCacheManager();

        #endregion

        #region Properties

        /// <summary>
        /// 获取主题缓存管理实例
        /// </summary>
        public static TopicCacheManager Instance
        {
            get { return _instance; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取附件主题缓存
        /// </summary>
        /// <returns>附件主题缓存集合</returns>
        public IEnumerable<AttachmentTopicDto> GetAttachmentTopicCache()
        {
            var attachmentTopics = CacheManager.Instance.Get(TopicCache, AttachmentTopicCache);
            if (attachmentTopics != null)
                return (IEnumerable<AttachmentTopicDto>) attachmentTopics;

            this.SetAttachmentTopicCache();
            return (IEnumerable<AttachmentTopicDto>) CacheManager.Instance.Get(TopicCache, AttachmentTopicCache);
        }

        /// <summary>
        /// 更新附件主题的缓存
        /// </summary>
        public void RefreshAttachmentTopicCache()
        {
            this.SetAttachmentTopicCache();
        }

        #endregion

        #region Private Methods

        private void SetAttachmentTopicCache()
        {
            var attachmentTopics = this.GetAttachmentTopicCacheFromStore();
            CacheManager.Instance.Put(TopicCache, AttachmentTopicCache, attachmentTopics);
        }

        private IEnumerable<AttachmentTopicDto> GetAttachmentTopicCacheFromStore()
        {
            using (var service = ServiceLocator.Instance.Resolve<ISubscriberService>())
            {
                return service.FindAttachmentTopics();
            }
        }

        #endregion
    }
}
