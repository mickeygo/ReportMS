using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Jobs;

namespace ReportMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            JobClient.Start();

            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                // Todo: only show the user tenants
                
                var tenants = service.GetAllTenants();
                return View(tenants);
            }
        }
    }
}