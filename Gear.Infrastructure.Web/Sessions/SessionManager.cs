using System.Web;
using System.Web.SessionState;

namespace Gear.Infrastructure.Web.Sessions
{
    /// <summary>
    /// Session 管理。
    /// 关于 Session 的相关信息设置，可以在 Web.config 文件中配置
    /// </summary>
    public class SessionManager : ISessionProvider
    {
        #region Private Fields

        private readonly HttpSessionState session;

        #endregion

        #region Ctor

        private SessionManager()
        {
            this.session = this.GetCurrentSessionState();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取 Session 管理实例
        /// </summary>
        public static ISessionProvider Instance
        {
            get { return new SessionManager(); }
        }

        #endregion

        #region ISessionProvider Members

        public object Get(string name)
        {
            return this.session[name];
        }

        public void Add(string name, object value)
        {
            this.session[name] = value;
        }

        public void Remove(string name)
        {
            this.session.Remove(name);
        }

        public void Clear()
        {
            this.session.Clear();
        }

        #endregion

        #region

        private HttpSessionState GetCurrentSessionState()
        {
            return HttpContext.Current.Session;
        }

        #endregion
    }
}
