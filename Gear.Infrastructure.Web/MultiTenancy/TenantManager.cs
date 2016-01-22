using System;
using System.Web.Routing;

namespace Gear.Infrastructure.Web.MultiTenancy
{
    /// <summary>
    /// 租户管理。
    /// 在路由表中配置，如 "{tenant}/{controller}/{action}/{id}" 或 "{tenant}/{area}/{controller}/{action}/{id}"
    /// </summary>
    public class TenantManager
    {
        private readonly string _tenantToken = "tenant";
        private readonly RouteData route;

        /// <summary>
        /// 初始化一个新的<c>TenantManager</c>实例
        /// </summary>
        /// <param name="route">路由</param>
        public TenantManager(RouteData route)
        {
            this.route = route;
        }

        /// <summary>
        /// 获取当前的租户名，若没有租户，则返回<see cref="String.Empty"/>空值
        /// </summary>
        /// <returns><see cref="System.String"/>当前租户名称</returns>
        public string GetCurrentTenant()
        {
            if (this.route.Values.ContainsKey(this.BuildTenantToken()))
                return this.route.Values[this.BuildTenantToken()].ToString();

            return String.Empty;
        }

        /// <summary>
        /// 创建 TenantToken 值，默认为 "tenant"
        /// </summary>
        /// <returns>返回租户 Token 值</returns>
        protected virtual string BuildTenantToken()
        {
            return this._tenantToken;
        }
    }
}
