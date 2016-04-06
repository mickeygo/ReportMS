using System.Collections.Generic;
using ReportMS.Web.Client.Membership;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers
{
    // All In One Manage
    [Role]
    public class ManageController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Tenants = this.GetTenants();
            return View();

            //var roles = this.GetRoles();
            //if (roles == null)
            //    return View();

            //ViewBag.Tenants = this.GetTenants();
            
            //using (var service = ServiceLocator.Instance.Resolve<IRoleService>())
            //{
            //    var model = (from role in roles
            //        from menu in service.FindMenus(role.ID)
            //        select menu).ToList();

            //    return View(model);
            //}
        }

        public ActionResult ReportSet()
        {
            return View();
        }

        public ActionResult UserSet()
        {
            return View();
        }

        #region Private Methods

        private IEnumerable<TenantDto> GetTenants()
        {
            // Todo: 1, Show the administrator menus (if the login user owns to administrator member)
            // 2, Show the user tenants
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                return service.GetAllTenants();
            }

            //if (this.IsAdministrator)
            //{
            //    using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            //    {
            //        return service.GetAllTenants();
            //    }
            //}

            //var roles = this.GetRoles();
            //return roles != null ? roles.Select(r => r.Tenant) : null;
        }

        private IEnumerable<RoleDto> GetRoles()
        {
            var userId = this.LoginUser.NameIdentifier.Value;
            return UserManager.Instance.GetRoles(userId);
        }

        #endregion
    }
}