using System;
using System.Web.Mvc;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.Web.Controllers.Manages
{
    public class ReportProfileManageController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProfile()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(ReportProfileDto model)
        {
            return Json(true);
        }

        public ActionResult EditProfile()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ReportProfileDto model)
        {
            return Json(true);
        }

        [HttpPost]
        public ActionResult DeleteProfile(Guid profileId)
        {
            return Json(true);
        }
    }
}