using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为订阅器应用服务
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
        /// 查找指定的附件主题
        /// </summary>
        /// <param name="topicId">要查找的主题 Id</param>
        /// <returns>附件主题 Dto 对象</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        AttachmentTopicDto FindAttachmentTopic(Guid topicId);

        /// <summary>
        /// 创建附件主题, 可以包括执行任务和订阅人
        /// </summary>
        /// <param name="topicDto">附件主题 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void CreateAttachmentTopic(AttachmentTopicDto topicDto);
    }
}
