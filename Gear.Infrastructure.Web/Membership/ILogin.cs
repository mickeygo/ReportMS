namespace Gear.Infrastructure.Web.Membership
{
    /// <summary>
    /// 表示实现接口的类为
    /// </summary>
    public interface ILogin
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="rememberMe">是否记住用户</param>
        void LogIn(string userName, string password, bool rememberMe = false);

        /// <summary>
        /// 用户登出
        /// </summary>
        void LogOut();
    }
}
