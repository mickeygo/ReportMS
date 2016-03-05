using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为用户应用服务.
    /// 查找用户拥有的角色.
    /// </summary>
    [ServiceContract]
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// 查找用户信息
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>用户信息</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        UserDto FindUser(Guid userId);

        /// <summary>
        /// 查找用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户信息</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        UserDto FindUser(string userName);

        /// <summary>
        /// 查找用户所有的角色
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>角色集合</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        IEnumerable<RoleDto> FindRoles(Guid userId);

        /// <summary>
        /// 查找用户在指定租户中的角色
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="tenantId">租户 Id</param>
        /// <returns>角色</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        RoleDto FindRole(Guid userId, Guid tenantId);

        /// <summary>
        /// 向用户设定指定权限
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="roleId">角色 Id，为 null 表示移除角色</param>
        /// <param name="creator">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void SetRoles(Guid userId, Guid? roleId, string creator);

    }
}
