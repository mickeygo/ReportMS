using System;

namespace Gear.Infrastructure
{
    /// <summary>
    /// 表示实现类是服务定位器
    /// </summary>
    public interface IServiceLocator : IServiceProvider
    {
        /// <summary>
        /// 解析指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">服务对象类型</typeparam>
        /// <returns>服务对象实例</returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// 通过重写参数，解析指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">服务对象类型</typeparam>
        /// <param name="overridedArguments">用于获取服务的的参数</param>
        /// <returns>服务对象实例</returns>
        T Resolve<T>(object overridedArguments) where T : class;

        /// <summary>
        /// 通过重写参数，解析指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="overridedArguments">用于获取服务的的参数</param>
        /// <returns>服务对象实例</returns>
        object Resolve(Type serviceType, object overridedArguments);

        /// <summary>
        /// 解析所有指定类型的服务对象
        /// </summary>
        /// <param name="serviceType">要解析的服务对象</param>
        /// <returns><see cref="System.Array"/>所有解析的服务对象实例</returns>
        Array ResolveAll(Type serviceType);

        /// <summary>
        /// 解析所有指定类型的服务对象
        /// </summary>
        /// <typeparam name="T">要解析的服务对象</typeparam>
        /// <returns><see cref="System.Array"/>所有解析的服务对象实例</returns>
        T[] ResolveAll<T>() where T : class;

        /// <summary>
        /// 返回一个 Boolean 值，表示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <returns>真表示已经被注册，否则为假.</returns>
        bool IsRegistered<T>();

        /// <summary>
        /// 返回一个 Boolean 值，表示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <returns>真表示已经被注册，否则为假.</returns>
        bool IsRegistered(Type type);
    }
}
