using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示实现类是事件聚合器
    /// </summary>
    /// <remarks>关于 EventAggregator 模式，可参见 http://msdn.microsoft.com/en-us/library/ff921122(v=pandp.20).aspx </remarks>
    public class EventAggregator : IEventAggregator
    {
        #region Private Fields
        private readonly object sync = new object();
        private readonly MethodInfo registerEventHandlerMethod;
        private readonly ConcurrentDictionary<Type, List<object>> eventHandlerCollection = new ConcurrentDictionary<Type, List<object>>(); 
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>EventAggregator</c>实例
        /// </summary>
        public EventAggregator()
        {
            // 获取该实例中所有的且有且只有一个 "IEventHandler<>" 泛型类型的参数的 "Subscribe" 方法
            this.registerEventHandlerMethod = (from p in this.GetType().GetMethods()
                                                let methodName = p.Name
                                                let parameters = p.GetParameters()
                                                where methodName == "Subscribe" &&
                                                      parameters != null &&
                                                      parameters.Length == 1 &&
                                                      parameters[0].ParameterType.GetGenericTypeDefinition() == typeof (IEventHandler<>)
                                                select p).First();
        }

        /// <summary>
        /// 初始化<c>EventAggregator</c>实例
        /// </summary>
        /// <param name="handlers">已被注册到事件聚合器中的事件处理者</param>
        /// <remarks>
        /// All the event handlers registered to the Event Aggregator should implement the <see cref="IEventHandler{T}"/>
        /// interface, otherwise, the instance will be ignored. When using IoC containers to register dependencies,
        /// remember to specify not only the name of the dependency, but also the type of the dependency.
        /// </remarks>
        public EventAggregator(IEnumerable<object> handlers) : this()
        {
            // 调用所有的 
            foreach (var obj in handlers)
            {
                var type = obj.GetType();
                var implementedInterfaces = type.GetInterfaces();
                foreach (var implementedInterface in implementedInterfaces)
                {
                    if (implementedInterface.IsGenericType &&
                        implementedInterface.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                    {
                        var eventType = implementedInterface.GetGenericArguments().First();
                        var method = this.registerEventHandlerMethod.MakeGenericMethod(eventType);
                        method.Invoke(this, new[] { obj });
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 检查两个时间处理者是否相等
        /// 若两个事件是 ActionDelegated 委托，则简单比较两个委托是否相等（重写 Equals）；否则，则比较两个对象是否相等.
        /// 我们不需要为每个指定的事件注册多次相同类型的事件处理程序
        /// </summary>
        private readonly Func<object, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            if (o1Type.IsGenericType && o1Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>) &&
                o2Type.IsGenericType && o2Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>))
                return o1.Equals(o2);
            return o1Type == o2Type;
        };

        #region IEventAggregator Members

        /// <summary>
        /// 订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">事件处理者</param>
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            lock (this.sync)
            {
                var eventType = typeof (TEvent);
                if (this.eventHandlerCollection.ContainsKey(eventType))
                {
                    var handlers = this.eventHandlerCollection[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => this.eventHandlerEquals(deh, eventHandler)))
                            handlers.Add(eventHandlerCollection);
                    }
                    else
                        handlers = new List<object> {eventHandler};
                }
                else
                {
                    this.eventHandlerCollection.TryAdd(eventType, new List<object> { eventHandler });
                }
            }
        }

        /// <summary>
        /// 订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集</param>
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Subscribe(eventHandler);
        }

        /// <summary>
        /// 订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者</param>
        public void Subscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Subscribe(eventHandler);
        }

        /// <summary>
        /// 订阅 <see cref="Action{T}"/> 委托.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerAction"><see cref="Action"/> 委托</param>
        public void Subscribe<TEvent>(Action<TEvent> eventHandlerAction) where TEvent : class, IEvent
        {
            this.Subscribe(new ActionDelegatedEventHandler<TEvent>(eventHandlerAction));
        }

        /// <summary>
        /// 订阅 <see cref="Action{T}"/> 委托.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action"/> 委托集</param>
        public void Subscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions) where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                this.Subscribe(eventHandlerAction);
        }

        /// <summary>
        /// 订阅 <see cref="Action{T}"/> 委托.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action"/> 委托</param>
        public void Subscribe<TEvent>(params Action<TEvent>[] eventHandlerActions) where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                this.Subscribe(eventHandlerAction);
        }

        /// <summary>
        /// 注销订阅的事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">事件处理者</param>
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            lock (this.sync)
            {
                var eventType = typeof (TEvent);
                if (this.eventHandlerCollection.ContainsKey(eventType))
                {
                    var handlers = this.eventHandlerCollection[eventType];
                    if (handlers != null && handlers.Exists(deh => this.eventHandlerEquals(deh, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => this.eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }

        /// <summary>
        /// 注销订阅的事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集</param>
        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Unsubscribe(eventHandler);
        }

        /// <summary>
        /// 注销订阅的事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者</param>
        public void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Unsubscribe(eventHandler);
        }

        /// <summary>
        /// 注销订阅的<see cref="Action{T}"/>委托
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerAction"><see cref="Action{T}"/> 委托</param>
        public void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction) where TEvent : class, IEvent
        {
            this.Unsubscribe(new ActionDelegatedEventHandler<TEvent>(eventHandlerAction));
        }

        /// <summary>
        /// 注销订阅的<see cref="Action{T}"/>委托
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 委托</param>
        public void Unsubscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions) where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                this.Unsubscribe(eventHandlerAction);
        }

        /// <summary>
        /// 注销订阅的<see cref="Action{T}"/>委托
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 委托</param>
        public void Unsubscribe<TEvent>(params Action<TEvent>[] eventHandlerActions) where TEvent : class, IEvent
        {
            foreach (var eventHandlerAction in eventHandlerActions)
                this.Unsubscribe(eventHandlerAction);
        }

        /// <summary>
        /// 注销订阅所有的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        public void UnsubscribeAll<TEvent>() where TEvent : class, IEvent
        {
            lock (this.sync)
            {
                var eventType = typeof(TEvent);
                if (this.eventHandlerCollection.ContainsKey(eventType))
                {
                    var handlers = this.eventHandlerCollection[eventType];
                    if (handlers != null)
                        handlers.Clear();
                }
            }
        }

        /// <summary>
        /// 注销订阅所有的在事件聚合器的所有事件
        /// </summary>
        public void UnsubscribeAll()
        {
            lock (this.sync)
            {
                this.eventHandlerCollection.Clear();
            }
        }

        /// <summary>
        /// 获取订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>处理程序集合</returns>
        public IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>() where TEvent : class, IEvent
        {
            var eventType = typeof (TEvent);
            if (this.eventHandlerCollection.ContainsKey(eventType))
            {
                var handlers = this.eventHandlerCollection[eventType];
                return handlers != null ? handlers.Select(e => e as IEventHandler<TEvent>) : null;
            }

            return null;
        }

        /// <summary>
        /// 将事件发布到所有的订阅者
        /// </summary>
        /// <typeparam name="TEvent">要发布的事件类型</typeparam>
        /// <param name="event">要发布的事件</param>
        public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            if (@event == null)
                throw  new ArgumentNullException("event");

            var eventType = @event.GetType();
            if (!this.eventHandlerCollection.ContainsKey(eventType)) 
                return;

            var handlers = this.eventHandlerCollection[eventType];
            if (handlers == null || !handlers.Any()) 
                return;

            foreach (var handler in handlers)
            {
                var eventHandler = handler as IEventHandler<TEvent>;
                if (eventHandler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                    Task.Factory.StartNew(o => eventHandler.Handle((TEvent)o), @event);
                else
                    eventHandler.Handle(@event);
            }
        }

        /// <summary>
        /// 将事件发布到所有的订阅者
        /// </summary>
        /// <typeparam name="TEvent">要发布的事件类型</typeparam>
        /// <param name="event">要发布的事件</param>
        /// <param name="callback">在事件被发布且处理后执行的回调方法; TEvent 表示事件, bool 表示是否有成功执行, Exception 表示抛出的异常(若发生异常)</param>
        /// <param name="timeout">表示并行处理事件的超时时间段</param>
        public void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class, IEvent
        {
            if (@event == null)
                throw new ArgumentNullException("event");

            var eventType = @event.GetType();
            if (this.eventHandlerCollection.ContainsKey(eventType))
            {
                var handlers = this.eventHandlerCollection[eventType];
                if (handlers == null || !handlers.Any())
                {
                    callback(@event, false, null);
                    return;
                }

                var tasks = new List<Task>();
                try
                {
                    foreach (var handler in handlers)
                    {
                        var eventHandler = handler as IEventHandler<TEvent>;
                        if (eventHandler.GetType().IsDefined(typeof (ParallelExecutionAttribute), false))
                            tasks.Add(Task.Factory.StartNew((o) => eventHandler.Handle((TEvent) o), @event));
                        else
                            eventHandler.Handle(@event);

                        if (tasks.Any())
                        {
                            if (timeout == null)
                                Task.WaitAll(tasks.ToArray());
                            else
                                Task.WaitAll(tasks.ToArray(), timeout.Value);
                        }

                        callback(@event, true, null);
                    }
                }
                catch (Exception ex)
                {
                    callback(@event, false, ex);
                }
            }
            else
                callback(@event, false, null);
        }

        #endregion
    }
}
