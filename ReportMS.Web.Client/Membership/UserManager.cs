using System;
using System.Collections.Generic;
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

        #region Ctor

        private UserManager()
        {
        }

        #endregion

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
        /// 查找用户所有有效的角色
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>角色信息结合</returns>
        public IEnumerable<RoleDto> GetRoles(Guid userId)
        {
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                return userService.FindRoles(userId);
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

        /// <summary>
        /// 判断用户是否是管理员（并非系统管理者）.
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>true 表示是管理员; 否则为 false</returns>
        public bool IsAdmin(Guid userId)
        {
            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            {
                return userService.IsAdmin(userId);
            }
        }

        #endregion

        #region Create Or Update User Information

        /// <summary>
        /// 从 HR 系统(EZ)中查询用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserDto GetUserFromHR(string userName)
        {
            var userQService = ServiceLocator.Instance.Resolve<IUserQueryService>();
            return userQService.Find(userName);
        }

        /// <summary>
        /// 创建用户信息，若存在该用户，则更新
        /// </summary>
        /// <param name="userName">要创建的用户名</param>
        public void AddOrUpdateUser(string userName)
        {
            var user = this.GetUserFromHR(userName);
            if (user != null)
            {
                using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
                {
                    if (userService.IsExistUser(userName))
                        userService.UpdateUser(user);
                    else
                        userService.CreateUser(user);
                }
            }
        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="userName">要创建的用户名</param>
        public void CreateUser(string userName)
        {
            var user = this.GetUserFromHR(userName);
            if (user != null)
            {
                using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
                {
                    userService.CreateUser(user);
                }
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userName">要更新的用户名</param>
        public void UpdateUser(string userName)
        {
            var user = this.GetUserFromHR(userName);
            if (user != null)
            {
                using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
                {
                    userService.UpdateUser(user);
                }
            }
        }

        #endregion
    }
}
