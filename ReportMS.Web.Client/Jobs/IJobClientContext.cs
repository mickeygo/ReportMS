using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// 表示实现此接口的类为 Job 客户端上下文
    /// </summary>
    public interface IJobClientContext
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="action">要执行的方法. action 必须是 public</param>
        /// <param name="cronOptions">执行的时间表参数. 在设置的时间点执行</param>
        void AddTask(Expression<Action> action, ScheduleCronOptions cronOptions);

        /// <summary>
        /// 获取所有的 Job 任务.
        /// Item1 为 Action；Item2 为 Schedule
        /// </summary>
        List<Tuple<Expression<Action>, ScheduleCronOptions>> Tasks { get; }
    }
}
