using System;
using System.IO;
using Gear.Utility.Schedule;

namespace ReportMS.Web.Client.Jobs.Subscribers
{
    // 测试 Job，仅供测试时使用
    public class TestJob : JobSubScriber
    {
        #region ISubScriber Members

        public override void Handle()
        {
            using (var sw = new StreamWriter(@"D:\JobText.txt", true))
            {
                sw.WriteLineAsync(DateTime.Now.ToString());
            }
        }

        public override ScheduleCronOptions Schedule
        {
            get { return new ScheduleCronOptions(); }
        }

        #endregion
    }
}
