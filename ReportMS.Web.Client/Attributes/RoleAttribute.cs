using System;
using System.Web;
using System.Web.Mvc;
using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Membership;
using ReportMS.Web.Client.Membership;


namespace ReportMS.Web.Client.Attributes
{
    /// <summary>
    /// 角色筛选。
    /// 默认情况下，允许的角色为 Administrator | Manager
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RoleAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            switch (this.RoleType)
            {
                case RoleType.Administrator:
                    return this.IsAdministrator();
                case RoleType.Manager:
                    return this.IsManager();
                default:
                    return this.IsAdministrator() || this.IsManager();
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = "You have not power to access this page.",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else if (filterContext.IsChildAction)
            {
                filterContext.Result = new ContentResult { Content = "You have not power to access this page." };
            }
            else
            {
                var controller = filterContext.Controller;

                var view = new ViewResult
                {
                    ViewName = "Permission",
                    MasterName = null,
                    ViewData = controller.ViewData,
                    TempData = controller.TempData,
                    ViewEngineCollection = ViewEngines.Engines
                };
                filterContext.Result = view;
            }
        }

        private bool IsAdministrator()
        {
            return MemberManager.IsCurrentLoginUserInAdministrator();
        }

        private bool IsManager()
        {
            var auth = OwinAuthenticationManager.OwinAuthentication;
            if (!auth.Identity.IsAuthenticated)
                return false;

            var userId = auth.NameIdentifier;
            if (!userId.HasValue)
                return false;

            return UserManager.Instance.IsAdmin(userId.Value);
        }

        /// <summary>
        /// 获取或设置系统角色类型
        /// </summary>
        public RoleType RoleType { get; set; }
    }

    /// <summary>
    /// 系统角色类型
    /// </summary>
    [Flags]
    public enum RoleType
    {
        /// <summary>
        /// 系统管理员角色
        /// </summary>
        Administrator = 1,

        /// <summary>
        /// 管理者角色
        /// </summary>
        Manager = 2
    }
}
