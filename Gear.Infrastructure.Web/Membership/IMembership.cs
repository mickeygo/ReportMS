namespace Gear.Infrastructure.Web.Membership
{
    /// <summary>
    /// 表示实现此接口类为成员管理管理类
    /// </summary>
    public interface IMembership
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword">确认密码</param>
        void Register(string userName, string password, string confirmPassword);

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="userName"></param>
        void LogOff(string userName);
    }
}
