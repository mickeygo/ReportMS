using System;
using System.Collections.Generic;
using Gear.Infrastructure.Events;

namespace Gear.Infrastructure.Bus
{
    /// <summary>
    /// 表示实现接口的类为消息总线类型
    /// </summary>
    public interface IBus : IDisposable
    {
        /// <summary>
        /// 获取总线的全局唯一标识
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 将指定的消息发布到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型</typeparam>
        /// <param name="message">要发布的消息</param>
        void Publish<TMessage>(TMessage message) where TMessage : class, IEvent;

        /// <summary>
        /// 将指定的消息集合发布到总线
        /// </summary>
        /// <typeparam name="TMessage">要发布的消息的类型</typeparam>
        /// <param name="messages">要发布的消息</param>
        void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IEvent;

        /// <summary>
        /// 清除要提交的消息
        /// </summary>
        void Clear();
    }
}
