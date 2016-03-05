using System;
using System.Collections.Generic;
using System.Linq;
using Gear.Infrastructure.Specifications;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;
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

        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRoleRepository _menuRoleRepository;
        private readonly IActionRoleRepository _actionRoleRepository;
        private readonly IReportGroupRoleRepository _reportGroupRoleRepository;

        #endregion

        #region Ctor

        public RoleService(IRoleRepository roleRepository, IMenuRoleRepository menuRoleRepository,
            IActionRoleRepository actionRoleRepository,
            IReportGroupRoleRepository reportGroupRoleRepository)
        {
            this._roleRepository = roleRepository;
            this._menuRoleRepository = menuRoleRepository;
            this._actionRoleRepository = actionRoleRepository;
            this._reportGroupRoleRepository = reportGroupRoleRepository;
        }

        #endregion

        #region IRoleService Members

        public RoleDto FindRole(Guid roleId)
        {
            return this._roleRepository.GetByKey(roleId).MapAs<RoleDto>();
        }

        public IEnumerable<RoleDto> FindRoles(Guid tenantId)
        {
            var spec = Specification<Role>.Eval(r => r.TenantId == tenantId);
            return this._roleRepository.FindAll(spec).MapAs<RoleDto>();
        }

        public IEnumerable<RoleDto> FindAllRoles()
        {
            return this._roleRepository.FindAll().MapAs<RoleDto>();
        }

        public bool ExistRole(string roleName)
        {
            var spec = Specification<Role>.Eval(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            return this._roleRepository.Exist(spec);
        }

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

        public IEnumerable<ReportGroupRoleDto> FindReportGroupRoles(Guid roleId)
        {
            var spec = Specification<ReportGroupRole>.Eval(r => r.RoleId == roleId);
            return this._reportGroupRoleRepository.FindAll(spec).MapAs<ReportGroupRoleDto>();
        }

        public void CreateRole(RoleDto roleDto)
        {
            var role = new Role(roleDto.RoleName, roleDto.DisplayName, roleDto.Description, roleDto.TenantId,
                roleDto.CreatedBy);
            this._roleRepository.Add(role);
        }

        public void UpdateRole(RoleDto roleDto)
        {
            var role = this._roleRepository.GetByKey(roleDto.ID);
            if (role == null)
                return;

            role.UpdateRole(roleDto.DisplayName, roleDto.Description, roleDto.UpdatedBy);
            this._roleRepository.Update(role);
        }

        public void DeleteRole(Guid roleId)
        {
            var role = this._roleRepository.GetByKey(roleId);
            if (role == null)
                return;

            role.Disable();
            this._roleRepository.Update(role);
        }

        public void SetReportGroupRoles(Guid roleId, IEnumerable<Guid> reportGroupIds, string creator)
        {
            // Remove the all that own to the role
            var repGroupRoles = this._reportGroupRoleRepository.FindAll(Specification<ReportGroupRole>.Eval(r => r.RoleId == roleId));
            if (repGroupRoles != null)
            {
                foreach (var repGroupRole in repGroupRoles)
                    this._reportGroupRoleRepository.Remove(repGroupRole);
            }

            // add the report groups if the reportGroupIds has any item.
            if (reportGroupIds == null || !reportGroupIds.Any())
                return;

            var addRepGroupRoles = (from reportGroupId in reportGroupIds
                select new ReportGroupRole(reportGroupId, roleId, creator));

            foreach (var addRepGroupRole in addRepGroupRoles)
                this._reportGroupRoleRepository.Add(addRepGroupRole);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._roleRepository != null)
                this._roleRepository.Context.Dispose();
            if (this._menuRoleRepository != null)
                this._menuRoleRepository.Context.Dispose();
            if (this._actionRoleRepository != null)
                this._actionRoleRepository.Context.Dispose();
            if (this._reportGroupRoleRepository != null)
                this._reportGroupRoleRepository.Context.Dispose();
        }

        #endregion
    }
}
