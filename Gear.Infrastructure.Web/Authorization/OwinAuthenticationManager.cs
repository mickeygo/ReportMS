using System.Web;
using Gear.Infrastructure.Authentication;
using Microsoft.Owin.Security;

namespace Gear.Infrastructure.Web.Authorization
{
    /// <summary>
    /// 基于 Owin 的验证管理
    /// </summary>
    public class OwinAuthenticationManager
    {
        #region Ctor

        private OwinAuthenticationManager()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="ticket">验证票据</param>
        public static void SignIn(OwinAuthenticationTicket ticket)
        {
            var authentication = GetAuthentication();
            authentication.SignIn(ticket.Properties, ticket.Identities);
        }

        /// <summary>
        /// 验证登出
        /// </summary>
        /// <param name="properties">用于存储验证会话的相关属性对象</param>
        /// <param name="authenticationTypes">验证类型</param>
        public static void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            var authentication = GetAuthentication();
            authentication.SignOut(properties, authenticationTypes);
        }

        /// <summary>
        /// 验证登出
        /// </summary>
        /// <param name="authenticationTypes">验证类型</param>
        public static void SingOut(params string[] authenticationTypes)
        {
            var authentication = GetAuthentication();
            authentication.SignOut(authenticationTypes);
        }

        /// <summary>
        /// 获取验证身份
        /// </summary>
        public static IAuthentication OwinAuthentication
        {
            get
            {
                var authentication = GetAuthentication();
                return new OwinAuthentication(authentication.User);
            }
        }

        #endregion

        #region Private Methods

        public static IAuthenticationManager GetAuthentication()
        {
            var owinContext = HttpContext.Current.Request.GetOwinContext();
            return owinContext.Authentication;
        }

        #endregion
    }
}
