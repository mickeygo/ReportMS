using System.Web.Mvc;
using Gear.Infrastructure.Web.Authorization;

namespace Gear.Infrastructure.Web.Controllers
{
    /// <summary>
    /// ASP.NET MVC Controller 扩展类
    /// </summary>
    public static class ControllerExtensions
    {
        #region Public Methods

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="filterContext">授权上下文</param>
        /// <returns>true 验证通过，否则为 false</returns>
        public static bool Authenticate(this Controller controller, AuthorizationContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            if (IsAllowAnonymousOfActionOrController(actionDescriptor))
                return true;

            var authentication = OwinAuthenticationManager.OwinAuthentication;
            return authentication.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="filterContext">授权上下文</param>
        /// <param name="limitAction">是否访问权限限制到 Action，默认为 false</param>
        /// <returns>true 通过授权，否则为 false</returns>
        public static bool Authorise(this Controller controller, AuthorizationContext filterContext, bool limitAction = false)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            if (IsAllowAnonymousOfActionOrController(actionDescriptor))
                return true;

            var m_action = controller.RouteData.Values["action"] as string;
            var m_controller = controller.RouteData.Values["controller"] as string;
            var m_area = controller.RouteData.DataTokens["area"] as string;

            return true;
        }

        #endregion

        #region Private Methods

        private static bool IsAllowAnonymousOfActionOrController(ActionDescriptor actionDescriptor, bool inherit = true)
        {
            var controllerDescriptor = actionDescriptor.ControllerDescriptor;

            return actionDescriptor.IsDefined(typeof (AllowAnonymousAttribute), inherit)
                   || controllerDescriptor.IsDefined(typeof (AllowAnonymousAttribute), inherit);
        }

        #endregion
    }
}
