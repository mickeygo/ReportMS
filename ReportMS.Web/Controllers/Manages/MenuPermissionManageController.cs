using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
    public class MenuPermissionManageController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Permission()
        {
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var permissions = service.FindAllPermissions();
                return PartialView("_Permission", permissions);
            }
        }

        public ActionResult Menu()
        {
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var menus = service.FindAllMenus();
                return PartialView("_Menu", menus);
            }
        }

        #region Permission

        public ActionResult CreatePermission()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePermission(ActionsDto model)
        {
            model.CreatedBy = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.CreatePermission(model);
                }
            }
            catch (Exception)
            {
                return Json(false, "Create the permission failure.");
            }

            return Json(true);
        }

        public ActionResult EditPermission(Guid permissionId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var model = service.FindPermission(permissionId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPermission(ActionsDto model)
        {
            model.UpdatedBy = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.ModifyPermission(model);
                }
            }
            catch (Exception)
            {
                return Json(false, "Modify the permission failure.");
            }

            return Json(true);
        }

        [HttpPost]
        public ActionResult DeletePermission(Guid permissionId)
        {
            var handler = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.RemovePermission(permissionId, handler);
                }
            }
            catch (Exception)
            {
                return Json(true, "Delete permission failure.");
            }

            return Json(true);
        }

        public ActionResult AttachPermissionToRole(Guid permissionId)
        {
            // find all roles.
            // find the roles that include to the permission.
            using (var roleService = ServiceLocator.Instance.Resolve<IRoleService>())
            using (var permissionService = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var roles = roleService.FindAllRoles();
                var rolesOfPermission = permissionService.FindRolesOfPermission(permissionId);

                ViewBag.Roles = roles;
                ViewBag.RolesOfPermission = rolesOfPermission;
                var model = new ActionsDto {ID = permissionId};
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttachPermissionToRole(ActionsDto model, Guid[] roles)
        {
            var handler = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.AttachPermissionToRoles(model.ID, roles, handler);
                }
            }
            catch (Exception)
            {
                return Json(false, "Attach the permission to roles failure.");
            }

            return Json(true);
        }

        #endregion


        #region Menu

        public ActionResult CreateMenu()
        {
            // In ui, check the permissions whether exist.
            this.ViewBags();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMenu(MenuDto model)
        {
            model.CreatedBy = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.CreateMenu(model);
                }
            }
            catch (Exception)
            {
                return Json(false, "Create the menu failure.");
            }
            return Json(true);
        }

        public ActionResult EditMenu(Guid MenuId)
        {
            this.ViewBags();
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var model = service.FindMenu(MenuId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenu(MenuDto model)
        {
            model.UpdatedBy = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.ModifyMenu(model);
                }
            }
            catch (Exception)
            {
                return Json(false, "Modify the menu failure.");
            }
            return Json(true);
        }

        [HttpPost]
        public ActionResult DeleteMenu(Guid menuId)
        {
            var handler = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.RemoveMenu(menuId, handler);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the menu failure.");
            }

            return Json(true);
        }

        public ActionResult AttachMenuToRole(Guid menuId)
        {
            // find all roles.
            // find the roles that include to the menu.
            using (var roleService = ServiceLocator.Instance.Resolve<IRoleService>())
            using (var menuService = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var roles = roleService.FindAllRoles();
                var rolesOfMenu = menuService.FindRolesOfMenu(menuId);

                ViewBag.Roles = roles;
                ViewBag.RolesOfMenu = rolesOfMenu;
                var model = new MenuDto {ID = menuId};
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttachMenuToRole(MenuDto model, Guid[] roles)
        {
            var handler = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
                {
                    service.AttachMenuToRoles(model.ID, roles, handler);
                }
            }
            catch (Exception)
            {
                return Json(false, "Attach the menu to roles failure.");
            }

            return Json(true);
        }

        #endregion

        #region Private Methods

        private void ViewBags()
        {
            ViewBag.Permissions = this.GetAllPermissions();
            ViewBag.ParentMenus = this.GetParentMenus();
            ViewBag.Sorts = this.GetAvailableMenuSorts();
        }

        private IEnumerable<SelectListItem> GetAllPermissions()
        {
            IEnumerable<ActionsDto> permissions;
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                permissions = service.FindAllPermissions();
            }
            if (permissions == null)
                return null;

            return (from permission in permissions
                let t =
                    String.IsNullOrWhiteSpace(permission.Area)
                        ? string.Format("{0} / {1}", permission.Controller, permission.Action)
                        : string.Format("{0} / {1} / {2}", permission.Area, permission.Controller, permission.Action)
                select new SelectListItem {Value = permission.ID.ToString(), Text = t});
        }

        private IEnumerable<SelectListItem> GetParentMenus()
        {
            IEnumerable<MenuDto> menus;
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                menus = service.FindMenusViaLevel(MenuLevelDto.Parent);
            }
            if (menus == null)
                return null;

            return (from menu in menus
                select new SelectListItem {Value = menu.ID.ToString(), Text = menu.DisplayName});
        }

        private IEnumerable<SelectListItem> GetAvailableMenuSorts(Guid? parentId = null)
        {
            var count = 0;

            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                if (parentId.HasValue)
                {
                    var menus = service.FindMenuWithChildren(parentId.Value);
                    if (menus != null && menus.Any())
                        count = menus.Where(m => m.Level == MenuLevelDto.Children).Max(m => m.Sort);
                }
                else
                {
                    var menus = service.FindMenusViaLevel(MenuLevelDto.Parent);
                    if (menus != null && menus.Any())
                        count = menus.Max(m => m.Sort);
                }
            }

            return
                Enumerable.Range(1, count + 1)
                    .Select(s => new SelectListItem { Text = s.ToString(), Value = s.ToString(), Selected = (s == count + 1) });
        }
    }

    #endregion
}
