using System;
using System.Web.Mvc;
using Gear.Infrastructure;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Web.Client.Attributes;

namespace ReportMS.Web.Controllers.Manages
{
    [Role]
    public class TenementManageController : BaseController
    {
        // GET: Tenement
        public ActionResult Index()
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                var tenants = service.GetAllTenants();
                return View(tenants);
            }
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TenantDto model)
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                var existTenant = service.GetTenant(model.TenantName);
                if (existTenant != null)
                    return Json(false, "The tenant name already exists.");

                model.CreatedBy = this.LoginUser.Identity.Name;
                var tenant = service.CreateTenant(model);
                return Json(true, tenant);
            }
        }

        public ActionResult Edit(Guid tenantId)
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                var tenant = service.GetTenant(tenantId);
                return PartialView(tenant);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TenantDto model)
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                model.UpdatedBy = this.LoginUser.Identity.Name;
                var tenant = service.UpdateTenant(model);
                return Json(true, tenant);
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid tenantId)
        {
            using (var service = ServiceLocator.Instance.Resolve<ITenantService>())
            {
                var disableBy = this.LoginUser.Identity.Name;
                service.DeleteTenant(tenantId, disableBy);
                return Json(true);
            }
        }
    }
}