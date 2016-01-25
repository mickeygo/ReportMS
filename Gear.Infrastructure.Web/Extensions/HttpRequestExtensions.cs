using System;
using System.Net;
using System.Text;
using System.Web;

namespace Gear.Infrastructure.Web.Extensions
{
    /// <summary>
    ///  Web 请求的扩展类
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取客户端的 IP 地址
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <returns>返回当前请求 Client 的 IP 地址</returns>
        public static string GetIP(this HttpRequest request)
        {
            var localIp = "127.0.0.1";

            if (request.IsLocal)
                return localIp;

            var result = request.UserHostAddress;
            if (String.IsNullOrEmpty(result))
                result = request.UserHostName;

            return String.IsNullOrEmpty(result) ? localIp : result;
        }

        /// <summary>
        /// 获取客户端的主机名。
        /// 若不能解析，则返回 null
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <returns>主机名</returns>
        public static string GetHostName(this HttpRequest request)
        {
            try
            {
                if (request.IsLocal)
                    return Dns.GetHostName();
                var ip = GetIP(request);
                return Dns.GetHostEntry(ip).HostName;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 Url 或 表单 中指定的参数
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <param name="name">要查询的参数名</param>
        /// <returns>要查询的参数值</returns>
        public static string GetVariable(this HttpRequest request, string name)
        {
            return request.QueryString[name] ?? request.Form[name];
        }

        /// <summary>
        /// 获取 Url 或 表单中参数字符串。
        /// 解析 GET 和 POST 请求中所有的 key / value 值
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <returns>GET 和 POST 请求中所有的值</returns>
        public static string GetAllVariable(this HttpRequest request)
        {
            var sb = new StringBuilder();

            var query = request.QueryString;
            if (query.HasKeys())
            {
                foreach (var key in query.AllKeys)
                    sb.AppendFormat("&{0}={1}", key, query[key]);
            }

            var form = request.Form;
            if (form.HasKeys())
            {
                foreach (var key in form.AllKeys)
                    sb.AppendFormat("&{0}={1}", key, form[key]);
            }

            return sb.Length != 0 ? sb.Remove(0, 1).ToString() : null;
        }

        /// <summary>
        /// 获取表单中 CheckBox 值是否有被选中
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <param name="name">表单 name</param>
        /// <returns>true 表示已选中；false 表示没有选中</returns>
        public static bool HasCheckedValue(this HttpRequest request, string name)
        {
            // checked ==> "true, false"
            // unchecked ==> "false"
            var value = GetVariable(request, name);
            return !("false".Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 返回上一个页面的地址。
        /// 若不存在，则为 null
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <param name="isEncode">是否对 Url 进行编码</param>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer(this HttpRequest request, bool isEncode = false)
        {
            var referrer = request.UrlReferrer;
            if (referrer == null)
                return null;

            return isEncode ? HttpUtility.UrlPathEncode(referrer.ToString()) : referrer.ToString();
        }

        /// <summary>
        /// 返回请求的 Url。
        /// 移除参数（不包含 ？ 及后面的参数值）
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <param name="isEncode">是否对 Url 进行编码</param>
        /// <returns>请求的 Ur</returns>
        public static string GetUrl(this HttpRequest request, bool isEncode = false)
        {
            var url = request.Url.ToString();
            var parameterPostion = url.IndexOf("?", StringComparison.Ordinal);
            var urlNotParam = parameterPostion >= 0 ? url.Substring(0, parameterPostion) : url;

            return isEncode ? HttpUtility.UrlPathEncode(urlNotParam) : url;
        }

        /// <summary>
        /// 得到完整主机头。
        /// 若当前站点不为默认的 80 端口，则加上端口
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <returns>完整主机头</returns>
        public static string GetFullHost(this HttpRequest request)
        {
            return !request.Url.IsDefaultPort ? string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString()) : request.Url.Host;
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <param name="request">Http 请求</param>
        /// <param name="isEncode">是否对 Url 进行编码</param>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl(this HttpRequest request, bool isEncode = false)
        {
            return isEncode ? HttpUtility.UrlPathEncode(request.RawUrl) : request.RawUrl;
        }
    }
}
