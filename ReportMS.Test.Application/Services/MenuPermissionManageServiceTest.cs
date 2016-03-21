using System;
using Gear.Infrastructure;
using Gear.Infrastructure.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.ServiceContracts;
using ReportMS.Test.Common;

namespace ReportMS.Test.Application.Services
{
    [TestClass]
    public class MenuPermissionManageServiceTest
    {
        [TestInitialize]
        public void Init()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            BootStrapper.Start();
        }


        [TestMethod]
        public void CreateMenu_Test()
        {
            var menu = new MenuDto
            {
                MenuName = "TestMenu",
                DisplayName = "My test menu",
                Description = "My test menu description",
                Level = MenuLevelDto.Parent,
                Sort = 1,
                CreatedBy = "gang.yang@advantech.com.cn"
            };

            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                service.CreateMenu(menu);
            }
        }

        [TestMethod]
        public void UpdateMenu_Test()
        {
            using (var service = ServiceLocator.Instance.Resolve<IMenuPermissionService>())
            {
                var menu = service.FindMenu(new Guid("0419F21C-61F9-CC70-95F3-08D3513BD1D1"));
                menu.Description = "My test menu description test";
                menu.UpdatedBy = "gang.yang@advantech.com.cn";
                
                service.ModifyMenu(menu);
            }
        }
    }
}
