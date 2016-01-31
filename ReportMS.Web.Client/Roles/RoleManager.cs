using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Client.Roles
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleManager
    {
        private static readonly RoleManager instance = new RoleManager();

        #region Public Properties

        /// <summary>
        /// 获取当前实例
        /// </summary>
        public static RoleManager Instance
        {
            get { return instance; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 验证角色是否有权限
        /// </summary>
        /// <param name="roleId">角色 id</param>
        /// <param name="area">Area</param>
        /// <param name="controller">Controller</param>
        /// <param name="action">Action, 为 null 表示不验证 Action</param>
        /// <returns>True 表示有权限</returns>
        public bool HasActionOfRole(Guid roleId, string area, string controller, string action = null)
        {
            Func<ActionsDto, bool> predicate = a =>
            {
                var flag = (a.Area.Equals(area, StringComparison.OrdinalIgnoreCase));
                flag = flag && (a.Controller.Equals(controller, StringComparison.OrdinalIgnoreCase));
                if (action != null)
                    flag = flag && (a.Action.Equals(action, StringComparison.OrdinalIgnoreCase));

                return flag;
            };

            var actions = this.GetActions(roleId);
            return actions.Any(predicate);
        }

        #endregion

        #region Private Methods

        private IEnumerable<ActionsDto> GetActions(Guid roleId)
        {
            using (var roleService = ServiceLocator.Instance.Resolve<IRoleService>())
            {
                return roleService.FindAcions(roleId);
            }
        }

        #endregion
    }
}
