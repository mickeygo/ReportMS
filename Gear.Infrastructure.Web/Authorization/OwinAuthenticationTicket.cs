using System.Collections.Generic;
using System.Security.Claims;
using Gear.Infrastructure.Authentication;
using Microsoft.Owin.Security;

namespace Gear.Infrastructure.Web.Authorization
{
    /// <summary>
    /// 表示基于 Owin 身份验证的票据
    /// </summary>
    public sealed class OwinAuthenticationTicket
    {
        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>OwinAuthenticationTicket</c>实例
        /// </summary>
        /// <param name="properties">验证属性值</param>
        /// <param name="identities">基于声明的身份集合</param>
        public OwinAuthenticationTicket(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            this.Properties = properties;
            this.Identities = identities;
        }

        /// <summary>
        /// 初始化一个新的<c>OwinAuthenticationTicket</c>实例
        /// </summary>
        /// <param name="properties">验证属性值</param>
        /// <param name="claims">Claim 集合</param>
        /// <param name="authenticationType">验证类型</param>
        public OwinAuthenticationTicket(AuthenticationProperties properties, IEnumerable<Claim> claims, string authenticationType)
        {
            this.Properties = properties;
            var identity = new ClaimsIdentity(claims, authenticationType);
            this.Identities = new[] {identity};
        }

        /// <summary>
        /// 初始化一个新的<c>OwinAuthenticationTicket</c>实例
        /// </summary>
        /// <param name="isPresistent">是否持久化验证会话</param>
        /// <param name="data">要存储的数据</param>
        /// <param name="authenticationType">验证类型</param>
        public OwinAuthenticationTicket(bool isPresistent, AuthenticationData data, string authenticationType)
        {
            this.Properties = new AuthenticationProperties
            {
                IsPersistent = isPresistent
            };

            var claimManager = new ClaimManager(data);
            var identity = new ClaimsIdentity(claimManager.Claims, authenticationType);
            this.Identities = new[] { identity };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取验证属性值
        /// </summary>
        public AuthenticationProperties Properties { get; private set; }

        /// <summary>
        /// 获取验证身份标识集合
        /// </summary>
        public ClaimsIdentity[] Identities { get; private set; }

        #endregion
    }
}
