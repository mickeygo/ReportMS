namespace Gear.Infrastructure.Web.Membership
{
    /// <summary>
    /// 登录选项
    /// </summary>
    public class LoginOptions
    {
        #region Public Properties

        /// <summary>
        /// 获取账户名
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 获取登录密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 是否记住
        /// </summary>
        public bool RememberMe { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>LoginOptions</c>实例
        /// </summary>
        /// <param name="userName">账户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="rememberMe">是否记住，默认为 false</param>
        public LoginOptions(string userName, string password, bool rememberMe = false)
        {
            this.UserName = userName;
            this.Password = password;
            this.RememberMe = rememberMe;
        }

        #endregion
    }
}
