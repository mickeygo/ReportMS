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
        protected virtual void CreateCookie()
        {
            OwinAuthenticationManager.SignIn(this.AuthenticationTicket);
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
        /// <param name="authenticationType">要清除的验证类型</param>
        protected virtual void ClearCookie(params string[] authenticationType)
        {
            OwinAuthenticationManager.SingOut(authenticationType);
        }

        /// <summary>
        /// 清除 Session
        /// </summary>
        /// <param name="sessions">要清楚的 Session 名称集合</param>
        protected virtual void ClearSessions(params string[] sessions)
        {
            var sessionManager = SessionManager.Instance;
            foreach (var session in sessions)
                sessionManager.Remove(session);
        }

        /// <summary>
        /// 清除所有的 Session
        /// </summary>
        protected virtual void ClearAllSession()
        {
            SessionManager.Instance.Clear();
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// 获取身份验证票据
        /// </summary>
        public OwinAuthenticationTicket AuthenticationTicket { get; protected set; }

        #endregion
    }
}
