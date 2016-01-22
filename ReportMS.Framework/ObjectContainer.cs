using System;
using System.Linq;

namespace ReportMS.Framework
{
    /// <summary>
    /// 表示对象容器的基类
    /// </summary>
    public abstract class ObjectContainer : IObjectContainer
    {
        /// <summary>
        /// 配置文件中容器节点名
        /// </summary>
        public const string ObjectContainerConfig = "container";

        #region Private Methods
        
        private object GetProxyObject(Type targetType, object targetObject)
        {
            // TODO: Using DynamicProxy
            return targetObject;
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// 获取指定的服务对象实例
        /// </summary>
        /// <typeparam name="T">要获取的服务对象类型</typeparam>
        /// <returns></returns>
        protected virtual T DoGetService<T>() where T : class
        {
            return this.DoGetService(typeof(T)) as T;
        }

        /// <summary>
        /// 获取指定的服务对象实例
        /// </summary>
        /// <typeparam name="T">要获取的服务对象类型</typeparam>
        /// <param name="overridedArguments">在获取服务时应用的覆盖参数</param>
        /// <returns></returns>
        protected virtual T DoGetService<T>(object overridedArguments) where T : class
        {
            return this.DoGetService(typeof(T), overridedArguments) as T;
        }

        /// <summary>
        /// 获取指定的服务对象实例
        /// </summary>
        /// <param name="serviceType">要获取的服务对象类型</param>
        /// <returns></returns>
        protected abstract object DoGetService(Type serviceType);

        /// <summary>
        /// 获取指定的服务对象实例
        /// </summary>
        /// <param name="serviceType">要获取的服务对象类型</param>
        /// <param name="overridedArguments">在获取服务时应用的覆盖参数</param>
        /// <returns></returns>
        protected abstract object DoGetService(Type serviceType, object overridedArguments);

        /// <summary>
        /// 解析指定类型的所有对象
        /// </summary>
        /// <param name="serviceType">要解析的服务类型</param>
        /// <returns></returns>
        protected abstract Array DoResolveAll(Type serviceType);

        /// <summary>
        /// 解析指定类型的所有对象
        /// </summary>
        /// <typeparam name="T">要解析的类型</typeparam>
        /// <returns></returns>
        protected virtual T[] DoResolveAll<T>() where T : class
        {
            var orginal = this.DoResolveAll(typeof(T));
            return orginal.Cast<T>().ToArray();
        }
        #endregion

        #region IObjectContainer Members

        /// <summary>
        /// 用 application/web 配置文件初始化对象容器
        /// </summary>
        public abstract void InitializeFromConfigFile();

        /// <summary>
        /// 用 application/web 配置文件初始化对象容器
        /// </summary>
        /// <param name="configSectionName">用于初始化对象容器的 application/web 配置文件名</param>
        public abstract void InitializeFromConfigFile(string configSectionName);

        /// <summary>
        /// 从自定义的配置文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        public abstract void InitializeFromCustomerConfigFile();

        /// <summary>
        /// 从自定义的配置文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        /// <param name="configFileName">配置文件名</param>
        /// <param name="configSectionName">配置节点名</param>
        public abstract void InitializeFromCustomerConfigFile(string configFileName, string configSectionName);

        /// <summary>
        /// 获取被包装的容器实例
        /// </summary>
        /// <typeparam name="T">这个被包装的容器类型</typeparam>
        /// <returns></returns>
        public abstract T GetWrappedContainer<T>();

        #endregion

        #region IServiceLocator Members

        /// <summary>
        /// 获取指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">服务对象类型</typeparam>
        /// <returns>服务对象实例</returns>

        public T Resolve<T>() where T : class
        {
            var serviceImpl = this.DoGetService<T>();
            return (T)this.GetProxyObject(typeof(T), serviceImpl);
        }

        /// <summary>
        /// 通过重写参数，获取指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">服务对象类型</typeparam>
        /// <param name="overridedArguments">用于获取服务的的参数</param>
        /// <returns>服务对象实例</returns>
        public T Resolve<T>(object overridedArguments) where T : class
        {
            var serviceImpl = this.DoGetService<T>(overridedArguments);
            return (T)this.GetProxyObject(typeof(T), serviceImpl);
        }

        /// <summary>
        /// 通过重写参数，获取指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="overridedArguments">用于获取服务的的参数</param>
        /// <returns>服务对象实例</returns>
        public object Resolve(Type serviceType, object overridedArguments)
        {
            var serviceImpl = this.DoGetService(serviceType, overridedArguments);
            return this.GetProxyObject(serviceType, serviceImpl);
        }

        /// <summary>
        /// 解析所有指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">要解析的服务对象</param>
        /// <returns><see cref="System.Array"/>所有解析的服务对象实例</returns>
        public Array ResolveAll(Type serviceType)
        {
            var serviceImpls = this.DoResolveAll(serviceType);
            return (from object serviceImpl in serviceImpls
                    select this.GetProxyObject(serviceType, serviceImpl)).ToArray();
        }

        /// <summary>
        /// 解析所有指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">要解析的服务对象</typeparam>
        /// <returns><see cref="System.Array"/>所有解析的服务对象实例</returns>
        public T[] ResolveAll<T>() where T : class
        {
            var serviceImpls = this.DoResolveAll<T>();
            return (from object serviceImpl in serviceImpls
                    select (T)this.GetProxyObject(typeof(T), serviceImpl)).ToArray();
        }

        /// <summary>
        /// 返回一个 Boolean 值，显示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <returns>真表示已经被注册，否则为假.</returns>
        public abstract bool Registered<T>();

        /// <summary>
        /// 返回一个 Boolean 值，显示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <returns>真表示已经被注册，否则为假.</returns>
        public abstract bool Registered(Type type);

        #endregion

        #region IServiceProvider Members

        /// <summary>
        /// 获取指定的服务对象实例
        /// </summary>
        /// <param name="serviceType">要获取的服务对象类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            var serviceImpl = this.DoGetService(serviceType);
            return this.GetProxyObject(serviceType, serviceImpl);
        }

        #endregion

        #region Proterties

        /// <summary>
        /// 获取容器对象配置文件路径
        /// </summary>
        public virtual string ObjectContainerFileNameConfig
        {
            get { return @"Container.config"; }
        }

        /// <summary>
        /// 获取容器对象配置的节点名
        /// </summary>
        public virtual string ObjectContainerSectionNameConfig
        {
            get { return @"objectContainer"; }
        }

        #endregion
    }
}
