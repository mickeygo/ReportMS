using System.Web.Optimization;

namespace ReportMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Required
            RegisterjQueryUIBundles(bundles); // jQuery UI
            RegistersEsShimBundles(bundles);  // es-shim
            RegistersReactBundles(bundles); // react
            RegisterDataTablesBundles(bundles); // dataTables

            // startup
            RegistersStartupBundles(bundles); // startup
            
            // 
            RegistersReportBundles(bundles); // Report
        }

        #region Required

        // Register jQuery UI
        static void RegisterjQueryUIBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
               "~/Content/jquery-ui-{version}.custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui-timepicker-addon.js"));
        }

        // Register dataTables
        static void RegisterDataTablesBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/dataTables").Include(
                "~/Content/DataTables/css/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js"));
        }

        // Register es-shim
        static void RegistersEsShimBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/esShim").Include(
                "~/Scripts/es5-shim.js",
                "~/Scripts/es5-sham.js"));
        }

        // Register React
        static void RegistersReactBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                "~/Scripts/react/react-{version}.js",
                "~/Scripts/react/JSXTransformer-{version}.js",
                "~/Scripts/react/react-with-addons-{version}.js"));
        }

        #endregion

        #region

        static void RegistersStartupBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/startupUi").Include(
                "~/Content/component.css"));

            bundles.Add(new ScriptBundle("~/bundles/startup").Include(
                "~/Js/startup.js"));
        }

        // Register Report
        static void RegistersReportBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/report").Include(
                "~/Js/reportTable.js",
                "~/Js/reportTransfer.js"));
        }

        #endregion
    }
}
