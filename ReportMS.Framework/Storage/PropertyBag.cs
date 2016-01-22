using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReportMS.Framework.Storage
{
    /// <summary>
    /// 对象的属性包，包含一系列属性与属性指之间的映射集合
    /// </summary>
    public class PropertyBag : IEnumerable<KeyValuePair<string, object>>
    {
        #region Private Fields
        private readonly Dictionary<string, object> propertyValues = new Dictionary<string, object>();
        #endregion

        #region Public Static Fields
        /// <summary>
        /// 在给定对象上获取属性的绑定标志(Public | Instance | SetProperty | GetProperty)
        /// </summary>
        public static readonly BindingFlags PropertyBagBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化<c>PropertyBag</c>实例
        /// </summary>
        public PropertyBag()
        { }

        /// <summary>
        /// 初始化<c>PropertyBag</c>实例，通过给定的对象，填充内容属性包
        /// </summary>
        /// <param name="target">用于初始化属性包的目标对象</param>
        public PropertyBag(object target)
        {
            target
                .GetType()
                .GetProperties(PropertyBagBindingFlags)
                .ToList()
                .ForEach(pi => this.propertyValues.Add(pi.Name, pi.GetValue(target, null)));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 通过索引获取属性的值
        /// </summary>
        /// <param name="idx">索引</param>
        /// <returns>属性的值.</returns>
        public object this[string idx]
        {
            get { return this.propertyValues[idx]; }
            set { this.propertyValues[idx] = value; }
        }

        /// <summary>
        /// 获取属性包中的元素数量
        /// </summary>
        public int Count
        {
            get { return this.propertyValues.Count; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 清空属性包
        /// </summary>
        public void Clear()
        {
            this.propertyValues.Clear();
        }

        /// <summary>
        /// 向属性包中添加属性和属性的值
        /// </summary>
        /// <param name="propertyName">要添加的属性名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns>添加属性后的新实例</returns>
        public PropertyBag Add(string propertyName, object propertyValue)
        {
            this.propertyValues.Add(propertyName, propertyValue);
            return this;
        }

        /// <summary>
        /// 向属性包中添加属性，用于排序字段
        /// </summary>
        /// <typeparam name="T">字段的类型</typeparam>
        /// <param name="propertyName">要添加的属性名</param>
        /// <returns>添加排序字段属性后的新实例</returns>
        public PropertyBag AddSort<T>(string propertyName)
        {
            this.propertyValues.Add(propertyName, default(T));
            return this;
        }

        /// <summary>
        /// 获取当前属性包中属性
        /// </summary>
        /// <returns>当前属性包中属性</returns>
        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", propertyValues.Keys.Select(p => p)));
        }

        #endregion

        #region IEnumerator<KeyValuePair<string, object>> Members

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.propertyValues.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.propertyValues.GetEnumerator();
        }

        #endregion
    }
}
