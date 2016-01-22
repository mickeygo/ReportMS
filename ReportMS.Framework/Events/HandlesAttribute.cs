using System;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 内部领域事件处理者的装饰方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class HandlesAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置能被装饰方法处理的领域事件类型
        /// </summary>
        public Type DomainEventType { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>HandlesAttribute</c>实例.
        /// </summary>
        /// <param name="domainEventType">能被装饰方法处理的领域事件类型</param>
        public HandlesAttribute(Type domainEventType)
        {
            this.DomainEventType = domainEventType;
        }

        #endregion
    }
}
