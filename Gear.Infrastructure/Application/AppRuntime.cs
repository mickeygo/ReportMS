using System;
using System.Configuration;

namespace Gear.Infrastructure.Application
{
    /// <summary>
    /// 表示应用程序 创建、初始化 和 启动
    /// </summary>
    public sealed class AppRuntime
    {
        #region Private Fileds
        private static readonly AppRuntime instance = new AppRuntime();
        private static readonly object sync = new object();
        private const string appConfig = "application";
        #endregion

        #region Ctor

        private AppRuntime()
        {}

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取<c>AppRuntime</c>实例
        /// </summary>
        public static AppRuntime Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 初始化应用程序实例
        /// </summary>
        public static void Initialize()
        {
            lock (sync)
            {
                if (instance.CurrentApplication != null) 
                    return;

                lock (sync)
                {
                    var assemblyTypeName = GetAppConfigAssemblyName();
                    var objectType = Type.GetType(assemblyTypeName, true, true);
                    if (objectType == null)
                        throw new InfrastructureException("The provider type has not been defined in the ConfigSource.");
                    var application = Activator.CreateInstance(objectType) as IApp;
                    if (application == null)
                        throw new InfrastructureException("The application provider defined by type '{0}' doesn't exist.", objectType);
                    instance.CurrentApplication = application;
                }
            }
        }

        #endregion

        #region Private Method

        private static string GetAppConfigAssemblyName()
        {
            return ConfigurationManager.AppSettings[AppRuntime.appConfig];
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取当前应用程序
        /// </summary>
        public IApp CurrentApplication { get; private set; }

        #endregion
    }
}
