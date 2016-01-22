using Gear.Infrastructure.Application;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.Domain.Repositories.EntityFramework.DbContextInitializer;

namespace ReportMS.Web
{
    /// <summary>
    /// 应用程序引导程序
    /// </summary>
    public static class BootStrapper
    {
        public static void Start()
        {
            AppRuntimeStart();
        }

        static void AppRuntimeStart()
        {
            AppBootstrapper.Register<RmsDbContextInitializer>();
            AppBootstrapper.Register<DtoMapperInitializer>();

            AppRuntime.Initialize();
            AppRuntime.Instance.CurrentApplication.Start();
        }
    }
}