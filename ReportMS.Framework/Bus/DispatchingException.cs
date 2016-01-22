using System;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 表示分配器异常
    /// </summary>
    [Serializable]
    public class DispatchingException : InfrastructureException
    {
        /// <summary>
        /// 初始化 <c>DispatchingException</c> 实例
        /// </summary>
        public DispatchingException()
        { }

        /// <summary>
        /// 初始化 <c>DispatchingException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public DispatchingException(string message) : base(message)
        { }

        /// <summary>
        /// 初始化 <c>DispatchingException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">引起的内部异常</param>
        public DispatchingException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// 初始化 <c>DispatchingException</c> 实例
        /// </summary>
        /// <param name="format">异常消息格式</param>
        /// <param name="args">异常消息集合</param>
        public DispatchingException(string format, params object[] args)
            : base(String.Format(format, args))
        { }
    }
}
