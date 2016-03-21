using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class MenuPermissionRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Helper.Init();
        }

        [TestMethod]
        public void CreateMenu_Test()
        {
            var menu = new Menu("TestMenu", "My test menu", "My test menu description", null, MenuLevel.Parent, 1, null, "gang.yang@advantech.com.cn");

            //using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            //{
            //    var context = repositoryContext.Context.Set<Menu>();
            //    context.Add(menu);
            //    repositoryContext.Commit();
            //}

            using (var myContext = new RmsDbContext("rms"))
            {
                myContext.Context.Set<Menu>().Add(menu);
                myContext.Context.SaveChanges();
            }
        }
    }
}
