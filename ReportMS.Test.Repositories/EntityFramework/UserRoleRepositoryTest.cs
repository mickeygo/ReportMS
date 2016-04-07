using System;
using System.Linq;
using Gear.Infrastructure.Repository.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.AccountModule;
using ReportMS.Domain.Repositories;
using ReportMS.Domain.Repositories.EntityFramework;
using ReportMS.Test.Common;
using Gear.Infrastructure;
using Gear.Infrastructure.Specifications;

namespace ReportMS.Test.Repositories.EntityFramework
{
    [TestClass]
    public class UserRoleRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Helper.Init();
        }

        [TestMethod]
        public void DbContext_Test()
        {
            using (var repositoryContext = new EntityFrameworkRepositoryContext(new RmsDbContext("rms")))
            {
                var userRoles = repositoryContext.Context.Set<UserRole>().ToList();
                repositoryContext.Context.Dispose();

                Assert.IsNotNull(userRoles);
            }
        }

        [TestMethod]
        public void Repository_Test()
        {
            var repository = ServiceLocator.Instance.Resolve<IUserRoleRepository>();
            var userRoles = repository.FindAll().ToList();
            repository.Context.Dispose();

            Assert.IsNotNull(userRoles);
        }

        [TestMethod]
        public void FindByUserAndTenant_Test()
        {
            var userId = new Guid("B879DAE6-9A40-4D6A-A4E6-374DDF649923");
            var tenantId = new Guid("4327E3EC-7B62-4E37-B77F-4389FBBBE9E2");

            var spec = Specification<UserRole>.Eval(u => u.UserId == userId)
                       .And(Specification<UserRole>.Eval(u => u.Role.TenantId == tenantId));
            var repository = ServiceLocator.Instance.Resolve<IUserRoleRepository>();
            var userRole = repository.Find(spec);

            Assert.IsNotNull(userRole);
        }
    }
}
