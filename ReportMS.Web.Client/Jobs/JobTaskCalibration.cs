using System;
using System.Collections.Generic;
using System.Linq;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// Job 执行的任务的时间校准。
    /// 筛选出满足当前时间的主题
    /// </summary>
    public class JobTaskCalibration
    {
        #region Private Fields

        private readonly DateTime now = DateTime.Now;
        private readonly Dictionary<TopicDto, IEnumerable<TopicTaskDto>> _filterTopics = new Dictionary<TopicDto, IEnumerable<TopicTaskDto>>();
        private readonly IEnumerable<TopicDto> _topics;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>JobTaskCalibration</c>实例
        /// </summary>
        /// <param name="topics">要校准的主题集合</param>
        public JobTaskCalibration(IEnumerable<TopicDto> topics)
        {
            this._topics = topics;

            this.Calibrate();
        }

        #endregion

        #region Private Methods

        private void Calibrate()
        {
            if (this._topics == null)
                return;

            foreach (var topic in this._topics)
                this.CalibrateTopics(topic);
        }

        private void CalibrateTopics(TopicDto topic)
        {
            var tasks = topic.TopicTasks.Where(this.CalibrateCore);
            if (!tasks.Any())
                return;

            this._filterTopics.Add(topic, tasks);
        }

        private bool CalibrateCore(TopicTaskDto task)
        {
            switch (task.TaskSchedule)
            {
                case TaskScheduleDto.Minutely:
                    return true;
                case TaskScheduleDto.Hourly:
                    return (task.Minute == now.Minute);
                case TaskScheduleDto.Daily:
                    return (task.Hour == now.Hour)
                           && (task.Minute == now.Minute);
                case TaskScheduleDto.Weekly:
                    return (task.Week == now.DayOfWeek)
                           && (task.Hour == now.Hour)
                           && (task.Minute == now.Minute);
                case TaskScheduleDto.Monthly:
                    return (task.Day == now.Day)
                           && (task.Hour == now.Hour)
                           && (task.Minute == now.Minute);
                case TaskScheduleDto.Yearly:
                    return (task.Month == now.Month)
                           && (task.Day == now.Day)
                           && (task.Hour == now.Hour)
                           && (task.Minute == now.Minute);
                default:
                    return false;
            }
        }

        #endregion


        #region Properties

        /// <summary>
        /// 获取筛选后的主题.
        /// Key 为 符合条件的主题； Value 为符合当前主题当前时刻要执行的主题任务
        /// </summary>
        public IDictionary<TopicDto, IEnumerable<TopicTaskDto>> FilterTopics
        {
            get { return this._filterTopics; }
        }

        #endregion
    }
}
