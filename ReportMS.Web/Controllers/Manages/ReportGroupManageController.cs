using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers.Manages
{
    public class ReportGroupManageController : BaseController
    {
        #region CURD

        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
            {
                // Todo: release use the current role to filter
                var model = service.FindReportGroups();
                return View(model);
            }
        }

        public ActionResult CreateGroup()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(ReportGroupDto model)
        {
            model.CreatedBy = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
                {
                    service.CreateReportGroup(model);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Create the report group failure.");
            }
        }

        public ActionResult EditGroup(Guid reportGroupId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
            {
                var model = service.FindReportGroup(reportGroupId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(ReportGroupDto model)
        {
            model.UpdatedBy = this.LoginUser.Identity.Name;
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
                {
                    service.UpdateReportGroup(model);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Create the report group failure.");
            }
        }

        [HttpPost]
        public ActionResult DeleteGroup(Guid reportGroupId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
                {
                    service.RemoveReportGroup(reportGroupId);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the report group failure.");
            }
        }

        public ActionResult AddGroupItem(Guid reportGroupId)
        {
            ViewBag.ReportGroupId = reportGroupId.ToString();

            // 筛选出那些没有添加到此 Report Group 中 Report Profile
            using (var groupService = ServiceLocator.Instance.Resolve<IReportGroupService>())
            using (var profileService = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                var reportGroup = groupService.FindReportGroup(reportGroupId);
                var profiles = profileService.FindAllReportProfile();

                if (profiles == null)
                    return PartialView();
                if (reportGroup == null || !reportGroup.ReportGroupItems.Any())
                    return PartialView(profiles);

                var model = (from profile in profiles
                    where reportGroup.ReportGroupItems.All(g => g.ReportProfileId != profile.ID)
                    select profile);

                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroupItem(Guid reportGroupId, IEnumerable<Guid> profiles)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
                {
                    service.AddReportGroupItems(reportGroupId, profiles);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Add the report group item failure.");
            }
        }

        [HttpPost]
        public ActionResult DeleteGroupItem(Guid reportGroupId, Guid reportGroupItemId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
                {
                    service.RemoveReportGroupItem(reportGroupId, reportGroupItemId);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the report group item failure.");
            }
        }

        #endregion

        #region Query

        public ActionResult FindGroup(Guid reportGroupId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
            {
                var model = service.FindReportGroup(reportGroupId);
                return PartialView(model);
            }
        }

        public ActionResult FindReport(Guid reportId)
        {
            return PartialView();
        }

        #endregion
    }
}