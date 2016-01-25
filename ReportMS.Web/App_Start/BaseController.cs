using System.Web.Mvc;
using Gear.Infrastructure.Web.Controllers;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.Tenancy;
using ReportMS.Web.Client.Membership;

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

        #region protected override events

        // 重写, 登录验证
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.DoAuthorization(filterContext);
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