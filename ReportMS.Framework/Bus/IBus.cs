using System;
using System.Collections.Generic;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 表示实现类是消息总线
    /// </summary>
    public interface IBus : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 发布指定的消息到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息类型</typeparam>
        /// <param name="message">要发布的消息</param>
        void Publish<TMessage>(TMessage message);

        /// <summary>
        /// 发布指定的消息集到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息类型</typeparam>
        /// <param name="messages">要发布的消息集合</param>
        void Publish<TMessage>(IEnumerable<TMessage> messages);

        /// <summary>
        /// 清除已经发布且处于等待 Commit 状态的消息
        /// </summary>
        void Clear();
    }
}
