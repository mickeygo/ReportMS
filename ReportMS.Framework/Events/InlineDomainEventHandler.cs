using System;
using System.Linq;
using System.Reflection;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// Represents the domain event handler that is defined within the scope of
    /// an aggregate root and handles the domain event when <c>SourcedAggregateRoot.RaiseEvent&lt;TEvent&gt;</c>
    /// is called.
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public sealed class InlineDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : class, IDomainEvent
    {
        #region Private Fields
        private readonly Type domainEventType;
        private readonly Action<TDomainEvent> action;
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>InlineDomainEventHandler</c>实例
        /// </summary>
        /// <param name="aggregateRoot">领域事件被提出并处理的聚合根实例的实例</param>
        /// <param name="method">处理领域事件的方法</param>
        public InlineDomainEventHandler(ISourcedAggregateRoot aggregateRoot, MethodBase method)
        {
            var parameters = method.GetParameters();
            if (parameters == null || !parameters.Any())
                throw new ArgumentException(string.Format("Only the property member of the given data object and the field member is supported. Current member type is {0}.", method.Name), "method");

            domainEventType = parameters[0].ParameterType;
            action = domainEvent =>
            {
                try
                {
                    method.Invoke(aggregateRoot, new object[] { domainEvent });
                }
                catch
                {
                    // ignored
                }
            };
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as InlineDomainEventHandler<TDomainEvent>;
            if (obj == null)
                return false;
            return other != null && Delegate.Equals(this.action, other.action);
        }

        public override int GetHashCode()
        {
            if (this.action != null && this.domainEventType != null)
                return Utils.GetHashCode(this.action.GetHashCode(),
                    this.domainEventType.GetHashCode());
            return base.GetHashCode();
        }
        #endregion

        #region IHandler<TDomainEvent> Members

        /// <summary>
        /// 处理指定的消息
        /// </summary>
        /// <param name="message">要处理的消息</param>
        public void Handle(TDomainEvent message)
        {
            action(message);
        }

        #endregion
    }
}
