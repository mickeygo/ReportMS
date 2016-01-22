using System;
using System.Linq;
using Gear.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.DataTransferObjects.DtoInitializer;

namespace ReportMS.Test.Dtos
{
    [TestClass]
    public class DtoTest
    {
        [TestMethod]
        public void GetAssemblies_Test()
        {
            var dtoMapper = new DtoMapperInitializer();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var rmsAssemblies = assemblies.Where(a => a.FullName.Contains("ReportMS"));

            var types = rmsAssemblies.SelectMany(a => a.GetTypes());

            Assert.IsNotNull(types);
        }

        [TestMethod]
        public void GetAssemblies2_Test()
        {
            var currentDomain = AppDomain.CurrentDomain;

            var baseDir = currentDomain.BaseDirectory;

            var dir2 = System.IO.Path.GetDirectoryName(@"F:\\Advantech\\ReportMS\\ReportMS\\ReportMS.Test.Dtos\\bin\\Debug\\ReportMS.Test.Dtos.dll");

            Assert.IsTrue(baseDir == dir2);

            //Make an array for the list of assemblies.
            var assemblies = currentDomain.GetAssemblies();

            var rmsAssemblies = assemblies.Where(a => System.IO.Path.GetDirectoryName(a.Location) == baseDir);

            var types = rmsAssemblies.SelectMany(a => a.GetTypes());

            Assert.IsNotNull(types);
        }

        [TestMethod]
        public void GetAssemblies3_Test()
        {
            var dtoMapper = new DtoMapperInitializer();


            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var rmsAssemblies = assemblies.Where(a => a.FullName.Contains("ReportMS")).ToList();

            //var types = rmsAssemblies.SelectMany(a => a.GetTypes().Where(s => typeof(IApplicationStartup).IsAssignableFrom(s))).ToList();

            var types = (from a in AppDomain.CurrentDomain.GetAssemblies()
                         where a.GlobalAssemblyCache == false
                         from t in a.GetTypes()
                         where t.IsClass && typeof(IApplicationStartup).IsAssignableFrom(t) 
                         select t).ToList();

            Assert.IsNotNull(types);
        }
    }
}
