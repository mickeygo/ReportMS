using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ReportMS.Web.Client.Tenancy;

namespace ReportMS.Web.Client.Attributes
{
    /// <summary>
    /// 对租户进行验证。
    /// 对于 View， 若没有租户信息，会跳转到 ~/Home/Index/ ;
    /// 对于 Ajax 请求或 ChildAction View，若没有租户信息，提示租户不存在或无效。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateTenantAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var owner = new TenantOwner(httpContext);
            var isExistTenant = owner.ExistTenant();
            return isExistTenant;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = "The tenant information is not exist or invalid.",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else if (filterContext.IsChildAction)
            {
                filterContext.Result = new ContentResult {Content = "The tenant info is not exist or invalid."};
            }
            else
            {
                var route = new RouteValueDictionary(new {area = "", controller = "Home", action = "Index"});
                filterContext.Result = new RedirectToRouteResult(route);
            }
        }
    }
}
