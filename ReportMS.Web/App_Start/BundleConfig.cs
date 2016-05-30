using System.Web.Optimization;

namespace ReportMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.form.js"));

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

            RegistersStartupBundles(bundles); // startup
            RegistersReportBundles(bundles); // Report
            RegistersMenuBundles(bundles); // menu
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
            bundles.Add(new StyleBundle("~/Content/data_tables").Include(
                "~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js"));
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
            // JSXTransformer should be replace by babel
            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                "~/Scripts/react/react.js",
                "~/Scripts/react/react-with-addons.js",
                "~/Scripts/react/react-dom.js",
                "~/Scripts/react/react-dom-server.js",
                "~/Scripts/react/JSXTransformer.js"));
        }

        #endregion

        #region

        static void RegistersStartupBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/startupui").Include(
                "~/Content/layout.css"));

            bundles.Add(new ScriptBundle("~/bundles/startup").Include(
                "~/Js/jquery.dialog.js",
                "~/Js/startup.js"));
        }

        // Register Report
        static void RegistersReportBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/report").Include(
                "~/Js/reportTable.js",
                "~/Js/reportTransfer.js"));
        }

        static void RegistersMenuBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/menu").Include(
                "~/Js/equalHeight.js",
                "~/Js/jquery.tabSlideOut.v1.3.js"));
        }

        #endregion
    }
}
