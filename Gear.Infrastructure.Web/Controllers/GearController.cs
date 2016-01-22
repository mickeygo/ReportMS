using System;
using System.Web.Mvc;
using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Membership;
using Gear.Infrastructure.Web.MultiTenancy;
using Gear.Infrastructure.Web.Outputs;

namespace Gear.Infrastructure.Web.Controllers
{
    /// <summary>
    /// ASP.NET MVC 控制器基类
    /// </summary>
    public abstract class GearController : Controller
    {
        #region Public Properties

        /// <summary>
        /// 获取租户名称
        /// </summary>
        public string TenantName
        {
            get { return this.GetTenantName(); }
        }

        /// <summary>
        /// 当前登录人员是否是系统管理者成员
        /// </summary>
        public bool IsAdministrator
        {
            get { return MemberManager.IsCurrentLoginUserInAdministrator(); }
        }

        /// <summary>
        /// 导出文件, 通过 Http Response OutPutStream 流中文件输出
        /// </summary>
        public IOutput Output
        {
            get { return new MvcFileOutput(this.ControllerContext); }
        }

        #endregion

        #region Protected Methods

        protected virtual void DoAuthorization(AuthorizationContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            var controllerDescriptor = actionDescriptor.ControllerDescriptor;

            if (actionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || controllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            var authentication = OwinAuthenticationManager.OwinAuthentication;

            // authentication
            if (!authentication.Identity.IsAuthenticated)
            {
                var requestUrl = filterContext.HttpContext.Request.RawUrl ?? String.Empty;
                filterContext.Result = this.RedirectToAction("Login", "Account", new { area = String.Empty, returnUrl = requestUrl });
                
                return;
            }

            // authorization
            var action = this.RouteData.Values["action"] as string;
            var controller = this.RouteData.Values["controller"] as string;
            var area = this.RouteData.DataTokens["area"] as string;

        }

        #endregion

        #region Private Methods

        private string GetTenantName()
        {
            var tennant = new TenantManager(this.RouteData);
            return tennant.GetCurrentTenant();
        }

        #endregion
    }
}
