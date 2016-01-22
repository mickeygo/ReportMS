using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Gear.Infrastructure.Authentication;

namespace Gear.Infrastructure.Web.Authorization
{
    /// <summary>
    /// 表示基于 OWIN 的验证类
    /// </summary>
    public class OwinAuthentication : IAuthentication
    {
        #region Private Fields

        private readonly ClaimsPrincipal _claimsPrincipal;

        #endregion

        #region

        /// <summary>
        /// 初始化一个新的<c>OwinAuthentication</c>实例
        /// </summary>
        /// <param name="claimsPrincipal">基于声明的验证主体</param>
        public OwinAuthentication(ClaimsPrincipal claimsPrincipal)
        {
            this._claimsPrincipal = claimsPrincipal;
        }

        #endregion

        #region IAuthentication Members

        /// <summary>
        /// 获取用户的身份标识. 若不存在，则为 null
        /// </summary>
        public Guid? NameIdentifier
        {
            get
            {
                var id = this.FilterClaim(ClaimTypes.NameIdentifier);

                Guid result;
                return Guid.TryParse(id, out result) ? result : (Guid?) null;
            }
        }

        /// <summary>
        /// 获取当前用户的 Email
        /// </summary>
        public string Email
        {
            get { return this.FilterClaim(ClaimTypes.Email); }
        }

        /// <summary>
        /// 获取当前用户拥有的角色
        /// </summary>
        public string[] Roles
        {
            get { return this.FilterClaims(ClaimTypes.Role).ToArray(); }
        }

        #endregion

        #region IPrincipal Members

        /// <summary>
        /// 获取当前用户的身份验证标识
        /// </summary>
        public IIdentity Identity
        {
            get { return this._claimsPrincipal.Identity; }
        }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>，表示当前用户是否属于指定的角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns>ture 表示属于指定的角色; 否则为 false</returns>
        public bool IsInRole(string role)
        {
            return this._claimsPrincipal.IsInRole(role);
        }

        #endregion

        #region Private Methods

        private string FilterClaim(string claimType)
        {
            var claims = this._claimsPrincipal.Claims;
            return (from claim in claims
                where claim.Type == claimType
                select claim.Value).FirstOrDefault();
        }

        private IEnumerable<string> FilterClaims(string claimType)
        {
            var claims = this._claimsPrincipal.Claims;
            return (from claim in claims
                where claim.Type == claimType
                select claim.Value);
        }

        #endregion
    }
}
