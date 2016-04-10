using System;
using System.Collections.Generic;
using System.Linq;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.SubscriberModule;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 订阅器应用服务
    /// </summary>
    public class SubscriberService : ISubscriberService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IAttachmentTopicRepository _attachmentTopicRepository;
        private readonly ITaskRecordRepository _taskRecordRepository;
        
        #region Ctor

        public SubscriberService(ITopicRepository topicRepository,
            IAttachmentTopicRepository attachmentTopicRepository,
            ITaskRecordRepository taskRecordRepository)
        {
            _topicRepository = topicRepository;
            _attachmentTopicRepository = attachmentTopicRepository;
            _taskRecordRepository = taskRecordRepository;
        }

        #endregion

        #region ISubscriberService Members

        public IEnumerable<AttachmentTopicDto> FindAttachmentTopics()
        {
            return this._attachmentTopicRepository.FindAll().ToList().MapAs<AttachmentTopicDto>();
        }

        public AttachmentTopicDto FindAttachmentTopic(Guid topicId)
        {
            return this._attachmentTopicRepository.GetByKey(topicId).MapAs<AttachmentTopicDto>();
        }

        public void CreateAttachmentTopic(AttachmentTopicDto topicDto)
        {
            var topic = new AttachmentTopic(topicDto.TopicName, topicDto.Description, topicDto.ReportId,
                topicDto.SqlStatement, topicDto.Parameter, topicDto.CreatedBy);
            var taskDtos = topicDto.TopicTasks;
            if (taskDtos != null)
            {
                var tasks = (from task in taskDtos
                     select new TopicTask(topic.ID, (TaskSchedule) task.TaskSchedule, task.Month, task.Week, task.Day, task.Hour));
                topic.AddTopicTasks(tasks.ToArray());
            }

            var subscribers = topicDto.Subscribers;
            if (subscribers != null)
            {
                var taskSubcribers = (from subscriber in subscribers
                    select new Subscriber(topic.ID, subscriber.Email));
                topic.AddSubscribers(taskSubcribers.ToArray());
            }

            this._attachmentTopicRepository.Add(topic);
        }

        public IEnumerable<TopicDto> FindTopicsViaEmail(string email)
        {
            return (from topic in this._topicRepository.FindAll()
                from subscriber in topic.Subscribers
                where subscriber.Email.Equals(email)
                select topic
                ).ToList().MapAs<TopicDto>();
        }

        public void RemoveTopic(Guid topicId, string handler)
        {
            var topic = this._topicRepository.GetByKey(topicId);
            if (topic != null)
            {
                topic.Disable();
                topic.SetUpdatedBy(handler);
                this._topicRepository.Update(topic);
            }
        }

        public void DeleteSubscriber(Guid topicId, Guid subscriberId, bool disableTopic = false)
        {
            this._topicRepository.RemoveSubscriber(topicId, subscriberId, disableTopic);
        }

        public void DeleteSubscriber(Guid topicId, string email, bool disableTopic = false)
        {
            this._topicRepository.RemoveSubscriber(topicId, email, disableTopic);
        }

        public void LogTaskRecord(TaskRecordDto taskRecord)
        {
            var record = new TaskRecord(taskRecord.TopicTaskId, taskRecord.ExecuteStartTime, taskRecord.ExecuteEndTime,
                taskRecord.HostName, taskRecord.ExecutedResult, taskRecord.ErrorMessage);
            this._taskRecordRepository.Add(record);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._topicRepository != null) 
                this._topicRepository.Context.Dispose();
            if (this._attachmentTopicRepository != null)
                this._attachmentTopicRepository.Context.Dispose();
            if (this._taskRecordRepository != null)
                this._taskRecordRepository.Context.Dispose();
        }

        #endregion
    }
}
