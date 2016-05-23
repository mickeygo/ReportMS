using Gear.Infrastructure.Application;
using Gear.Infrastructure.Web.WebInitializer;
using ReportMS.DataTransferObjects.DtoInitializer;
using ReportMS.Domain.Repositories.EntityFramework.DbContextInitializer;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReportMS.Web.BootStrapper), "PreStart")]
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReportMS.Web.BootStrapper), "PostStart")]
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

        // 配置应用程序启动时需要加载的项
        static void AppRuntimeStart()
        {
            AppBootstrapper.Register<RmsDbContextInitializer>();
            AppBootstrapper.Register<DtoMapperInitializer>();
            AppBootstrapper.Register<OwinClaimInitializer>();

            AppRuntime.Initialize();
            AppRuntime.Instance.CurrentApplication.Start();
        }

        
        public static void PreStart()
        {

        }

        public static void PostStart()
        {
            
        }

    }
}