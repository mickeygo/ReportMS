using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    [ValidateTenant]
    public class RoleManageController : BaseController
    {
        public ActionResult Index()
        {
            var model = this.GetRolesOfCurrentTenant();
            return View(model);
        }

        public ActionResult _Index()
        {
            var model = this.GetRolesOfCurrentTenant();
            return PartialView(model);
        }

        public ActionResult CreateRole()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleDto model)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
                {
                    if (this.Tenant == null)
                        return Json(false, "Create the role failure, the role does not exist.");
                    if (service.ExistRole(model.RoleName))
                        return Json(false, string.Format("Create the role failure, the role name:[{0}] already exists.", model.RoleName));

                    model.TenantId = this.Tenant.ID; // 将 Role 附加到当前租户中
                    model.CreatedBy = this.LoginUser.Identity.Name;
                    service.CreateRole(model);

                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Create the role failure.");
            }
        }

        public ActionResult EditRole(Guid roleId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
            {
                var model = service.FindRole(roleId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleDto model)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
                {
                    model.UpdatedBy = this.LoginUser.Identity.Name;
                    service.UpdateRole(model);

                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Update the role failure.");
            }
        }

        [HttpPost]
        public ActionResult DeleteRole(Guid roleId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
                {
                    service.DeleteRole(roleId);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the role failure.");
            }
        }

        #region Report Manage

        public ActionResult AddReportGroup(Guid roleId)
        {
            using (var roleService = ServiceLocator.Instance.Resolve<IRoleService>())
            using (var repGroupService = ServiceLocator.Instance.Resolve<IReportGroupService>())
            {
                var reportGroups = repGroupService.FindReportGroups();
                var reportGroupsOfRole = roleService.FindReportGroupRoles(roleId);

                var model = new RoleDto { ID = roleId };
                ViewBag.ReportGroups = reportGroups;
                ViewBag.ReportGroupsOfRole = reportGroupsOfRole;
                
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReportGroup(RoleDto model, IEnumerable<Guid> reportGroups)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
                {
                    service.SetReportGroupRoles(model.ID, reportGroups, this.LoginUser.Identity.Name);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Add the report group failure.");
            }
        }

        #endregion

        #region Private Methods

        private IEnumerable<RoleDto> GetRolesOfCurrentTenant()
        {
            using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
            {
                return service.FindRoles(this.Tenant.ID);
            }
        }

        #endregion

    }
}