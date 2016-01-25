using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Gear.Infrastructure.Configurations.Fluent;

namespace Gear.Infrastructure.Web.Membership
{
    /// <summary>
    /// 成员管理
    /// </summary>
    public class MemberManager
    {
        #region

        private static readonly char[] _splitChars = {',', ';'};

        #endregion

        #region Static Public Methods

        /// <summary>
        /// 获取系统管理人员.
        /// 若不存在，则返回空的数组（非 null）
        /// </summary>
        /// <returns>管理人员集合</returns>
        public static string[] GetAdministrators()
        {
            var administrator = SystemAdminConfigurator.Default.SystemAdminElement.Administrator;
            if (String.IsNullOrWhiteSpace(administrator))
                return new string[0];

            return (
                from admin in administrator.Split(_splitChars, StringSplitOptions.RemoveEmptyEntries)
                let s = admin.Trim()
                select s).ToArray();
        }

        /// <summary>
        /// 获取当前登录人员名称
        /// </summary>
        /// <returns>当前登录人员名称</returns>
        public static string GetCurrentLoginUser()
        {
            var loginUser = (ClaimsPrincipal) HttpContext.Current.User;
            return loginUser != null ? loginUser.Identity.Name : null;
        }

        /// <summary>
        /// 判断当前登录者是否是系统管理员成员
        /// </summary>
        /// <returns>true 表示是系统管理人员成员；false 表示不是</returns>
        public static bool IsCurrentLoginUserInAdministrator()
        {
            var administrators = GetAdministrators();
            var loginUser = GetCurrentLoginUser();

            return administrators.Contains(loginUser, StringComparer.OrdinalIgnoreCase);
        }

        #endregion
    }
}
