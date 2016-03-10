using System;

namespace Gear.Utility.Schedule
{
    /// <summary>
    /// 执行计划表参数。
    /// 通过设置时间参数，来指定 Job 在某个设定的时间点执行。
    /// </summary>
    public class ScheduleCronOptions
    {
        #region Properties

        // 获取 Job 执行计划的分钟数 [0-59]
        public int Minute { get; private set; }

        // 获取 Job 执行计划的小时数 [0-23]
        public int Hour { get; private set; }

        // 获取 Job 执行计划的天数 [1-31]
        public int Day { get; private set; }

        // 获取 Job 执行计划的周数 [0-6]
        public int Week { get; private set; }

        // 获取 Job 执行计划的月数 [1-12]
        public int Month { get; private set; }

        /// <summary>
        /// 获取执行执行计划
        /// </summary>
        public ScheduleCron scheduleCron { get; private set; }

        #endregion

        #region

        /// <summary>
        /// 初始化一个<c>ScheduleCronOptions</c>实例.执行频率为每分钟.
        /// 注：按每分钟执行时，执行的秒并不一定是 00 时间点
        /// </summary>
        public ScheduleCronOptions()
        {
            this.scheduleCron = ScheduleCron.Minutely;
        }

        /// <summary>
        /// 初始化一个<c>ScheduleCronOptions</c>实例.执行频率为每小时
        /// </summary>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public ScheduleCronOptions(int minute)
        {
            this.ValidateOptions(null, null, null, minute);

            Minute = minute;
            this.scheduleCron = ScheduleCron.Hourly;
        }

        /// <summary>
        /// 初始化一个<c>ScheduleCronOptions</c>实例。执行频率为每天
        /// </summary>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public ScheduleCronOptions(int hour, int minute)
        {
            this.ValidateOptions(null, null, hour, minute);

            Minute = minute;
            Hour = hour;
            this.scheduleCron = ScheduleCron.Daily;
        }

        /// <summary>
        /// 初始化一个<c>ScheduleCronOptions</c>实例。执行频率为每周
        /// </summary>
        /// <param name="dayOfWeek">指定执行的时间：周几 [0-6]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public ScheduleCronOptions(DayOfWeek dayOfWeek, int hour, int minute)
        {
            this.ValidateOptions(null, null, hour, minute);

            Minute = minute;
            Hour = hour;
            Week = (int)dayOfWeek;
            this.scheduleCron = ScheduleCron.Weekly;
        }

        /// <summary>
        /// 初始化一个<c>ScheduleCronOptions</c>实例。执行频率为每月
        /// </summary>
        /// <param name="day">指定执行的时间：天 [1-31]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public ScheduleCronOptions(int day, int hour, int minute)
        {
            this.ValidateOptions(null, day, hour, minute);

            Minute = minute;
            Hour = hour;
            Day = day;
            this.scheduleCron = ScheduleCron.Monthly;
        }

        /// <summary>
        /// 初始化一个<c>ScheduleCronOptions</c>实例。执行频率为每年
        /// </summary>
        /// <param name="month">指定执行的时间：月 [1-12]</param>
        /// <param name="day">指定执行的时间：天 [1-31]</param>
        /// <param name="hour">指定执行的时间：小时 [0-23]</param>
        /// <param name="minute">指定执行的时间：分钟 [0-59]</param>
        public ScheduleCronOptions(int month, int day, int hour, int minute)
        {
            this.ValidateOptions(month, day, hour, minute);

            Minute = minute;
            Hour = hour;
            Day = day;
            Month = month;
            this.scheduleCron = ScheduleCron.Yearly;
        }

        #endregion

        #region Private Methods

        private void ValidateOptions(int? month, int? day, int? hour, int? minute)
        {
            if (month.HasValue && (month < 1 || month > 12))
                throw new InvalidOperationException("The month must be 1 and 12.");
            if (day.HasValue && (day < 1 || day > 31))
                throw new InvalidOperationException("The day must be 1 and 31.");
            if (hour.HasValue && (hour < 0 || hour > 23))
                throw new InvalidOperationException("The hour must be 0 and 23.");
            if (minute.HasValue && ( minute < 0 || minute > 59))
                throw new InvalidOperationException("The minute must be 0 and 59.");
        }

        #endregion
    }
}
