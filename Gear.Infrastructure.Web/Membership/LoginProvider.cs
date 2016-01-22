using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Sessions;

namespace Gear.Infrastructure.Web.Membership
{
    /// <summary>
    /// 用户登录基类。
    /// 注：考虑使用 Cache 机制来代替 Session
    /// </summary>
    public abstract class LoginProvider
    {
        #region Protected Method

        /// <summary>
        /// 创建 Cookie
        /// </summary>
        protected void CreateCookie()
        {
            var authenticationTicket = this.CreateAuthenticationTicket();
            OwinAuthenticationManager.SignIn(authenticationTicket);
        }

        /// <summary>
        /// 创建 Session
        /// </summary>
        protected virtual void CreateSession()
        {
        }

        /// <summary>
        /// 清空 Cookie
        /// </summary>
        /// <param name="authenticationType"></param>
        protected virtual void ClearCookie(string authenticationType)
        {
            OwinAuthenticationManager.SingOut(authenticationType);
        }

        /// <summary>
        /// 清除 Session
        /// </summary>
        /// <param name="sessions">要清楚的 Session 名称集合</param>
        protected void ClearSessions(params string[] sessions)
        {
            var sessionManager = SessionManager.Instance;
            foreach (var session in sessions)
                sessionManager.Remove(session);
        }

        /// <summary>
        /// 清除所有的 Session
        /// </summary>
        protected void ClearAllSession()
        {
            SessionManager.Instance.Clear();
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// 创建身份验证票据
        /// </summary>
        /// <returns>身份验证票据</returns>
        protected abstract OwinAuthenticationTicket CreateAuthenticationTicket();

        #endregion
    }
}
