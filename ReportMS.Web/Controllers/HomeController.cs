using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                var tenants = service.GetAllTenants();
                return View(tenants);
            }
        }
    }
}