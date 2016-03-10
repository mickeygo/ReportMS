using System;
using System.Linq.Expressions;

namespace Gear.Utility.Schedule.Jobs
{
    /// <summary>
    /// 表示实现此接口的类为 Job 定时器类
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="action">要执行的方法. action 必须是 public</param>
        /// <param name="cronOptions">执行的时间表参数. 在设置的时间点执行</param>
        void AddTask(Expression<Action> action, ScheduleCronOptions cronOptions);
    }
}
