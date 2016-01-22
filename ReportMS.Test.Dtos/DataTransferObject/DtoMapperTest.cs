using System;
using Gear.Infrastructure.Application;
using Gear.Utility.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;
using ReportMS.Test.Common;

namespace ReportMS.Test.Dtos.DataTransferObject
{
    [TestClass]
    public class DtoMapperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            AppBootstrapper.Register<DtoMapperInitializer>();
            Helper.Init();
        }

        [TestMethod]
        public void ReportDtoMapper_Test()
        {
            var report = new Report("ReportNameTest", "ReportDisplayNameTest", "ReportDescTest", new Database("RMS", "dbo"), "gang.yang");
            var reportDto = AutoMapperAdapter.Adapt<ReportDto>(report);

            Assert.IsTrue(reportDto.Schema == "dbo");
        }

        [TestCleanup]
        public void Cleanup()
        {
            AutoMapperAdapter.Reset();
        }
    }
}
