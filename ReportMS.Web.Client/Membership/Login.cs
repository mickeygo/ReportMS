using System;
using Gear.Infrastructure;
using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Membership;

namespace ReportMS.Web.Client.Membership
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class Login : LoginProvider, ILogin
    {
        #region ILogin Members

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="rememberMe">是否记住用户</param>
        public void LogIn(string userName, string password, bool rememberMe = false)
        {
            
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        public void LogOut()
        {
            
        }

        #endregion

        protected override OwinAuthenticationTicket CreateAuthenticationTicket()
        {
            throw new NotImplementedException();
        }
    }
}
