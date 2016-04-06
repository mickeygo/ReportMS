using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
    public class ReportGroupManageController : BaseController
    {
        #region CURD

        public ActionResult Index()
        {
            var model = this.GetReportGroupsOfCurrentUser();
            return View(model);
        }

        public ActionResult _Index()
        {
            var model = this.GetReportGroupsOfCurrentUser();
            return PartialView(model);
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
                    service.RemoveReportGroup(reportGroupId, this.LoginUser.Identity.Name);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Delete the report group failure.");
            }
        }

        public ActionResult SetGroupItem(Guid reportGroupId)
        {
            // 筛选出那些没有添加到此 Report Group 中 Report Profile
            using (var groupService = ServiceLocator.Instance.Resolve<IReportGroupService>())
            using (var profileService = ServiceLocator.Instance.Resolve<IReportProfileService>())
            {
                var reportGroup = groupService.FindReportGroup(reportGroupId);
                var profiles = profileService.FindAllReportProfile();

                ViewBag.Profiles = profiles;
                return PartialView(reportGroup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetGroupItem(ReportGroupDto model, IEnumerable<Guid> profiles)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
                {
                    service.SetReportGroupItems(model.ID, profiles);
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false, "Set the report group items failure.");
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

        #region Private Methods

        private IEnumerable<ReportGroupDto> GetReportGroupsOfCurrentUser()
        {

            using (var service = ServiceLocator.Instance.Resolve<IReportGroupService>())
            {
                // Todo: release use the current role to filter
                return service.FindReportGroups();
            }
        }

        #endregion
    }
}