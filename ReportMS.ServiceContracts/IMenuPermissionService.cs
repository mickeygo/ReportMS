using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using Gear.Infrastructure.Services.ApplicationServices;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现接口的类为菜单权限应用服务
    /// </summary>
    [ServiceContract]
    public interface IMenuPermissionService : IApplicationService
    {
        #region Permission

        /// <summary>
        /// 查找所有的有效的权限
        /// </summary>
        /// <returns><c>ActionsDto</c>集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<ActionsDto> FindAllPermissions();

        /// <summary>
        /// 查找指定的权限
        /// </summary>
        /// <param name="permissionId">要查找的权限 Id</param>
        /// <returns><c>ActionsDto</c></returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        ActionsDto FindPermission(Guid permissionId);

        /// <summary>
        /// 查找有指定权限的那些角色
        /// </summary>
        /// <param name="permissionId">权限 Id</param>
        /// <returns><c>ActionRoleDto</c>集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<ActionRoleDto> FindRolesOfPermission(Guid permissionId);

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="permission">要创建的权限 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void CreatePermission(ActionsDto permission);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission">要修改的权限 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void ModifyPermission(ActionsDto permission);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="permissionId">要创建的权限 Id</param>
        /// <param name="handler">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemovePermission(Guid permissionId, string handler);

        /// <summary>
        /// 将权限附加到角色
        /// </summary>
        /// <param name="permissionId">要附加的权限 Id</param>
        /// <param name="roleIds">要附加到的角色 Id 集合</param>
        /// <param name="handler">处理人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void AttachPermissionToRoles(Guid permissionId, Guid[] roleIds, string handler);

        #endregion

        #region Menu

        /// <summary>
        /// 查找所有的有效的菜单
        /// </summary>
        /// <returns><c>MenuDto</c>集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<MenuDto> FindAllMenus();

        /// <summary>
        /// 查找指定的菜单
        /// </summary>
        /// <param name="menuId">要查找的菜单 Id</param>
        /// <returns><c>MenuDto</c></returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        MenuDto FindMenu(Guid menuId);

        /// <summary>
        /// 通过菜单等级来查找菜单
        /// </summary>
        /// <param name="menuLevel">菜单等级</param>
        /// <returns><c>MenuDto</c>集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<MenuDto> FindMenusViaLevel(MenuLevelDto menuLevel);

        /// <summary>
        /// 查找指定菜单 Id 的菜单及其子菜单
        /// </summary>
        /// <param name="menuId">要查找的菜单</param>
        /// <returns>返回该菜单及其子菜单</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<MenuDto> FindMenuWithChildren(Guid menuId);

        /// <summary>
        /// 查找有指定菜单的那些角色
        /// </summary>
        /// <param name="menuId">指定的菜单 Id</param>
        /// <returns><c>MenuRoleDto</c>集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<MenuRoleDto> FindRolesOfMenu(Guid menuId);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu">要创建的菜单</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void CreateMenu(MenuDto menu);

        /// <summary>
        /// 更改菜单
        /// </summary>
        /// <param name="menu">要更改的菜单</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void ModifyMenu(MenuDto menu);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">要删除的菜单 Id</param>
        /// <param name="handler">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void RemoveMenu(Guid menuId, string handler);

        /// <summary>
        /// 菜单附加到指定角色
        /// </summary>
        /// <param name="menuId">要附加的菜单 Id</param>
        /// <param name="roleIds">要附加到的角色 Id 集合</param>
        /// <param name="handler">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void AttachMenuToRoles(Guid menuId, Guid[] roleIds, string handler);

        #endregion
    }
}
