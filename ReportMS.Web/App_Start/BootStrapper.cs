using Gear.Infrastructure.Application;
using Gear.Infrastructure.Web.WebInitializer;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.Domain.Repositories.EntityFramework.DbContextInitializer;

namespace ReportMS.Web
{
    /// <summary>
    /// 应用程序引导程序
    /// </summary>
    public static class BootStrapper
    {
        /// <summary>
        /// 启动引导程序
        /// </summary>
        public static void Start()
        {
            AppRuntimeStart();
        }

        static void AppRuntimeStart()
        {
            AppBootstrapper.Register<RmsDbContextInitializer>();
            AppBootstrapper.Register<DtoMapperInitializer>();
            AppBootstrapper.Register<OwinClaimInitializer>();

            AppRuntime.Initialize();
            AppRuntime.Instance.CurrentApplication.Start();
        }
    }
}