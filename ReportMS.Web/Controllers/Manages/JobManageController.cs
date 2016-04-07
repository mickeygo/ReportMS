using System.Web.Mvc;
using ReportMS.Web.Client.Attributes;
using ReportMS.Web.Client.Jobs;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
    public class JobManageController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.JobStatus = JobSwitch.GetStatus();
            return View();
        }

        public ActionResult JobToggle(bool status)
        {
            if (status)
                JobSwitch.Run();
            else
                JobSwitch.Pause();

            return Json(true);
        }
    }
}