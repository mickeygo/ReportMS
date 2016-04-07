using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;
using ReportMS.Web.Client.Models;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
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

        public ActionResult _Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportService>())
            {
                var model = service.FindAllReport();
                return PartialView(model);
            }
        }

        public ActionResult CreateReport()
        {
            var model = new ReportDto {Schema = "dbo"};
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReport(ReportDto model)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportService>())
            {
                // check whether has existed repeat report name
                var isExistReport = service.ExistReport(model.ReportName);
                if (isExistReport)
                    return Json(false, string.Format("The report name:[{0}] already exists.", model.ReportName));

                // Todo: Check the database can connect successfully

                // Create the report header
                model.CreatedBy = this.LoginUser.Identity.Name;
                var report = service.CreateReport(model);

                return Json(true, report);
            }
        }

        public ActionResult EditReport(Guid reportId)
        {
            using (var service = ServiceLocator.Instance.Resolve<IReportService>())
            {
                var report = service.FindReport(reportId);
                var tableSchemas = this.GetTableSchema(report.Database, report.ReportName);

                var model = new ReportModifyViewModel {Report = report, TableSchemas = tableSchemas};

                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReport(ReportModifyViewModel model, IEnumerable<string> colunms)
        {
            try
            {
                var creator = this.LoginUser.Identity.Name;
                var reportDto = model.Report;
                using (var service = ServiceLocator.Instance.Resolve<IReportService>())
                {
                    // modify the report header
                    service.UpdateReportHeader(reportDto.ID, reportDto.DisplayName, reportDto.Description, creator);

                    // modify the report fields (add / remove)
                    var report = service.FindReport(reportDto.ID);
                    var tableSchemas = this.GetTableSchema(report.Database, report.ReportName);

                    // firstly remove all，then add and sort
                    IEnumerable<ReportFieldDto> addingFileds = null;
                    if (colunms != null)
                    {
                        addingFileds = (from table in tableSchemas
                            where colunms.Contains(table.ColunmName)
                            select new ReportFieldDto
                            {
                                FieldName = table.ColunmName,
                                DisplayName = table.ColunmName,
                                DataType = table.DataType,
                                CreatedBy = creator
                            });
                    }

                    service.SetReportFields(reportDto.ID, addingFileds);
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false, "Update the report failure.");
            }
        }

        [HttpPost]
        public ActionResult DeleteReport(Guid reportId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IReportService>())
                {
                    service.DeleteReport(reportId);
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false, "Delete the report failure.");
            }
        }

        #region Others Query

        // 显示所有的可用的配置
        private IEnumerable<DatabaseSchemaDto> GetDatabaseSchema(string connectionName)
        {
            var schemaService = ServiceLocator.Instance.Resolve<IReportSchemaQueryService>();
            return schemaService.GetDatabaseSchema(connectionName);
        }

        // 显示 Table / View 的信息(字段名及类型等)
        private IEnumerable<TableSchemaDto> GetTableSchema(string connectionName, string table)
        {
            var schemaService = ServiceLocator.Instance.Resolve<IReportSchemaQueryService>();
            return schemaService.GetTableSchema(connectionName, "dbo", table);
        }

        #endregion
    }
}