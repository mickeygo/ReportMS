using System;
using System.Web;
using System.Web.Routing;
using Gear.Infrastructure;
using Gear.Infrastructure.Web.MultiTenancy;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Client.Tenancy
{
    /// <summary>
    /// 租户拥有者。
    /// 用来检索当前租户名及其租户信息
    /// </summary>
    public class TenantOwner
    {
        private readonly TenantManager tenantManager;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>TenantOwner</c>实例。
        /// 使用当前的 HttpContext 对象来获取路由
        /// </summary>
        public TenantOwner()
        {
            var httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(httpContextWrapper);
            this.tenantManager = new TenantManager(routeData);
        }

        /// <summary>
        /// 初始化一个新的<c>TenantOwner</c>实例
        /// </summary>
        /// <param name="httpContext">用来获取路由的 Http上下文</param>
        public TenantOwner(HttpContextBase httpContext)
        {
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            this.tenantManager = new TenantManager(routeData);
        }

        /// <summary>
        /// 初始化一个新的<c>TenantOwner</c>实例
        /// </summary>
        /// <param name="route">路由</param>
        public TenantOwner(RouteData route)
        {
            this.tenantManager = new TenantManager(route);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 验证是否有租户请求且请求的租户信息是否有效
        /// </summary>
        /// <returns>True 表示存在租户；否则为 False</returns>
        public bool ExistTenant()
        {
            var tenantName = this.CurrentTenantName;
            if (String.IsNullOrWhiteSpace(tenantName))
                return false;

            using (var tenantService = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                return tenantService.ExistTenant(tenantName);
            }
        }

        /// <summary>
        /// 获取当前租户信息。
        /// 若不存在租户，则为 null.
        /// </summary>
        /// <returns>当前租户信息</returns>
        public TenantDto GetCurrentTenant()
        {
            var tenantName = this.CurrentTenantName;
            if (String.IsNullOrWhiteSpace(tenantName))
                return null;

            using (var tenantService = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                return tenantService.GetTenant(tenantName);
            }
        }

        #endregion

        #region Public Proterties 

        /// <summary>
        /// 获取当前租户名。
        /// 此租户名不一定在租户信息列表中存在
        /// </summary>
        public string CurrentTenantName
        {
            get { return this.tenantManager.GetCurrentTenant(); }
        }

        #endregion
    }
}
