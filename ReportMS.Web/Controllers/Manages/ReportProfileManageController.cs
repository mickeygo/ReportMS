using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers.Manages
{
    public class ReportProfileManageController : BaseController
    {
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                var model = service.FindAllReportProfile();
                return View(model);
            }
        }

        public ActionResult CreateProfile()
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportService>())
            {
                var reports = service.FindAllReport();
                var reportList = (from report in reports
                    orderby report.DisplayName
                    select new SelectListItem {Text = report.DisplayName, Value = report.ID.ToString()});

                ViewBag.Reports = reportList;
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(ReportProfileDto model)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
                {
                    var isExistProfile = service.ExistReportProfile(model.Name);
                    if (isExistProfile)
                        return Json(false, string.Format("Create the report profile failure. The profile name:[{0}] already exists.", model.Name));

                    model.CreatedBy = this.LoginUser.Identity.Name;
                    service.AddReportProfile(model);
                    return Json(true);
                }
            }
            catch(Exception)
            {
                return Json(false, "Create the report profile failure.");
            }
        }

        public ActionResult EditProfile(Guid profileId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                var model = service.FindReportProfile(profileId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ReportProfileDto model, IEnumerable<string> fields)
        {
            var editor = this.LoginUser.Identity.Name;
            using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                // Update the report profile header
                service.UpdateReportProfile(model.ID, model.Name, model.Description, editor);

                // Update the fields (remove all, and then add)
                service.SetProfileFields(model.ID, fields);

                return Json(true);
            }
        }

        [HttpPost]
        public ActionResult DeleteProfile(Guid profileId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
                {
                    service.RemoveReportProfile(profileId);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the report profile failure.");
            }
        }

        #region Query

        public ActionResult FindProfile(Guid profileId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                var model = service.FindReportProfile(profileId);
                return PartialView(model);
            }
        }

        #endregion
    }
}