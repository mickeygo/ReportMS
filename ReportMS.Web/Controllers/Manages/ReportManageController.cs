using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;

namespace ReportMS.Web.Controllers.Manages
{
    public class ReportManageController : BaseController
    {
        // GET: ReportManage
        // 显示全都的 Report 配置
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportService>())
            {
                var model = service.FindAllReport();
                return View(model);
            }
        }

        public ActionResult CreateReport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReport(ReportDto model)
        {
            return View();
        }

        public ActionResult EditReport(Guid reportId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReport(ReportDto model)
        {
            return View();
        }

        public ActionResult AddField(Guid reportId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddField(Guid reportId, IEnumerable<string> fields)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditField(Guid reportId, IEnumerable<string> fields)
        {
            return View();
        }

        #region Others Query

        // 显示所有的可用的配置
        public ActionResult Schema()
        {
            return View();
        }

        // 显示 Table / View 的信息(字段名及类型等)
        public ActionResult TableSchema(string database, string table)
        {
            return PartialView();
        }

        #endregion
    }
}