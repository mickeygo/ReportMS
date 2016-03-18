using System;
using System.IO;
using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs.Subscribers
{
    // 测试 Job
    public class TestJob : ISubScriber
    {
        #region ISubScriber Members

        public void Subscribe()
        {
            using (var sw = new StreamWriter(@"D:\JobText.txt", true))
            {
                sw.WriteLineAsync(DateTime.Now.ToString());
            }
        }

        public ScheduleCronOptions Schedule
        {
            get { return new ScheduleCronOptions(); }
        }

        #endregion
    }
}
