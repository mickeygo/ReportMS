using System;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.Reports.Managers;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers
{
    public class ReportController : BaseController
    {
        private static readonly string ExcelName = "__excelName";
        private static readonly string ExcelKey = "__excel";

        // GET: Report
        public ActionResult Index()
        {
            return View("_ReportLayout");
        }

        [HttpPost]
        public JsonResult GetReports()
        {
            var reportQuery = ServiceLocator.Instance.Resolve<IReportQueryService>();
            var reports = reportQuery.GetReports();
            var model = reports.Select(s => new { s.ID, s.DisplayName });
            return Json(new { reports = model, status = "success" });
        }

        [HttpPost]
        public JsonResult GetFields(Guid reportId)
        {
            var reportQuery = ServiceLocator.Instance.Resolve<IReportQueryService>();
            var report = reportQuery.GetReport(reportId);

            var model = report.Fields.OrderBy(s => s.Sort).Select(s => new { s.FieldName, s.DisplayName, s.DataType, s.Sort });
            return Json(new { fileds = model, status = "success" });
        }

        [HttpPost]
        public JsonResult GetDataSet()
        {
            var reportRead = ServiceLocator.Instance.Resolve<IReportRead>();
            var model = reportRead.ExecuteDataTablesQuery();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveExcel()
        {
            try
            {
                var reportRead = ServiceLocator.Instance.Resolve<IReportRead>();
                var fileName = reportRead.TableOrViewName;
                var fileBytes = reportRead.ExecuteExcelExport(fileName);
                TempData.Add(ExcelName, fileName);
                TempData.Add(ExcelKey, fileBytes);

                return Json(new { status = "success" });
            }
            catch (Exception)
            {
                return Json(new { status = "fail" });
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
    }
}