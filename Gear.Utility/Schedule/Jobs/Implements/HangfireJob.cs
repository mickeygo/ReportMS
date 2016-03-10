using Hangfire;
using System;
using System.Linq.Expressions;

namespace Gear.Utility.Schedule.Jobs.Implements
{
    /// <summary>
    /// 基于 Hangfire 框架的 Job
    /// </summary>
    public class HangfireJob : IJob
    {
        #region IJob Members

        // action 必须是 public 方法
        public void AddTask(Expression<Action> action, ScheduleCronOptions cronOptions)
        {
            var cron = this.ConvertToCronExpression(cronOptions);
            RecurringJob.AddOrUpdate(action, cron);
        }

        #endregion

        #region Private Methods

        // 执行的时间表。关于 CRON 详细信息，见 https://en.wikipedia.org/wiki/Cron#CRON_expression
        private string ConvertToCronExpression(ScheduleCronOptions cronOptions)
        {
            switch (cronOptions.scheduleCron)
            {
                case ScheduleCron.Minutely:
                    return Cron.Minutely();
                case ScheduleCron.Hourly:
                    return Cron.Hourly(cronOptions.Minute);
                case ScheduleCron.Daily:
                    return Cron.Daily(cronOptions.Hour, cronOptions.Minute);
                case ScheduleCron.Weekly:
                    return Cron.Weekly((DayOfWeek) cronOptions.Week, cronOptions.Hour, cronOptions.Minute);
                case ScheduleCron.Monthly:
                    return Cron.Monthly(cronOptions.Day, cronOptions.Hour, cronOptions.Minute);
                case ScheduleCron.Yearly:
                    return Cron.Yearly(cronOptions.Month, cronOptions.Day, cronOptions.Hour, cronOptions.Minute);
                default:
                    throw new InvalidOperationException("Can not convert the scheduleCronOptions to cron.");
            }
        }

        #endregion
    }
}
