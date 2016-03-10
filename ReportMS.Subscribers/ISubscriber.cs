using System;

namespace ReportMS.Subscribers
{
    /// <summary>
    /// 表示实现此接口的类型为订阅器
    /// </summary>
    public interface ISubscriber
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        void Subscribe(Action message);

        /// <summary>
        /// 发布消息
        /// </summary>
        void Publish();
    }
}
