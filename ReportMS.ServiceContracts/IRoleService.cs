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
        /// 查找角色拥有的菜单
        /// </summary>
        /// <param name="roleId">角色 Id</param>
        /// <returns>菜单集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<MenuDto> FindMenus(Guid roleId);

        /// <summary>
        /// 查找角色拥有的 Actions
        /// </summary>
        /// <param name="roleId">角色 Id</param>
        /// <returns>Actions 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<ActionsDto> FindAcions(Guid roleId);
    }
}
