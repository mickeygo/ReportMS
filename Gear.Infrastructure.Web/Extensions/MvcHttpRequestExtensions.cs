using System;
using System.Web.Mvc;

namespace Gear.Infrastructure.Web.Extensions
{
    /// <summary>
    /// 基于 MVC 的 Http 请求扩展类
    /// </summary>
    public static class MvcHttpRequestExtensions
    {
        /// <summary>
        /// 获取 Mvc POST 请求中 CheckBox 值是否有被选中
        /// </summary>
        /// <param name="collection">表单值集合</param>
        /// <param name="name">要查询的表单值表单 name</param>
        /// <returns>true 表示已选中；false 表示没有选中</returns>
        public static bool HasCheckedValue(this FormCollection collection, string name)
        {
            var value = collection[name];
            return !("false".Equals(value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
