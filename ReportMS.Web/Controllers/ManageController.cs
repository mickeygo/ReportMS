using System.Web.Mvc;

namespace ReportMS.Web.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        [OutputCache(Duration = 3600)]
        public ActionResult Menu()
        {
            return PartialView();
        }
    }
}