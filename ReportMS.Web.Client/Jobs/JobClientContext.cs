using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs
{
    /// <summary>
    /// Job 客户端上下文
    /// </summary>
    public class JobClientContext : IJobClientContext
    {
        #region Private Fields

        private readonly Lazy<List<Tuple<Expression<Action>, ScheduleCronOptions>>> newCollection
            = new Lazy<List<Tuple<Expression<Action>, ScheduleCronOptions>>>(() => new List<Tuple<Expression<Action>, ScheduleCronOptions>>());

        #endregion

        #region IJobClientContext Members

        public void AddTask(Expression<Action> action, ScheduleCronOptions schedule)
        {
            newCollection.Value.Add(Tuple.Create(action, schedule));
        }

        public List<Tuple<Expression<Action>, ScheduleCronOptions>> Tasks
        {
            get { return this.newCollection.Value; }
        }

        #endregion
    }
}
