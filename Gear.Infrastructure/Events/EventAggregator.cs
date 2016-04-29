using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 事件聚合器
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private readonly object sync = new object();
        private readonly ConcurrentDictionary<Type, List<object>> _eventHandlers = new ConcurrentDictionary<Type, List<object>>(); 
        private readonly MethodInfo registerEventHandlerMethod;

        // checks if the two event handlers are equal. if the event handler is an action-delegated, just simply
        // compare the two with the object.Equals override (since it was overriden by comparing the two delegates. Otherwise,
        // the type of the event handler will be used because we don't need to register the same type of the event handler
        // more than once for each specific event.
        private readonly Func<object, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            if (o1Type.IsGenericType &&
                o1Type.GetGenericTypeDefinition() == typeof (ActionDelegatedEventHandler<>) &&
                o2Type.IsGenericType &&
                o2Type.GetGenericTypeDefinition() == typeof (ActionDelegatedEventHandler<>))
            {
                return o1.Equals(o2);
            }
            return o1Type == o2Type;
        };

        /// <summary>
        /// 初始化一个新的<c>EventAggregator</c>实例
        /// </summary>
        public EventAggregator()
        {
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
        /// 初始化一个新的<c>EventAggregator</c>实例
        /// </summary>
        /// <param name="handlers">处理者集合</param>
        public EventAggregator(IEnumerable<object> handlers) : this()
        {
            foreach (var obj in handlers)
            {
                var type = obj.GetType();
                var implementedInterfaces = type.GetInterfaces();

                var methods = (from implementedInterface in implementedInterfaces
                    where implementedInterface.IsGenericType &&
                          implementedInterface.GetGenericTypeDefinition() == typeof (IEventHandler<>)
                    select implementedInterface.GetGenericArguments().First()
                    into eventType
                    select this.registerEventHandlerMethod.MakeGenericMethod(eventType));

                foreach (var method in methods)
                {
                    method.Invoke(this, new[] {obj});
                }
            }
        }

        #region IEventAggregator Members

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">事件处理者</param>
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) 
            where TEvent : class, IEvent
        {
            lock (this.sync)
            {
                var eventType = typeof(TEvent);
                if (this._eventHandlers.ContainsKey(eventType))
                {
                    var handlers = this._eventHandlers[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => eventHandlerEquals(deh, eventHandler)))
                            handlers.Add(eventHandler);
                    }
                    else
                        handlers = new List<object> {eventHandler};
                }
                else
                    this._eventHandlers.TryAdd(eventType, new List<object> { eventHandler });
            }
        }

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集合</param>
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) 
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Subscribe(eventHandler);
        }

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集合</param>
        public void Subscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers) 
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Subscribe(eventHandler);
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFunc">事件处理程序委托</param>
        public void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc) 
            where TEvent : class, IEvent
        {
            this.Subscribe(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc));
        }

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        public void Subscribe<TEvent>(IEnumerable<Func<TEvent, bool>> eventHandlerFuncs) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlerFuncs)
                this.Subscribe(eventHandler);
        }

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        public void Subscribe<TEvent>(params Func<TEvent, bool>[] eventHandlerFuncs) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlerFuncs)
                this.Subscribe(eventHandler);
        }

        /// <summary>
        /// 取消事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">要取消的事件处理者</param>
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            lock (this.sync)
            {
                var eventType = typeof (TEvent);
                if (this._eventHandlers.ContainsKey(eventType))
                {
                    var handlers = this._eventHandlers[eventType];
                    if (handlers != null && handlers.Exists(s => this.eventHandlerEquals(s, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }

        /// <summary>
        /// 取消一系列的事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">要取消的事件处理者集合</param>
        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Unsubscribe(eventHandler);
        }

        /// <summary>
        /// 取消一系列的事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">要取消的事件处理者集合</param>
        public void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                this.Unsubscribe(eventHandler);
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFunc">事件处理程序委托</param>
        public void Unsubscribe<TEvent>(Action<TEvent> eventHandlerFunc) where TEvent : class, IEvent
        {
            this.Unsubscribe(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc));
        }

        /// <summary>
        /// 取消订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        public void Unsubscribe<TEvent>(IEnumerable<Func<TEvent, bool>> eventHandlerFuncs) where TEvent : class, IEvent
        {
            foreach (var eventHandlerFunc in eventHandlerFuncs)
                this.Unsubscribe(eventHandlerFunc);
        }

        /// <summary>
        /// 取消订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        public void Unsubscribe<TEvent>(params Func<TEvent, bool>[] eventHandlerFuncs) where TEvent : class, IEvent
        {
            foreach (var eventHandlerFunc in eventHandlerFuncs)
                this.Unsubscribe(eventHandlerFunc);
        }

        /// <summary>
        /// 取消订阅所有的指定类型的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        public void UnsubscribeAll<TEvent>() where TEvent : class, IEvent
        {
            lock (this.sync)
            {
                var eventType = typeof(TEvent);
                if (this._eventHandlers.ContainsKey(eventType))
                {
                    var handlers = this._eventHandlers[eventType];
                    if (handlers != null)
                        handlers.Clear();
                }
            }
        }

        /// <summary>
        /// 取消订阅所有的事件
        /// </summary>
        public void UnsubscribeAll()
        {
            lock (this.sync)
            {
                this._eventHandlers.Clear();
            }
        }

        /// <summary>
        /// 获取事件的订阅集合
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>订阅的事件</returns>
        public IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>() where TEvent : class, IEvent
        {
            var eventType = typeof(TEvent);
            lock (this.sync)
            {
                if (this._eventHandlers.ContainsKey(eventType))
                {
                    var handlers = this._eventHandlers[eventType];
                    if (handlers != null)
                        return handlers.OfType<IEventHandler<TEvent>>();
                
                    return null;
                }
            }
            
            return null;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="e">要发布的事件</param>
        public void Publish<TEvent>(TEvent e) where TEvent : class, IEvent
        {
            if (e == null)
                throw new ArgumentNullException("e");

            var eventType = e.GetType();
            var isExistsEventHandlers = this._eventHandlers.ContainsKey(eventType) &&
                                        this._eventHandlers[eventType] != null && this._eventHandlers[eventType].Any();
            if (!isExistsEventHandlers) 
                return;

            var handlers = this._eventHandlers[eventType];
            foreach (var eventHandler in handlers.OfType<IEventHandler<TEvent>>())
            {
                if (eventHandler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                    Task.Factory.StartNew((o) => eventHandler.Handle((TEvent)o), e);
                else
                    eventHandler.Handle(e);
            }
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="e">要发布的事件</param>
        /// <param name="callback">事件发布后的的回调函数，若 T2 为 true，表示发布成功；若 T2 为 false，表示发布失败，T3 为异常信息</param>
        /// <param name="timeout">发布事件超时时间，默认为 null</param>
        public void Publish<TEvent>(TEvent e, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class, IEvent
        {
            if (e == null)
                throw new ArgumentNullException("e");

            var eventType = e.GetType();
            var isExistsEventHandlers = this._eventHandlers.ContainsKey(eventType) &&
                                        this._eventHandlers[eventType] != null && this._eventHandlers[eventType].Any();
            if (isExistsEventHandlers)
            {
                var handlers = this._eventHandlers[eventType];
                var tasks = new List<Task>();
                try
                {
                    foreach (var eventHandler in handlers.OfType<IEventHandler<TEvent>>())
                    {
                        if (eventHandler.GetType().IsDefined(typeof(ParallelExecutionAttribute), false))
                            tasks.Add(Task.Factory.StartNew((o) => eventHandler.Handle((TEvent)o), e));
                        else
                            eventHandler.Handle(e);
                    }
                    if (tasks.Any())
                    {
                        if (timeout == null)
                            Task.WaitAll(tasks.ToArray());
                        else
                            Task.WaitAll(tasks.ToArray(), timeout.Value);
                    }
                    callback(e, true, null);
                }
                catch (Exception ex)
                {
                    callback(e, false, ex);
                }
            }
            else
                callback(e, false, null);
        }

        #endregion
    }
}
