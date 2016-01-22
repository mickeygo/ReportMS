using System;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 在分发消息时，生成事件数据
    /// </summary>
    public class MessageDispatchEventArgs : EventArgs
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置消息
        /// </summary>
        public dynamic Message { get; set; }

        /// <summary>
        /// 获取或设置消息处理者类型
        /// </summary>
        public Type HandlerType { get; set; }

        /// <summary>
        /// 获取或设置处理者
        /// </summary>
        public object Handler { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>MessageDispatchEventArgs</c>实例
        /// </summary>
        public MessageDispatchEventArgs()
        { }

        /// <summary>
        /// 初始化<c>MessageDispatchEventArgs</c>实例
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="handlerType">消息处理者类型</param>
        /// <param name="handler">处理者</param>
        public MessageDispatchEventArgs(dynamic message, Type handlerType, object handler)
        {
            this.Message = message;
            this.HandlerType = handlerType;
            this.Handler = handler;
        }

        #endregion
    }
}
