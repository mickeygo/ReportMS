using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReportMS.Framework.Config;

namespace ReportMS.Framework.Bus
{
    /// <summary>
    /// 消息分发器
    /// </summary>
    public class MessageDispatcher : IMessageDispatcher
    {
        #region Private Fields
        private readonly Dictionary<Type, List<object>> handlers = new Dictionary<Type, List<object>>();
        #endregion

        #region Private Methods

        /// <summary>
        ///  将指定的消息处理程序类型注册到消息分发器
        /// </summary>
        /// <param name="messageDispatcher">消息派发器实例</param>
        /// <param name="handlerType">要注册的类型</param>
        private static void RegisterType(IMessageDispatcher messageDispatcher, Type handlerType)
        {
            var methodInfo = messageDispatcher.GetType().GetMethod("Register", BindingFlags.Public | BindingFlags.Instance);

            var handlerIntfTypeQuery = from p in handlerType.GetInterfaces()
                                       where p.IsGenericType && p.GetGenericTypeDefinition() == typeof (IHandler<>)
                                       select p;

            foreach (var handlerIntfType in handlerIntfTypeQuery)
            {
                var handlerInstance = Activator.CreateInstance(handlerType);
                var messageType = handlerIntfType.GetGenericArguments().First();
                var genericMethodInfo = methodInfo.MakeGenericMethod(messageType);
                genericMethodInfo.Invoke(messageDispatcher, new[] {handlerInstance});
            }
        }

        /// <summary>
        /// 在给定程序集内将所有处理程序类型注册到消息分发器
        /// </summary>
        /// <param name="messageDispatcher">消息派发器实例</param>
        /// <param name="assembly">程序集</param>
        private static void RegisterAssembly(IMessageDispatcher messageDispatcher, Assembly assembly)
        {
            var types = from type in assembly.GetExportedTypes()
                        let intfs = type.GetInterfaces()
                        where intfs.Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == typeof (IHandler<>)) &&
                              intfs.Any(p => p.IsDefined(typeof (RegisterDispatchAttribute), true))
                        select type;

            foreach (var type in types)
            {
                RegisterType(messageDispatcher, type);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 当消息分发程序将发送消息时发生
        /// </summary>
        /// <param name="e">事件数据</param>
        protected virtual void OnDispatching(MessageDispatchEventArgs e)
        {
            var temp = this.Dispatching;
            if (temp != null)
                temp(this, e);
        }

        /// <summary>
        /// 当消息分发程序发送消息失败时发生
        /// </summary>
        /// <param name="e">事件数据</param>
        protected virtual void OnDispatchFailed(MessageDispatchEventArgs e)
        {
            var temp = this.DispatchFailed;
            if (temp != null)
                temp(this, e);
        }

        /// <summary>
        /// 当消息分发程序将发送消息完成时发生
        /// </summary>
        /// <param name="e">事件数据</param>
        protected virtual void OnDispatched(MessageDispatchEventArgs e)
        {
            var temp = this.Dispatched;
            if (temp != null)
                temp(this, e);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a message dispatcher and registers all the message handlers
        /// specified in the <see cref="IConfigSource"/> instance.
        /// 创建一个消息分发器，并注册所有的消息处理程序
        /// </summary>
        /// <param name="configSource">The <see cref="IConfigSource"/> instance
        /// that contains the definitions for message handlers.</param>
        /// <param name="messageDispatcherType">消息分发器类型</param>
        /// <param name="args">用于初始化消息分发器的参数</param>
        /// <returns><see cref="IMessageDispatcher"/> 实例</returns>
        public static IMessageDispatcher CreateAndRegister(IConfigSource configSource, Type messageDispatcherType, params object[] args)
        {
            var messageDispatcher = (IMessageDispatcher)Activator.CreateInstance(messageDispatcherType, args);

            // TODO: MessageDispatcher CreateAndRegister

            //HandlerElementCollection handlerElementCollection = configSource.Config.Handlers;
            //foreach (HandlerElement handlerElement in handlerElementCollection)
            //{
            //    switch (handlerElement.SourceType)
            //    {
            //        case HandlerSourceType.Type:
            //            string typeName = handlerElement.Source;
            //            Type handlerType = Type.GetType(typeName);
            //            RegisterType(messageDispatcher, handlerType);
            //            break;
            //        case HandlerSourceType.Assembly:
            //            string assemblyString = handlerElement.Source;
            //            Assembly assembly = Assembly.Load(assemblyString);
            //            RegisterAssembly(messageDispatcher, assembly);
            //            break;
            //    }
            //}

            return messageDispatcher;
        }

        #endregion

        #region IMessageDispatcher Members

        /// <summary>
        /// 清空注册的消息处理程序
        /// </summary>
        public virtual void Clear()
        {
            this.handlers.Clear();
        }

        /// <summary>
        /// 派发消息
        /// </summary>
        /// <param name="message">要派发的消息</param>
        public virtual void DispatchMessage<T>(T message)
        {
            var messageType = typeof(T);
            if (this.handlers.ContainsKey(messageType))
            {
                var messageHandlers = handlers[messageType];
                foreach (var messageHandler in messageHandlers)
                {
                    var dynMessageHandler = (IHandler<T>)messageHandler;
                    var evtArgs = new MessageDispatchEventArgs(message, messageHandler.GetType(), messageHandler);
                    this.OnDispatching(evtArgs);
                    try
                    {
                        dynMessageHandler.Handle(message);
                        this.OnDispatched(evtArgs);
                    }
                    catch
                    {
                        this.OnDispatchFailed(evtArgs);
                    }
                }
            }
        }

        /// <summary>
        /// 将指定的消息处理程序注册到消息分发器
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">要被注册的处理程序</param>
        public virtual void Register<T>(IHandler<T> handler)
        {
            var keyType = typeof(T);

            if (handlers.ContainsKey(keyType))
            {
                var registeredHandlers = handlers[keyType];
                if (registeredHandlers != null)
                {
                    if (!registeredHandlers.Contains(handler))
                        registeredHandlers.Add(handler);
                }
                else
                {
                    registeredHandlers = new List<object> {handler};
                    handlers.Add(keyType, registeredHandlers);

                }
            }
            else
            {
                var registeredHandlers = new List<object> {handler};
                handlers.Add(keyType, registeredHandlers);
            }
        }

        /// <summary>
        /// 注销指定的消息处理程序
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">要被注销的处理程序</param>
        public virtual void UnRegister<T>(IHandler<T> handler)
        {
            var keyType = typeof(T);
            if (handlers.ContainsKey(keyType) &&
                handlers[keyType] != null &&
                handlers[keyType].Count > 0 &&
                handlers[keyType].Contains(handler))
            {
                handlers[keyType].Remove(handler);
            }
        }

        /// <summary>
        /// 当消息分发程序将发送消息时发生
        /// </summary>
        public event EventHandler<MessageDispatchEventArgs> Dispatching;

        /// <summary>
        /// 当消息分发程序发送消息失败时发生
        /// </summary>
        public event EventHandler<MessageDispatchEventArgs> DispatchFailed;

        /// <summary>
        /// 当消息分发程序将发送消息完成时发生
        /// </summary>
        public event EventHandler<MessageDispatchEventArgs> Dispatched;

        #endregion
    }
}
