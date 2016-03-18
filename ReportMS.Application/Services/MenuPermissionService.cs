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
    /// 菜单权限应用服务
    /// </summary>
    public class MenuPermissionService : IMenuPermissionService
    {
        #region Private Fields

        private readonly IActionRepository _actionRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IActionRoleRepository _actionRoleRepository;
        private readonly IMenuRoleRepository _menuRoleRepository;

        #endregion

        #region Ctor

        public MenuPermissionService(IActionRepository actionRepository, IMenuRepository menuRepository,
            IActionRoleRepository actionRoleRepository, IMenuRoleRepository menuRoleRepository)
        {
            _actionRepository = actionRepository;
            _menuRepository = menuRepository;
            _actionRoleRepository = actionRoleRepository;
            _menuRoleRepository = menuRoleRepository;
        }

        #endregion

        #region IMenuPermissionService Members

        public IEnumerable<ActionsDto> FindAllPermissions()
        {
            return this._actionRepository.FindAll().ToList().MapAs<ActionsDto>();
        }

        public ActionsDto FindPermission(Guid permissionId)
        {
            return this._actionRepository.GetByKey(permissionId).MapAs<ActionsDto>();
        }

        public IEnumerable<ActionRoleDto> FindRolesOfPermission(Guid permissionId)
        {
            var spec = Specification<ActionRole>.Eval(a => a.ActionsId == permissionId);
            return this._actionRoleRepository.FindAll(spec).ToList().MapAs<ActionRoleDto>();
        }

        public void CreatePermission(ActionsDto permission)
        {
            var action = new Actions(permission.Area, permission.Controller, permission.Action, permission.Description,
                permission.CreatedBy);
            this._actionRepository.Add(action);
        }

        public void ModifyPermission(ActionsDto permission)
        {
            var action = this._actionRepository.GetByKey(permission.ID);
            if (action != null)
            {
                action.Update(permission.Area, permission.Controller, permission.Action, permission.Description,
                    permission.UpdatedBy);
                this._actionRepository.Update(action);
            }
        }

        public void RemovePermission(Guid permissionId, string handler)
        {
            var action = this._actionRepository.GetByKey(permissionId);
            action.Disable();
            action.SetUpdatedBy(handler);

            this._actionRepository.Update(action);
        }

        public void AttachPermissionToRoles(Guid permissionId, Guid[] roleIds, string handler)
        {
            // Remove all roles that include the permission.
            var spec = Specification<ActionRole>.Eval(a => a.ActionsId == permissionId);
            var rolesOfPms = this._actionRoleRepository.FindAll(spec);
            if (rolesOfPms != null)
            {
                foreach (var item in rolesOfPms)
                    this._actionRoleRepository.Remove(item);
            }

            if (roleIds == null || !roleIds.Any())
                return;

            // attach to role
            var actionRoles = (from roleId in roleIds
                select new ActionRole(permissionId, roleId, handler));
            foreach (var actionRole in actionRoles)
                this._actionRoleRepository.Add(actionRole);
        }

        public IEnumerable<MenuDto> FindAllMenus()
        {
            return this._menuRepository.FindAll().ToList().MapAs<MenuDto>();
        }

        public IEnumerable<MenuDto> FindMenusViaLevel(MenuLevelDto menuLevel)
        {
            var spec = Specification<Menu>.Eval(m => m.Level == (MenuLevel) menuLevel);
            return this._menuRepository.FindAll(spec).ToList().MapAs<MenuDto>();
        }

        public MenuDto FindMenu(Guid menuId)
        {
            return this._menuRepository.GetByKey(menuId).MapAs<MenuDto>();
        }

        public IEnumerable<MenuRoleDto> FindRolesOfMenu(Guid menuId)
        {
            var spec = Specification<MenuRole>.Eval(a => a.MenuId == menuId);
            return this._menuRoleRepository.FindAll(spec).ToList().MapAs<MenuRoleDto>();
        }

        public void CreateMenu(MenuDto menu)
        {
            // Todo: set the sort
            var m_menu = new Menu(menu.MenuName, menu.DisplayName, menu.Description, menu.ParentId,
                (MenuLevel) menu.Level,
                menu.Sort, menu.ActionsId, menu.CreatedBy);

            this._menuRepository.Update(m_menu);
        }

        public void ModifyMenu(MenuDto menu)
        {
            var m_menu = this._menuRepository.GetByKey(menu.ID);
            if (m_menu != null)
            {
                // Todo: set the sort
                m_menu.Update(menu.DisplayName, menu.Description, menu.ParentId, (MenuLevel) menu.Level, menu.Sort,
                    menu.ActionsId, menu.UpdatedBy);
                this._menuRepository.Update(m_menu);
            }
        }

        public void RemoveMenu(Guid menuId, string handler)
        {
            var menu = this._menuRepository.GetByKey(menuId);
            if (menu != null)
            {
                menu.Disabled();
                menu.SetUpdatedBy(handler);
                this._menuRepository.Update(menu);
            }
        }

        public void AttachMenuToRoles(Guid menuId, Guid[] roleIds, string handler)
        {
            // Remove all roles that include the menu.
            var spec = Specification<MenuRole>.Eval(a => a.MenuId == menuId);
            var rolesOfmeun = this._menuRoleRepository.FindAll(spec);
            if (rolesOfmeun != null)
            {
                foreach (var item in rolesOfmeun)
                    this._menuRoleRepository.Remove(item);
            }

            if (roleIds == null || !roleIds.Any())
                return;

            // attach to role
            var menuRoles = (from roleId in roleIds
                select new MenuRole(menuId, roleId, handler));
            foreach (var menuRole in menuRoles)
                this._menuRoleRepository.Add(menuRole);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._actionRepository != null)
                this._actionRepository.Context.Dispose();
            if (this._actionRoleRepository != null)
                this._actionRoleRepository.Context.Dispose();
            if (this._menuRepository != null)
                this._menuRepository.Context.Dispose();
            if (this._menuRoleRepository != null)
                this._menuRoleRepository.Context.Dispose();
        }

        #endregion
    }
}
