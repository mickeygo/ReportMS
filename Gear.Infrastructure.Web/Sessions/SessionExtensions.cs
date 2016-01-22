using System.Web.SessionState;

namespace Gear.Infrastructure.Web.Sessions
{
    /// <summary>
    /// Session 的扩展类
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// 获取 Session 指定名称的对象
        /// </summary>
        /// <param name="httpSessionState">Session State</param>
        /// <param name="name">Session 名</param>
        /// <returns><see cref="System.Object"/>存储在 Session 中的对象</returns>
        public static object Get(this HttpSessionState httpSessionState, string name)
        {
            return httpSessionState[name];
        }
    }
}
