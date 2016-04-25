using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为订阅主题管理类.
    /// 此主题可作为其他主题的基类
    /// </summary>
    [ServiceContract]
    public interface ITopicService : IApplicationService
    {
        /// <summary>
        /// 查找所有有效的订阅主题
        /// </summary>
        /// <returns>主题 Dto 对象集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<TopicDto> FindAll();

        /// <summary>
        /// 查找订阅主题
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <returns>主题 Dto 对象</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        TopicDto Find(Guid topicId);

        /// <summary>
        /// 通过主题的拥有者（创建人）来查找订阅主题
        /// </summary>
        /// <param name="owner">拥有者</param>
        /// <returns>主题 Dto 对象集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<TopicDto> FindTopicsViaOwner(string owner);

        /// <summary>
        /// 通过指定的订阅邮件来查找主题
        /// </summary>
        /// <param name="email">要查找的邮件</param>
        /// <returns>主题 Dto 对象集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<TopicDto> FindTopicsViaEmail(string email);

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="topicDto">要修改的主题 Dto 对象</param>
        /// <param name="includeSubscriber">是否也会修改订阅者，默认为 true</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void ModifyTopic(TopicDto topicDto, bool includeSubscriber = true);

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
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void DeleteSubscriber(Guid topicId, Guid subscriberId);

        /// <summary>
        /// 删除订阅者
        /// </summary>
        /// <param name="topicId">要删除的订阅者所在的主题 Id</param>
        /// <param name="email">要删除的订阅者email</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void DeleteSubscriber(Guid topicId, string email);

        /// <summary>
        /// 任务执行的记录
        /// </summary>
        /// <param name="taskRecord">任务记录 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void LogTaskRecord(TaskRecordDto taskRecord);
    }
}
