using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.SubscriberModule;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 订阅主题服务
    /// </summary>
    public class TopicService : ITopicService
    {
        #region Privte Fields

        private readonly ITopicRepository _topicRepository;
        private readonly ITaskRecordRepository _taskRecordRepository;

        #endregion

        #region Ctor    

        public TopicService(ITopicRepository topicRepository, ITaskRecordRepository taskRecordRepository)
        {
            _topicRepository = topicRepository;
            _taskRecordRepository = taskRecordRepository;
        }

        #endregion

        #region ITopicService Members

        public IEnumerable<TopicDto> FindAll()
        {
            return this._topicRepository.FindAll().ToList().MapAs<TopicDto>();
        }

        public TopicDto Find(Guid topicId)
        {
            return this._topicRepository.GetByKey(topicId).MapAs<TopicDto>();
        }

        public IEnumerable<TopicDto> FindTopicsViaOwner(string owner)
        {
            var spec = Specification<Topic>.Eval(t => t.CreatedBy.Equals(owner, StringComparison.OrdinalIgnoreCase));
            return this._topicRepository.FindAll(spec).ToList().MapAs<TopicDto>();
        }

        public IEnumerable<TopicDto> FindTopicsViaEmail(string email)
        {
            return (from topic in this._topicRepository.FindAll()
                from subscriber in topic.Subscribers
                where subscriber.Email.Equals(email)
                select topic
                ).ToList().MapAs<TopicDto>();
        }

        public void ModifyTopic(TopicDto topicDto, bool includeSubscriber = true)
        {
            var topic = this._topicRepository.GetByKey(topicDto.ID);
            if (topic == null)
                return;

            topic.Update(topicDto.TopicName, topicDto.Description, topicDto.Subject, topicDto.Body, topicDto.UpdatedBy);

            if (includeSubscriber)
            {
                this._topicRepository.RemoveSubscribers(topic.Subscribers);

                var subscribers = topicDto.Subscribers;
                if (subscribers != null)
                {
                    var taskSubcribers = (from subscriber in subscribers
                                          select new Subscriber(topic.ID, subscriber.Email));
                    topic.AddSubscribers(taskSubcribers.ToArray());
                }
            }

            this._topicRepository.Update(topic);
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

        public void DeleteSubscriber(Guid topicId, Guid subscriberId)
        {
            var topic = this._topicRepository.GetByKey(topicId);
            if (topic == null)
                return;

            var subscriber = topic.Subscribers.SingleOrDefault(s => s.ID == subscriberId);
            if (subscriber != null)
            {
                this._topicRepository.RemoveSubscriber(subscriber);
                this._topicRepository.Update(topic);
            }
        }

        public void DeleteSubscriber(Guid topicId, string email)
        {
            var topic = this._topicRepository.GetByKey(topicId);
            if (topic == null)
                return;

            var subscribers = topic.Subscribers.Where(s => s.Email.Equals(email)).ToList();
            if (subscribers.Any())
            {
                this._topicRepository.RemoveSubscribers(subscribers);
                this._topicRepository.Update(topic);
            }
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
            if (this._taskRecordRepository != null)
                this._taskRecordRepository.Context.Dispose();
        }

        #endregion
    }
}
