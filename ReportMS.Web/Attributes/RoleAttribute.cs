using System;
using System.Web;
using System.Web.Mvc;
using Gear.Infrastructure.Web.Membership;

namespace ReportMS.Web.Attributes
{
    /// <summary>
    /// 角色授权.
    /// 其中在不指定角色的情况下，只允许系统管理员(Administrator)的角色访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class RoleAttribute : AuthorizeAttribute
    {
        #region

        private readonly string[] m_roles;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>RoleAttribute</c>实例。
        /// 只允许系统管理员(Administrator)的角色访问
        /// </summary>
        public RoleAttribute()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>RoleAttribute</c>实例。
        /// 注意角色名大小写，其中默认包含系统管理员(Administrator)的角色
        /// </summary>
        /// <param name="roles">能访问的角色</param>
        public RoleAttribute(params string[] roles)
        {
            this.m_roles = roles;
        }

        #endregion

        #region IAuthorizationFilter Members

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            
            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;

            var isAdministrator = MemberManager.IsCurrentLoginUserInAdministrator();
            if (isAdministrator)
                return true;

            return this.m_roles.HasAny(user.IsInRole);
        }
        #endregion
    }
}