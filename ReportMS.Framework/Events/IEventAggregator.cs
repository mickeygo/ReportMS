using System;
using System.Collections.Generic;

namespace ReportMS.Framework.Events
{
    /// <summary>
    /// 表示实现类是事件聚合器
    /// </summary>
    /// <remarks>关于 EventAggregator 模式，可参见 http://msdn.microsoft.com/en-us/library/ff921122(v=pandp.20).aspx </remarks>
    public interface IEventAggregator
    {
        /// <summary>
        /// 订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">事件处理者</param>
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集</param>
        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者</param>
        void Subscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅 <see cref="Action{T}"/> 委托.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerAction"><see cref="Action"/> 委托</param>
        void Subscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅 <see cref="Action{T}"/> 委托.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action"/> 委托集</param>
        void Subscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅 <see cref="Action{T}"/> 委托.
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action"/> 委托</param>
        void Subscribe<TEvent>(params Action<TEvent>[] eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅的事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">事件处理者</param>
        void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅的事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集</param>
        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅的事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者</param>
        void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅的<see cref="Action{T}"/>委托
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerAction"><see cref="Action{T}"/> 委托</param>
        void Unsubscribe<TEvent>(Action<TEvent> eventHandlerAction)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅的<see cref="Action{T}"/>委托
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 委托</param>
        void Unsubscribe<TEvent>(IEnumerable<Action<TEvent>> eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅的<see cref="Action{T}"/>委托
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerActions"><see cref="Action{T}"/> 委托</param>
        void Unsubscribe<TEvent>(params Action<TEvent>[] eventHandlerActions)
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅所有的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnsubscribeAll<TEvent>()
            where TEvent : class, IEvent;

        /// <summary>
        /// 注销订阅所有的在事件聚合器的所有事件
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 获取订阅事件处理程序
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>处理程序集合</returns>
        IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>()
            where TEvent : class, IEvent;

        /// <summary>
        /// 将事件发布到所有的订阅者
        /// </summary>
        /// <typeparam name="TEvent">要发布的事件类型</typeparam>
        /// <param name="event">要发布的事件</param>
        void Publish<TEvent>(TEvent @event)
            where TEvent : class, IEvent;

        /// <summary>
        /// 将事件发布到所有的订阅者
        /// </summary>
        /// <typeparam name="TEvent">要发布的事件类型</typeparam>
        /// <param name="event">要发布的事件</param>
        /// <param name="callback">在事件被发布且处理后执行的回调方法</param>
        /// <param name="timeout">表示处理事件的超时时间段</param>
        void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TEvent : class, IEvent;
    }
}
