using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
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

        /// <summary>
        /// 通过指定的订阅邮件来查找主题
        /// </summary>
        /// <param name="email">要查找的邮件</param>
        /// <returns>主题 Dto 对象集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<TopicDto> FindTopicsViaEmail(string email);

        /// <summary>
        /// 移除主题
        /// </summary>
        /// <param name="topicId">要移除的主题 Id</param>
        /// <param name="handler">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemoveTopic(Guid topicId, string handler);

        /// <summary>
        /// 删除订阅者
        /// </summary>
        /// <param name="topicId">要删除的订阅者所在的主题 Id</param>
        /// <param name="subscriberId">要删除的订阅者 Id</param>
        /// <param name="disableTopic">是否在不存在订阅者时，禁用此主题。默认为 false</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void DeleteSubscriber(Guid topicId, Guid subscriberId, bool disableTopic = false);

        /// <summary>
        /// 删除订阅者
        /// </summary>
        /// <param name="topicId">要删除的订阅者所在的主题 Id</param>
        /// <param name="email">要删除的订阅者email</param>
        /// <param name="disableTopic">是否在不存在订阅者时，禁用此主题。默认为 false</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void DeleteSubscriber(Guid topicId, string email, bool disableTopic = false);

        /// <summary>
        /// 任务执行的记录
        /// </summary>
        /// <param name="taskRecord">任务记录 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void LogTaskRecord(TaskRecordDto taskRecord);
    }
}
