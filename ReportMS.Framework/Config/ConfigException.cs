using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportMS.Framework.Config
{
    /// <summary>
    /// 表示配置异常
    /// </summary>
    [Serializable]
    public class ConfigException : InfrastructureException
    {
        /// <summary>
        /// 初始化 <c>ConfigException</c> 实例
        /// </summary>
        public ConfigException()
        { }

        /// <summary>
        /// 初始化 <c>ConfigException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public ConfigException(string message) : base(message)
        { }

        /// <summary>
        /// 初始化 <c>ConfigException</c> 实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">引起的内部异常</param>
        public ConfigException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// 初始化 <c>ConfigException</c> 实例
        /// </summary>
        /// <param name="format">异常消息格式</param>
        /// <param name="args">异常消息集合</param>
        public ConfigException(string format, params object[] args)
            : base(String.Format(format, args))
        { }
    }
}
