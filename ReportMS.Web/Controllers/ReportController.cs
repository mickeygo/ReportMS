using System;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.Reports.Managers;
using ReportMS.ServiceContracts;
using ReportMS.Web.Attributes;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers
{
    [ValidateTenant]
    public class ReportController : BaseController
    {
        private static readonly string ExcelName = "__excelName";
        private static readonly string ExcelKey = "__excel";

        // GET: Report
        [Layout(Layout.WithoutWebTitle, Wide = Wide.Widescreen90)]
        public ActionResult Index()
        {
            return View("_ReportLayout");
        }

        [HttpPost]
        public ActionResult GetReports()
        {
            // Find the role of the current user in current tenant.
            // remark: every one in tenant only has one role.

            var reportQuery = ServiceLocator.Instance.Resolve<IReportQueryService>();
            var roleId = this.GetUserRoleId();
            var reportProfiles = reportQuery.GetReportProfiles(roleId);
            if (reportProfiles == null)
                return Json(false, "There are not any report in this current for you.");

            var model = reportProfiles.Select(s => new { s.ID, s.Name, s.ReportId });
            return Json(new { reportProfiles = model, status = "success" });
        }

        [HttpPost]
        public ActionResult GetFields(Guid reportProflieId)
        {
            var reportQuery = ServiceLocator.Instance.Resolve<IReportQueryService>();
            var report = reportQuery.GetReportWithProfile(reportProflieId);
            var model = report.Fields.OrderBy(s => s.Sort).Select(s => new { s.FieldName, s.DisplayName, s.DataType, s.Sort });

            return Json(new { fileds = model, status = "success" });
        }

        [HttpPost]
        public ActionResult GetDataSet()
        {
            var reportRead = ServiceLocator.Instance.Resolve<IReportRead>();
            var model = reportRead.ExecuteDataTablesQuery();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveExcel()
        {
            try
            {
                var reportRead = ServiceLocator.Instance.Resolve<IReportRead>();
                var fileName = reportRead.TableOrViewName;
                var fileBytes = reportRead.ExecuteExcelExport(fileName);
                TempData.Add(ExcelName, fileName);
                TempData.Add(ExcelKey, fileBytes);

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public void ExportExcel()
        {
            if (!TempData.ContainsKey(ExcelKey))
                return;

            var fileName = TempData[ExcelName] as string;
            var fileBytes = TempData[ExcelKey] as byte[];

            this.Output.OutPutExcel(fileBytes, fileName);
        }

        #region Private Methods

        private Guid GetUserRoleId()
        {
            var userId = this.LoginUser.NameIdentifier;
            using (var service = ServiceLocator.Instance.Resolve<IUserService>())
            {
                var role = service.FindRole(userId.Value, this.Tenant.ID);
                return role.ID;
            }
        }

        #endregion
    }
}