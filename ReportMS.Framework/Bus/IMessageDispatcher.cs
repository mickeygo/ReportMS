using System;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 表示实现类是消息分发器
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// 清空注册的消息处理程序
        /// </summary>
        void Clear();

        /// <summary>
        /// 调度消息
        /// </summary>
        /// <param name="message">要被分发的消息</param>
        void DispatchMessage<T>(T message);

        /// <summary>
        /// 将指定的消息处理程序注册到消息分发器
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">要被注册的处理程序</param>
        void Register<T>(IHandler<T> handler);

        /// <summary>
        /// 注销指定的消息处理程序
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">要被注销的处理程序</param>
        void UnRegister<T>(IHandler<T> handler);

        /// <summary>
        /// 当消息分发程序将发送消息时发生
        /// </summary>
        event EventHandler<MessageDispatchEventArgs> Dispatching;

        /// <summary>
        /// 当消息分发程序发送消息失败时发生
        /// </summary>
        event EventHandler<MessageDispatchEventArgs> DispatchFailed;

        /// <summary>
        /// 当消息分发程序将发送消息完成时发生
        /// </summary>
        event EventHandler<MessageDispatchEventArgs> Dispatched;

    }
}
