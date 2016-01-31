using System.Text;
using System.Web.Mvc;
using Gear.Infrastructure.Web.Controllers;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.Tenancy;
using ReportMS.Web.Client.Membership;
using ReportMS.Web.Client.Roles;

namespace ReportMS.Web
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    public abstract class BaseController : GearController
    {
        #region Private fields

        private TenantDto _tenant;
        private RoleDto _roleOfTenant;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取当前租户信息
        /// </summary>
        public TenantDto Tenant
        {
            get { return this._tenant ?? (this._tenant = this.GetTenant()); }
        }

        /// <summary>
        /// 获取登录的用户在当前租户中的角色的详细信息（多租户）
        /// </summary>
        public RoleDto RoleOfTenant
        {
            get { return this._roleOfTenant ?? (this._roleOfTenant = this.GetRoleOfTenant()); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 跳转到主页
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        /// <summary>
        /// Json 序列化，基于 Newtonsoft.Json 框架
        /// </summary>
        /// <param name="isSuccess">要传递的状态 status: success/fail</param>
        /// <param name="message">要传递的消息 message</param>
        /// <returns>ActionResult</returns>
        public ActionResult Json(bool isSuccess, string message = null)
        {
            return this.Json(isSuccess, message, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Json 序列化，基于 Newtonsoft.Json 框架
        /// </summary>
        /// <param name="isSuccess">要传递的状态 status: success/fail</param>
        /// <param name="message">要传递的消息 message</param>
        /// <param name="behavior">请求 Json 序列化的行为</param>
        /// <returns>ActionResult</returns>
        public ActionResult Json(bool isSuccess, string message, JsonRequestBehavior behavior)
        {
            return this.Json(isSuccess, null, message, behavior);
        }

        /// <summary>
        /// Json 序列化，基于 Newtonsoft.Json 框架
        /// </summary>
        /// <param name="isSuccess">要传递的状态 status: success/fail</param>
        /// <param name="data">要序列化的数据 data</param>
        /// <param name="message">要传递的消息 message</param>
        /// <returns>ActionResult</returns>
        public ActionResult Json(bool isSuccess, object data, string message = null)
        {
            return this.Json(isSuccess, data, message, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Json 序列化，基于 Newtonsoft.Json 框架
        /// </summary>
        /// <param name="isSuccess">要传递的状态 status: success/fail</param>
        /// <param name="data">要序列化的数据 data</param>
        /// <param name="message">要传递的消息 message</param>
        /// <param name="behavior">请求 Json 序列化的行为</param>
        /// <returns>ActionResult</returns>
        public ActionResult Json(bool isSuccess, object data, string message, JsonRequestBehavior behavior)
        {
            var status = isSuccess ? "success" : "fail";
            return this.NewtonsoftJson(new { status, data, message }, behavior);
        }

        #endregion

        #region protected override events

        // 重写, 登录验证
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Authentication
            if (!this.Authenticate(filterContext))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            // Administrator


            // Authorization
            var action = this.RouteData.Values["action"] as string;
            var controller = this.RouteData.Values["controller"] as string;
            //var area = this.RouteData.DataTokens["area"] as string;

            //var hasPermission = RoleManager.Instance.HasActionOfRole(this.RoleOfTenant.ID, action, controller);
            //if (!hasPermission)
            //    filterContext.Result = View("Permission");
        }

        // 重写，登录 Log 记录
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        // 重写，异常处理
        protected override void OnException(ExceptionContext filterContext)
        {
            
        }

        #endregion

        #region Private Methods

        private TenantDto GetTenant()
        {
            var tenantOwer = new TenantOwner(this.RouteData);
            return tenantOwer.GetCurrentTenant();
        }

        private RoleDto GetRoleOfTenant()
        {
            if (this.Tenant == null)
                return null;

            var userId = this.LoginUser.NameIdentifier;
            if (!userId.HasValue)
                return null;

            var tenantId = this.Tenant.ID;
            return UserManager.Instance.GetRoleOfTenant(userId.Value, tenantId);
        }

        #endregion
    }
}