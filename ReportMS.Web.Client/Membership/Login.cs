using Gear.Infrastructure.Authentication;
using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Membership;
using Microsoft.AspNet.Identity;

namespace ReportMS.Web.Client.Membership
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class Login : LoginProvider, ILogin
    {
        #region Private Fields

        private readonly string authenticationType = DefaultAuthenticationTypes.ApplicationCookie;

        #endregion

        #region ILogin Members

        /// <summary>
        /// 用户登录。
        /// 在当前(多租户)系统中，需要根据实际的租户信息来确定用户的角色, 按改系统的逻辑, 不同的租户中用户角色不同
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="rememberMe">是否记住用户</param>
        /// <returns>ture 表示登录成功; false 表示登录失败</returns>
        public bool LogIn(string userName, string password, bool rememberMe)
        {
            // 本地验证
            var validation = new UserValidation(userName, password);
            if (!validation.ValidateInLocal())
                return false;

            // 登录
            this.OnCreateAuthenticationTicket(userName, rememberMe);
            this.CreateCookie();

            return true;
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        public void LogOut()
        {
            base.ClearCookie(authenticationType);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 创建身份验证票据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="isPresistent">是否持久化</param>
        protected virtual void OnCreateAuthenticationTicket(string userName, bool isPresistent)
        {
            var user = UserManager.Instance.GetUserInfo(userName);
            var userData = new AuthenticationData(user.ID, user.UserName, user.Email);

            this.AuthenticationTicket = new OwinAuthenticationTicket(isPresistent, userData, authenticationType);
        }

        #endregion
    }
}
