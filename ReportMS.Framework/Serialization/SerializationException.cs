using System;

namespace ReportMS.Framework.Serialization
{
    /// <summary>
    /// 表示序列化异常
    /// </summary>
    public class SerializationException : InfrastructureException
    {
        /// <summary>
        /// 初始化 <c>SerializationException</c> 实例
        /// </summary>
        public SerializationException()
        {
            
        }

        /// <summary>
        /// 初始化 <c>SerializationException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public SerializationException(string message) : base(message)
        { }

        /// <summary>
        /// 初始化 <c>SerializationException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">引起的内部异常</param>
        public SerializationException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// 初始化 <c>SerializationException</c> 实例
        /// </summary>
        /// <param name="format">异常消息格式</param>
        /// <param name="args">异常消息集合</param>
        public SerializationException(string format, params object[] args)
            : base(String.Format(format, args))
        { }
    }
}
