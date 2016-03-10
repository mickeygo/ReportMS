using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.SubscriberModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 主题的 Dto 配置类
    /// </summary>
    internal class TopicMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<TaskSchedule, TaskScheduleDto>();
            AutoMapperAdapter.Register<Topic, TopicDto>();
            AutoMapperAdapter.Register<AttachmentTopic, AttachmentTopicDto>();
            AutoMapperAdapter.Register<TopicTask, TopicTaskDto>();
            AutoMapperAdapter.Register<Subscriber, SubscriberDto>();
            AutoMapperAdapter.Register<TaskRecord, TaskRecordDto>();
        }
    }
}
