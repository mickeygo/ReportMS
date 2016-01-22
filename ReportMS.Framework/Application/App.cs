using System;
using System.Configuration;

namespace ReportMS.Framework.Application
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class App : IApp
    {
        #region Private Methods
        private void Initialize()
        {
            this.GetObjectContainer();
        }

        private void GetObjectContainer()
        {
            var assemblyTypeName = ConfigurationManager.AppSettings[ObjectContainer.ObjectContainerConfig];
            var objectType = Type.GetType(assemblyTypeName, true, true);
            if (objectType == null)
                throw new InfrastructureException("The provider type has not been defined in the ConfigSource.");
            var container = Activator.CreateInstance(objectType) as ObjectContainer;
            if (container == null)
                throw new InvalidCastException("The config 'objectContainer' can not convert to 'ObjectContainer' object.");
            this.ObjectContainer = container;
            this.ObjectContainer.InitializeFromCustomerConfigFile();
        }
        #endregion

        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected virtual void OnStart() 
        { }

        #region IApp Members

        /// <summary>
        /// 获取对象容器
        /// </summary>
        public ObjectContainer ObjectContainer { get; private set; }

        /// <summary>
        /// 程序启动
        /// </summary>
        public void Start()
        {
            this.Initialize();
            this.OnStart();
        }

        #endregion
    }
}
