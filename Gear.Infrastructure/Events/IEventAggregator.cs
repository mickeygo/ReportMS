using System;
using System.Collections.Generic;

namespace Gear.Infrastructure.Events
{
    /// <summary>
    /// 表示实现接口的类为实际聚合器
    /// 订阅事件 Subscribe；取消订阅 Unsubscribe；发布事件 Publish
    /// 关于 Event Aggregator 模式，可参见 https://msdn.microsoft.com/en-us/library/ff921122.aspx
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">事件处理者</param>
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集合</param>
        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">事件处理者集合</param>
        void Subscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFunc">事件处理程序委托</param>
        void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        void Subscribe<TEvent>(IEnumerable<Func<TEvent, bool>> eventHandlerFuncs)
            where TEvent : class, IEvent;

        /// <summary>
        /// 订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        void Subscribe<TEvent>(params Func<TEvent, bool>[] eventHandlerFuncs)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandler">要取消的事件处理者</param>
        void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消一系列的事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">要取消的事件处理者集合</param>
        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消一系列的事件订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlers">要取消的事件处理者集合</param>
        void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] eventHandlers)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFunc">事件处理程序委托</param>
        void Unsubscribe<TEvent>(Action<TEvent> eventHandlerFunc)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        void Unsubscribe<TEvent>(IEnumerable<Func<TEvent, bool>> eventHandlerFuncs)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消订阅一系列事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventHandlerFuncs">事件处理程序委托集</param>
        void Unsubscribe<TEvent>(params Func<TEvent, bool>[] eventHandlerFuncs)
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消订阅所有的指定类型的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnsubscribeAll<TEvent>()
            where TEvent : class, IEvent;

        /// <summary>
        /// 取消订阅所有的事件
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 获取事件的订阅集合
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>订阅的事件</returns>
        IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>()
            where TEvent : class, IEvent;

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="e">要发布的事件</param>
        void Publish<TEvent>(TEvent e)
            where TEvent : class, IEvent;

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="e">要发布的事件</param>
        /// <param name="callback">事件发布后的的回调函数，若 T2 为 true，表示发布成功；若 T2 为 false，表示发布失败，T3 为异常信息</param>
        /// <param name="timeout">发布事件超时时间，默认为 null</param>
        void Publish<TEvent>(TEvent e, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TEvent : class, IEvent;
    }
}
