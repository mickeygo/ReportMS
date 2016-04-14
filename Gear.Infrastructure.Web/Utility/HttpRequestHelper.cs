using System.Web;
using Gear.Infrastructure.Web.Extensions;

namespace Gear.Infrastructure.Web.Utility
{
    /// <summary>
    /// Http 请求辅助类
    /// </summary>
    public static class HttpRequestHelper
    {
        /// <summary>
        /// 获取客户端的 IP 地址
        /// </summary>
        public static string GetClientHostIP()
        {
            return HttpContext.Current.Request.GetIP();
        }

        /// <summary>
        /// 获取当前 Web 请求中的变量值。
        /// 值来源于 QueryString，Form，Cookie 或 ServerVariables
        /// </summary>
        /// <param name="variable">变量</param>
        /// <returns>变量的值</returns>
        public static string GetVariable(string variable)
        {
            return HttpContext.Current.Request[variable];
        }

        /// <summary>
        /// 获取当前 Web 请求中的变量值。
        /// 值来源于 QueryString 或 Form . 优先从 QueryString 中查找
        /// </summary>
        /// <param name="variable">变量</param>
        /// <returns>变量的值</returns>
        public static string GetVariableFromQueryStringOrForm(string variable)
        {
            var request = HttpContext.Current.Request;
            return request.QueryString[variable] ?? request.Form[variable];
        }
    }
}
