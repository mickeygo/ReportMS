using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
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
        /// 查找指定租户下的所有有效的角色
        /// </summary>
        /// <param name="tenantId">要查找的租户 Id</param>
        /// <returns>角色 Dto 对象集</returns>
        /// <remarks>根据设计的聚合关系，角色依赖租户，因此通过租户查找角色不应属于租户的范畴</remarks>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<RoleDto> FindRoles(Guid tenantId);

        /// <summary>
        /// 查找所有有效的角色
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<RoleDto> FindAllRoles();

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
        /// 查找角色拥有的报表分组
        /// </summary>
        /// <param name="roleId">角色 Id</param>
        /// <returns>报表分组 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<ReportGroupRoleDto> FindReportGroupRoles(Guid roleId);

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

        /// <summary>
        /// 设置该角色拥有的报表组
        /// </summary>
        /// <param name="roleId">角色 Id</param>
        /// <param name="reportGroupIds">报表组 Id 集合</param>
        /// <param name="creator">创建人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void SetReportGroupRoles(Guid roleId, IEnumerable<Guid> reportGroupIds, string creator);
    }
}
