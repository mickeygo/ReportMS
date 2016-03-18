using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.SubscriberModule
{
    /// <summary>
    /// 主题的执行任务.记录执行计划信息
    /// </summary>
    public class TopicTask : Entity, IValidatableObject
    {
        #region Public Properties

        /// <summary>
        /// 获取主题 Id
        /// </summary>
        public Guid TopicId { get; private set; }

        /// <summary>
        /// 获取主题
        /// </summary>
        public virtual Topic Topic { get; private set; }

        /// <summary>
        /// 获取一个<c>TaskSchedule</c>，表示任务的执行计划
        /// </summary>
        public TaskSchedule TaskSchedule { get; private set; }

        /// <summary>
        /// 获取 Task 执行计划的月数 [1-12]
        /// </summary>
        public int? Month { get; private set; }

        /// <summary>
        /// 获取 Task 执行计划的周数 [0-6]
        /// </summary>
        public DayOfWeek? Week { get; private set; }

        /// <summary>
        /// 获取 Task 执行计划的天数 [1-31]
        /// </summary>
        public int? Day { get; private set; }

        /// <summary>
        /// 获取 Task 执行计划的小时数 [0-23]
        /// </summary>
        public int? Hour { get; private set; }

        /// <summary>
        /// 获取 Task 执行计划的分钟数 [0-59]
        /// </summary>
        public int? Minute { get; private set; }

        #endregion

        #region
        
        /// <summary>
        /// 初始化一个新的<see cref="TopicTask"/>实例。仅供 EntityFramework 调用
        /// </summary>
        public TopicTask()
        {
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例.执行频率为每分钟.
        /// 注：按每分钟执行时，执行的秒并不一定是 00 时间点
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        public TopicTask(Guid topicId)
        {
            this.TopicId = topicId;
            this.TaskSchedule = TaskSchedule.Minutely;

            this.GenerateNewIdentity();
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例.执行频率为每小时
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public TopicTask(Guid topicId, int minute)
        {
            this.TopicId = topicId;
            this.Minute = minute;
            this.TaskSchedule = TaskSchedule.Hourly;

            this.GenerateNewIdentity();
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例。执行频率为每天
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public TopicTask(Guid topicId, int hour, int minute)
        {

            this.TopicId = topicId;
            this.Hour = hour;
            this.Minute = minute;
            this.TaskSchedule = TaskSchedule.Daily;

            this.GenerateNewIdentity();
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例。执行频率为每周
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="dayOfWeek">指定执行的时间：周几 [0-6]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public TopicTask(Guid topicId, DayOfWeek dayOfWeek, int hour, int minute)
        {
            this.TopicId = topicId;
            this.Week = dayOfWeek;
            this.Hour = hour;
            this.Minute = minute;
            this.TaskSchedule = TaskSchedule.Weekly;

            this.GenerateNewIdentity();
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例。执行频率为每月
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="day">指定执行的时间：天 [1-31]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public TopicTask(Guid topicId, int day, int hour, int minute)
        {
            this.TopicId = topicId;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.TaskSchedule = TaskSchedule.Monthly;

            this.GenerateNewIdentity();
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例。执行频率为每年
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="month">指定执行的时间：月 [1-12]</param>
        /// <param name="day">指定执行的时间：天 [1-31]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public TopicTask(Guid topicId, int month, int day, int hour, int minute)
        {
            this.TopicId = topicId;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.TaskSchedule = TaskSchedule.Yearly;

            this.GenerateNewIdentity();
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例。其中默认执行的分钟为 00
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="taskSchedule">任务的执行计划</param>
        /// <param name="month">指定执行的时间：月 [1-12]</param>
        /// <param name="week">指定执行的时间：周几 [0-6]</param>
        /// <param name="day">指定执行的时间：天 [1-31]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        public TopicTask(Guid topicId, TaskSchedule taskSchedule, int? month, DayOfWeek? week, int? day, int? hour)
            : this(topicId, taskSchedule, month, week, day, hour, 0)
        {
        }

        /// <summary>
        /// 初始化一个<c>TopicTask</c>实例
        /// </summary>
        /// <param name="topicId">主题 Id</param>
        /// <param name="taskSchedule">任务的执行计划</param>
        /// <param name="month">指定执行的时间：月 [1-12]</param>
        /// <param name="week">指定执行的时间：周几 [0-6]</param>
        /// <param name="day">指定执行的时间：天 [1-31]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public TopicTask(Guid topicId, TaskSchedule taskSchedule, int? month, DayOfWeek? week, int? day, int? hour, int? minute)
        {
            TopicId = topicId;
            TaskSchedule = taskSchedule;
            Month = month;
            Week = week;
            Day = day;
            Hour = hour;
            Minute = minute;

            this.GenerateNewIdentity();
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Month.HasValue && (Month < 1 || Month > 12))
                yield return new ValidationResult("The month must be 1 and 12.");
            if (Day.HasValue && (Day < 1 || Day > 31))
                yield return new ValidationResult("The day must be 1 and 31.");
            if (Hour.HasValue && (Hour < 0 || Hour > 23))
                yield return new ValidationResult("The hour must be 0 and 23.");
            if (Minute.HasValue && (Minute < 0 || Minute > 59))
                yield return new ValidationResult("The minute must be 0 and 59.");
        }

        #endregion
    }
}
