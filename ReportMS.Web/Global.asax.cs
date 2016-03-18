using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;

namespace ReportMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _backgroundJobServer;

        protected void Application_Start()
        {
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.MonitorProfiler(); // 要在 DbContext 之前初始化
            BootStrapper.Start();

            // Start Job
            // Todo: Pack the job configuration to a package.
            GlobalConfiguration.Configuration.UseSqlServerStorage(GetDbConnectionName("rms"));
            _backgroundJobServer = new BackgroundJobServer();
            Client.Jobs.JobClient.Start();
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

        protected void Application_End(object sender, EventArgs e)
        {
            _backgroundJobServer.Dispose();
        }

        #region Private Methods

        [Conditional("DEBUG")]
        void MonitorProfiler()
        {
            StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();
        }

        string GetDbConnectionName(string name)
        {
#if DEBUG
            return name + "Debug";
#else
                    return name;
#endif
        }

        #endregion
    }
}
