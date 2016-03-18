using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                // Todo: only show these tenants own to current user
                
                var tenants = service.GetAllTenants();
                return View(tenants);
            }
        }
    }
}