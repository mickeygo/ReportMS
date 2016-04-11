using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
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
        /// 是否存在指定的用户
        /// </summary>
        /// <param name="userName">要检查的用户名</param>
        /// <returns>true 表示存在；false 表示不存在</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        bool IsExistUser(string userName);

        /// <summary>
        /// 设置该用户在当前租户中的角色。若角色不存在，表示移除角色
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <param name="tenantId">租户 Id</param>
        /// <param name="roleId">角色 Id，为 null 表示移除角色</param>
        /// <param name="creator">操作人</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void SetRoles(Guid userId, Guid tenantId, Guid? roleId, string creator);

        /// <summary>
        /// 判断用户是否是管理员（并非系统管理者）.
        /// </summary>
        /// <param name="userId">用户 Id</param>
        /// <returns>true 表示是管理员; 否则为 false</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        bool IsAdmin(Guid userId);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userDto">要创建的用户 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void CreateUser(UserDto userDto);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userDto">要更新的用户 Dto 对象</param>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        void UpdateUser(UserDto userDto);
    }
}
