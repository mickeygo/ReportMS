using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportMS.Web.Startup))]
namespace ReportMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
