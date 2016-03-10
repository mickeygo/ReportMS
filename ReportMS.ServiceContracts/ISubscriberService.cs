using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 订阅器服务
    /// </summary>
    [ServiceContract]
    public interface ISubscriberService : IApplicationService
    {
        /// <summary>
        /// 查找所有的有效的附件主题
        /// </summary>
        /// <returns><c>AttachmentTopicDto</c>附件主题 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<AttachmentTopicDto> FindAttachmentTopics();

        /// <summary>
        /// 创建附件主题, 可以包括执行任务和订阅人
        /// </summary>
        /// <param name="topicDto">附件主题 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void CreateAttachmentTopic(AttachmentTopicDto topicDto);

        /// <summary>
        /// 任务执行的记录
        /// </summary>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void LogTaskRecord(TaskRecordDto taskRecord);
    }
}
