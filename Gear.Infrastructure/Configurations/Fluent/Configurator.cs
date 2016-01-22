using System;
using System.Configuration;

namespace Gear.Infrastructure.Configurations.Fluent
{
    /// <summary>
    /// 配置器基类
    /// </summary>
    public abstract class Configurator<T> where T : ConfigurationSection, new()
    {
        #region Private Fields

        private readonly string _setion;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>Configurator</c>实例
        /// </summary>
        /// <param name="section">节点名称</param>
        protected Configurator(string section)
        {
            this._setion = section;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 获取配置信息, 若没有设置配置信息，会初始化配置实体
        /// </summary>
        /// <returns>要获取的配置信息类型</returns>
        protected T GetConfiguration()
        {
            try
            {
                var section = ConfigurationManager.GetSection(this._setion);
                if (section != null)
                    return (T) section;
            }
            catch (Exception)
            {
                // Suppress error
            }

            return new T();
        }

        #endregion
    }
}
