using System;
using System.Collections.Generic;
using System.Linq;

namespace Gear.Infrastructure.Application
{
    /// <summary>
    /// 程序启动时的引导程序
    /// 用此类来注册程序第一次启动时要加载的对象类型
    /// </summary>
    public class AppBootstrapper
    {
        private readonly object sync = new object();
        private readonly List<Type> apps = new List<Type>();
        private static readonly AppBootstrapper bootstrapper = new AppBootstrapper();

        private AppBootstrapper()
        {
        }

        private void RegisterApplication<T>() where T : IApplicationStartup, new()
        {
            lock (this.sync)
            {
                this.apps.Add(typeof (T));
            }
        }

        private void ResloveAll()
        {
            lock (this.sync)
            {
                foreach (var obj in apps.Select(Activator.CreateInstance).OfType<IApplicationStartup>())
                {
                    obj.Initialize();
                }
            }
        }

        /// <summary>
        /// 注册引导程序
        /// </summary>
        /// <typeparam name="T">引导程序类型</typeparam>
        public static void Register<T>() where T : IApplicationStartup, new()
        {
            bootstrapper.RegisterApplication<T>();
        }

        /// <summary>
        /// 解析所有注册的对象（内部使用）
        /// </summary>
        internal static void Reslove()
        {
            bootstrapper.ResloveAll();
        }
    }
}
