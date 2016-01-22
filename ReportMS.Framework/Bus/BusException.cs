using System;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 表示总线异常
    /// </summary>
    [Serializable]
    public class BusException : InfrastructureException
    {
        /// <summary>
        /// 初始化 <c>BusException</c> 实例
        /// </summary>
        public BusException()
        { }

        /// <summary>
        /// 初始化 <c>BusException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public BusException(string message) : base(message)
        { }

        /// <summary>
        /// 初始化 <c>BusException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">引起的内部异常</param>
        public BusException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// 初始化 <c>BusException</c> 实例
        /// </summary>
        /// <param name="format">异常消息格式</param>
        /// <param name="args">异常消息集合</param>
        public BusException(string format, params object[] args)
            : base(String.Format(format, args))
        { }
    }
}
