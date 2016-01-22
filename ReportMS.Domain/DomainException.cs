using System;

namespace ReportMS.Domain
{
    /// <summary>
    /// 表示领域异常
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        /// <summary>
        /// 初始化 <c>InfrastructureException</c> 实例
        /// </summary>
        public DomainException()
        { }

        /// <summary>
        /// 初始化 <c>InfrastructureException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public DomainException(string message)
            : base(message)
        { }

        /// <summary>
        /// 初始化 <c>InfrastructureException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">引起的内部异常</param>
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// 初始化 <c>InfrastructureException</c> 实例
        /// </summary>
        /// <param name="format">异常消息格式</param>
        /// <param name="args">异常消息集合</param>
        public DomainException(string format, params object[] args)
            : base(String.Format(format, args))
        { }
    }
}
