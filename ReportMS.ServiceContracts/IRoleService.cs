using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为角色应用服务。
    /// 查询角色及改角色拥有的菜单和功能
    /// </summary>
    [ServiceContract]
    public interface IRoleService : IApplicationService
    {
        /// <summary>
        /// 查找指定的角色
        /// </summary>
        /// <param name="roleId">要查找的角色 Id</param>
        /// <returns>角色 Dto 对象</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        RoleDto FindRole(Guid roleId);

        /// <summary>
        /// 是否存在指定的角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>true 表示存在；否则为 false</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        bool ExistRole(string roleName);

        /// <summary>
        /// 查找角色拥有的菜单
        /// </summary>
        /// <param name="roleId">角色 Id</param>
        /// <returns>菜单集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<MenuDto> FindMenus(Guid roleId);

        /// <summary>
        /// 查找角色拥有的 Actions
        /// </summary>
        /// <param name="roleId">角色 Id</param>
        /// <returns>Actions 集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<ActionsDto> FindAcions(Guid roleId);

        /// <summary>
        /// 创建了角色
        /// </summary>
        /// <param name="roleDto">要创建的角色 Dto</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void CreateRole(RoleDto roleDto);

        /// <summary>
        /// 更改角色
        /// </summary>
        /// <param name="roleDto">要更改的角色 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void UpdateRole(RoleDto roleDto);

        /// <summary>
        /// 删除指定的角色
        /// </summary>
        /// <param name="roleId">要删除的角色 Id</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void DeleteRole(Guid roleId);
    }
}
