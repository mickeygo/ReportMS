using System;
using System.Text;
using System.Web.Mvc;
using Gear.Utility.Serialization;

namespace Gear.Infrastructure.Web.Json
{
    /// <summary>
    /// 基于 NewtonsoftJson 组件的 Json 序列化对象
    /// </summary>
    public class NewtonsoftJsonResult : ActionResult
    {
        /// <summary>
        /// 初始化一个新的<c>NewtonsoftJsonResult</c>实例
        /// </summary>
        public NewtonsoftJsonResult()
        {
            this.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        /// <summary>
        /// override, 重写执行结果
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) 
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("The request http method is denied.");
            }

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (this.Data != null)
            {
                var json = new NewtonsoftJsonSerializer();
                response.Write(json.Serialize(this.Data));
            }
        }

        /// <summary>
        /// 获取或设置要序列化的内容在 Response 输出时的编码
        /// </summary>
        public Encoding ContentEncoding { get; set; }

        /// <summary>
        /// 获取或设置要响应的内容的 Http MIME 类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 获取或设置要序列化的内容
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 获取或设置 Json 请求行为, 默认为 DenyGet
        /// </summary>
        public JsonRequestBehavior JsonRequestBehavior { get; set; }
    }
}
