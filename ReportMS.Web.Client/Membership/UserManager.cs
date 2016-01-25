using System;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Client.Membership
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    public class UserManager
    {
        private static readonly UserManager instance = new UserManager();

        #region Public Properties

        /// <summary>
        /// 获取当前实例
        /// </summary>
        public static UserManager Instance
        {
            get { return instance; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>用户信息</returns>
        public UserDto GetUserInfo(Guid userId)
        {
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                return userService.FindUser(userId);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="usernMane">用户名</param>
        /// <returns>用户信息</returns>
        public UserDto GetUserInfo(string usernMane)
        {
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                return userService.FindUser(usernMane);
            }
        }

        /// <summary>
        /// 获取当前用户在当前租户中的角色
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="tenantId">租户 Id</param>
        /// <returns>角色信息</returns>
        public RoleDto GetRoleOfTenant(Guid userId, Guid tenantId)
        {
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                return userService.FindRole(userId, tenantId);
            }
        }

        #endregion
    }
}
