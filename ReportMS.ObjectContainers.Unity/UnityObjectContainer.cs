using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Gear.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ReportMS.ObjectContainers.Unity
{
    /// <summary>
    /// Microsoft.Practices.Unity Ioc/DI 容器
    /// </summary>
    public class UnityObjectContainer : ObjectContainer
    {
        #region Private Fileds
        private readonly IUnityContainer container;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化 <c>UnityObjectContainer</c> 对象
        /// </summary>
        public UnityObjectContainer()
        {
            this.container = new UnityContainer();
        }
        #endregion

        #region Protected Methods

        protected override object DoGetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }

        protected override object DoGetService(Type serviceType, object overridedArguments)
        {
            var overrides = new List<ParameterOverride>();
            var argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new ParameterOverride(propertyName, propertyValue));
                });
            return container.Resolve(serviceType, overrides.ToArray());
        }

        protected override Array DoResolveAll(Type serviceType)
        {
            return container.ResolveAll(serviceType).ToArray();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从 web / app config 文件中用默认的节点名（unity）初始化 Unity 容器
        /// </summary>
        public override void InitializeFromConfigFile()
        {
            this.InitializeFromConfigFile(UnityConfigurationSection.SectionName);
        }

        /// <summary>
        /// 从 web / app config 文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        /// <param name="configSectionName"></param>
        public override void InitializeFromConfigFile(string configSectionName)
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection(configSectionName);
            section.Configure(this.container);
        }

        /// <summary>
        /// 从自定义的配置文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        public override void InitializeFromCustomerConfigFile()
        {
            this.InitializeFromCustomerConfigFile(this.ObjectContainerFileNameConfig, this.ObjectContainerSectionNameConfig);
        }

        /// <summary>
        /// 从自定义的配置文件中用指定的节点名初始化 Unity 容器
        /// </summary>
        /// <param name="configFileName">配置文件名</param>
        /// <param name="configSectionName">配置节点名</param>
        public override void InitializeFromCustomerConfigFile(string configFileName, string configSectionName)
        {
            var configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);
            var configmap = new ExeConfigurationFileMap { ExeConfigFilename = configFile };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configmap, ConfigurationUserLevel.None);
            var section = (UnityConfigurationSection)configuration.GetSection(configSectionName);
            section.Configure(this.container);
        }

        /// <summary>
        /// 获取被包装的容器的实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T GetWrappedContainer<T>()
        {
            if (typeof(T) == typeof(UnityContainer))
                return (T)this.container;
            throw new InfrastructureException("The wrapped container type provided by the current object container should be '{0}'.", typeof(UnityContainer));
        }

        /// <summary>
        /// 返回一个 Boolean 值，表示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <typeparam name="T">检查的类型</typeparam>
        /// <returns>真表示已经被注册，否则为假.</returns>
        public override bool IsRegistered<T>()
        {
            return this.container.IsRegistered<T>();
        }

        /// <summary>
        /// 返回一个 Boolean 值，表示是否给定的类型已经被服务定位器注册
        /// </summary>
        /// <param name="type">检查的类型</param>
        /// <returns>真表示已经被注册，否则为假.</returns>
        public override bool IsRegistered(Type type)
        {
            return this.container.IsRegistered(type);
        }

        /// <summary>
        /// 重写，获取容器对象配置文件路径，路径为 "Configs\unity.config"
        /// </summary>
        public override string ObjectContainerFileNameConfig
        {
            get { return @"Configs\unity.config"; }
        }

        /// <summary>
        /// 重写，获取容器对象配置的节点名，节点为 "unity"
        /// </summary>
        public override string ObjectContainerSectionNameConfig
        {
            get { return @"unity"; }
        }

        #endregion
    }
}
