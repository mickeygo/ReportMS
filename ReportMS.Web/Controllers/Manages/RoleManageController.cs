using System;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers.Manages
{
    public class RoleManageController : BaseController
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult EditRole()
        {
            return PartialView();
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

        [HttpPost]
        public ActionResult AddReportGroup(Guid roleId, Guid reportGroupId)
        {
            return Json(true);
        }

        #endregion

    }
}