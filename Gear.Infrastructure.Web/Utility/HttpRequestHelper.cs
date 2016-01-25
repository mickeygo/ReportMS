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
    }
}
