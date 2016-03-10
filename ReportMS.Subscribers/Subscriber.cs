using System;
using System.Threading;

namespace ReportMS.Subscribers
{
    /// <summary>
    /// 订阅器
    /// </summary>
    public abstract class Subscriber : ISubscriber
    {
        public event EventHandler Rss;

        #region ISubscriber Members

        public void Subscribe(Action message)
        {

        }

        public void Publish()
        {
            var temp = Interlocked.CompareExchange(ref Rss, null, null);
            if (temp != null)
                temp(this, null);
        }

        #endregion
    }
}
