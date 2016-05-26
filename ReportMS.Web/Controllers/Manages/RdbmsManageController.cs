using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gear.Infrastructure;
using Gear.Infrastructure.Storage;
using Gear.Infrastructure.Storage.Config;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
    public class RdbmsManageController : BaseController
    {
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<IRdbmsService>())
            {
                var model = service.FindAllRdbms();
                return View(model);
            }
        }

        public ActionResult _Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<IRdbmsService>())
            {
                var model = service.FindAllRdbms();
                return PartialView(model);
            }
        }

        public ActionResult Create()
        {
            this.GetViewBags();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RdbmsDto model)
        {
            if (!this.TestConnection(model))
                return Json(false, "Connect the RDBMS failure, please check your database server account.");

            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRdbmsService>())
                {
                    service.CreateRdbms(model);
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false, "Create the RDBMS failure.");
            }
        }

        public ActionResult Edit(Guid rdbmsId)
        {
            this.GetViewBags();
            using (var service = ServiceLocator.Instance.Resolve<IRdbmsService>())
            {
                var model = service.FindRdbms(rdbmsId);
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RdbmsDto model)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRdbmsService>())
                {
                    service.UpdateRdbms(model);
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false, "Modify the RDBMS failure.");
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid rdbmsId)
        {
            try
            {
                using (var service = ServiceLocator.Instance.Resolve<IRdbmsService>())
                {
                    service.RemoveRdbms(rdbmsId);
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false, "Delete the RDBMS failure.");
            }
        }

        #region Private Methods

        private void GetViewBags()
        {
            ViewBag.Providers = this.GetDatabaseProvider();
        }

        private IEnumerable<SelectListItem> GetDatabaseProvider()
        {
            var providers = new[] { RdbmsProvider.MSSQL, RdbmsProvider.Oracle };

            return (from provider in providers
                select new SelectListItem {Value = provider, Text = provider});
        }

        private bool TestConnection(RdbmsDto rdbms)
        {
            var connectionOpt = new ConnectionOptions
            {
                DataSource = rdbms.Server,
                InitialCatalog = rdbms.Catalog,
                UserId = rdbms.UserId,
                Password = rdbms.Password,
                ReadOnly = rdbms.ReadOnly
            };

            var connTest = StorageManager.ConnectionTest(connectionOpt, rdbms.Provider);
            return connTest.Test();
        }

        #endregion
    }
}