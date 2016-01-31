using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ReportMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.MonitorProfiler(); // 要在 DbContext 之前初始化
            BootStrapper.Start();
        }

#if DEBUG
        protected void Application_BeginRequest()
        {
            if (this.Request.IsLocal)
                StackExchange.Profiling.MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }
#endif

        [Conditional("DEBUG")]
        void MonitorProfiler()
        {
            StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();
        }
    }
}
