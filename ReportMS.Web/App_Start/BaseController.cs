using System.Web.Mvc;
using Gear.Infrastructure.Web.Controllers;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Web.Client.Tenancy;

namespace ReportMS.Web
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    public abstract class BaseController : GearController
    {
        #region Private fields

        private TenantDto _tenant;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取当前租户信息
        /// </summary>
        public TenantDto Tenant
        {
            get { return this._tenant ?? (this._tenant = this.GetTenant()); }
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

        #endregion
    }
}