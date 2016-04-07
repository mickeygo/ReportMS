using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using Gear.Infrastructure.Web.Attributes;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers
{
    [AllowAuthenticated]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.IsManager = this.IsAdmin || this.IsAdministrator;

            var userId = this.LoginUser.NameIdentifier.Value;
            using (var service = ServiceLocator.Instance.Resolve<IUserService>())
            {
                //  Only show these tenants own to current user
                var tenantsOfRole = service.FindRoles(userId);
                if (tenantsOfRole == null)
                    return View();

                var model = tenantsOfRole.Where(t => t.TenantId.HasValue).Select(r => r.Tenant);
                return View(model);
            }
        }
    }
}