using System;
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
