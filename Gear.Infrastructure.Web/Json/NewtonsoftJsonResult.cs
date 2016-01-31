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
        public NewtonsoftJsonResult()
        {
            this.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

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

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }
    }
}
