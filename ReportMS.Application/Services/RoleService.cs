using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 角色的应用服务
    /// </summary>
    public class RoleService : IRoleService
    {
        #region Private Fields

        private readonly IMenuRoleRepository _menuRoleRepository;
        private readonly IActionRoleRepository _actionRoleRepository;

        #endregion

        public RoleService(IMenuRoleRepository menuRoleRepository, IActionRoleRepository actionRoleRepository)
        {
            this._menuRoleRepository = menuRoleRepository;
            this._actionRoleRepository = actionRoleRepository;
        }

        #region IRoleService Members

        public IEnumerable<MenuDto> FindMenus(Guid roleId)
        {
            var menuRoles = this._menuRoleRepository.FindAll(Specification<MenuRole>.Eval(r => r.RoleId == roleId));
            return menuRoles.Select(r => r.Menu).MapAs<MenuDto>();
        }

        public IEnumerable<ActionsDto> FindAcions(Guid roleId)
        {
            var actionRoles = this._actionRoleRepository.FindAll(Specification<ActionRole>.Eval(r => r.RoleId == roleId));
            return actionRoles.Select(r => r.Actions).MapAs<ActionsDto>();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._menuRoleRepository != null)
                this._menuRoleRepository.Context.Dispose();
            if (this._actionRoleRepository != null)
                this._actionRoleRepository.Context.Dispose();
        }

        #endregion
    }
}
