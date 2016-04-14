using System;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;
using ReportMS.Web.Client.Membership;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
    [ValidateTenant]
    public class UserManageController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Index()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttachRole(UserDto model, Guid? role)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IUserService>())
                {
                    var creator = this.LoginUser.Identity.Name;
                    service.SetRoles(this.LoginUser.NameIdentifier.Value, this.Tenant.ID, role, creator);

                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Set the user role failure.");
            }
        }

        #region Query

        public ActionResult FindUser(string userName)
        {
            // use the current tenant to lookup.
            // lookup for the user in local store (only).
            // find the user all roles.

            using (var userService = ServiceLocator.Instance.Resolve<IUserService>())
            using (var roleService = ServiceLocator.Instance.Resolve<IRoleService>())
            {
                var user = userService.FindUser(userName);
                if (user == null)
                {
                    UserManager.Instance.CreateUser(userName); // add user to local
                    user = userService.FindUser(userName);
                    if (user == null)
                        return PartialView();
                }

                var role = userService.FindRole(user.ID, this.Tenant.ID);
                var rolesOfTenant = roleService.FindRoles(this.Tenant.ID);

                ViewBag.RoleOfUser = role;
                ViewBag.RolesOfTenant = rolesOfTenant;

                return PartialView(user);
            }
        }

        #endregion
    }
}