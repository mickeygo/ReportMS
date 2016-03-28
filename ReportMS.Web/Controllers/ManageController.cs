using System.Web.Mvc;

namespace ReportMS.Web.Controllers
{
    // All In One Manage
    public class ManageController : BaseController
    {
        public ActionResult Index()
        {
            // Manage Page

            // Show profiles

            // Show profiles include the tenant

            return View();
        }

        public ActionResult ReportSet()
        {
            return View();
        }

        public ActionResult UserSet()
        {
            return View();
        }

        #region

        [AllowAnonymous]
        [OutputCache(Duration = 3600)]
        public ActionResult Menu()
        {
            return PartialView();
        }

        #endregion
    }
}