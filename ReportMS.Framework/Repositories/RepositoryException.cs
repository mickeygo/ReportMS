using System;

namespace ReportMS.Framework.Repositories
{
    /// <summary>
    /// 表示仓储异常
    /// </summary>
    public class RepositoryException : InfrastructureException
    {
        /// <summary>
        /// 初始化 <c>RepositoryException</c> 实例
        /// </summary>
        public RepositoryException()
        { }

        /// <summary>
        /// 初始化 <c>RepositoryException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public RepositoryException(string message) : base(message)
        { }

        /// <summary>
        /// 初始化 <c>RepositoryException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">引起的内部异常</param>
        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// 初始化 <c>RepositoryException</c> 实例
        /// </summary>
        /// <param name="format">异常消息格式</param>
        /// <param name="args">异常消息集合</param>
        public RepositoryException(string format, params object[] args)
            : base(String.Format(format, args))
        { }
    }
}
