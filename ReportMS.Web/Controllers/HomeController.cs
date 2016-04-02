using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var userId = this.LoginUser.NameIdentifier.Value;
            using (var service = ServiceLocator.Instance.Resolve<IUserService>())
            {
                //  Only show these tenants own to current user
                var tenantsOfRole = service.FindRoles(userId);
                var model = tenantsOfRole.Select(r => r.Tenant);
                return View(model);
            }
        }
    }
}