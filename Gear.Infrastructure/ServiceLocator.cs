using System;
using Gear.Infrastructure.Application;

namespace Gear.Infrastructure
{
    /// <summary>
    /// 服务定位器
    /// </summary>
    public sealed class ServiceLocator : IServiceLocator
    {
        #region Private Fields
        private static readonly IObjectContainer objectContainer = AppRuntime.Instance.CurrentApplication.ObjectContainer;
        private static readonly ServiceLocator instance = new ServiceLocator();
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>ServiceLocator</c>
        /// </summary>
        private ServiceLocator()
        { }

        #endregion

        #region IServiceLocator Members

        /// <summary>
        /// 获取当前的<c>ServiceLocator</c>
        /// </summary>
        public static ServiceLocator Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 获取指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">服务对象类型</typeparam>
        /// <returns>服务对象实例</returns>
        public T Resolve<T>() where T : class
        {
            return objectContainer.Resolve<T>();
        }

        /// <summary>
        /// 通过重写参数，获取指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">服务对象类型</typeparam>
        /// <param name="overridedArguments">用于获取服务的的参数</param>
        /// <returns>服务对象实例</returns>
        public T Resolve<T>(object overridedArguments) where T : class
        {
            return objectContainer.Resolve<T>(overridedArguments);
        }

        /// <summary>
        /// 通过重写参数，获取指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="overridedArguments">用于获取服务的的参数</param>
        /// <returns>服务对象实例</returns>
        public object Resolve(Type serviceType, object overridedArguments)
        {
            return objectContainer.Resolve(serviceType, overridedArguments);
        }

        /// <summary>
        /// 解析所有指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">要解析的服务对象</param>
        /// <returns><see cref="System.Array"/>所有解析的服务对象实例</returns>
        public Array ResolveAll(Type serviceType)
        {
            return objectContainer.ResolveAll(serviceType);
        }

        /// <summary>
        /// 解析所有指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">要解析的服务对象</typeparam>
        /// <returns><see cref="System.Array"/>所有解析的服务对象实例</returns>
        public T[] ResolveAll<T>() where T : class
        {
            return objectContainer.ResolveAll<T>();
        }

        /// <summary>
        /// 返回一个 Boolean 值，表示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <returns>真表示已经被注册，否则为假.</returns>
        public bool IsRegistered<T>()
        {
            return objectContainer.IsRegistered<T>();
        }

        /// <summary>
        /// 返回一个 Boolean 值，表示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <returns>真表示已经被注册，否则为假.</returns>
        public bool IsRegistered(Type type)
        {
            return objectContainer.IsRegistered(type);
        }

        #endregion

        #region IServiceProvider Members

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns>服务实例</returns>
        public object GetService(Type serviceType)
        {
            return objectContainer.GetService(serviceType);
        }

        #endregion
    }
}
