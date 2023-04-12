using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Helpers;
using System.Web.Optimization;

namespace LoginApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Contents/vendors/jquery/dist/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Contents/vendors/bootstrap/dist/js/bootstrap.bundle.min.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Contents/vendors/datatables.net/js/jquery.dataTables.min.js",
                      "~/Contents/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                      "~/Contents/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                      "~/Contents/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                      "~/Contents/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                      "~/Contents/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                      "~/Contents/vendors/datatables.net-buttons/js/buttons.print.min.js",
                      "~/Contents/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                      "~/Contents/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                      "~/Contents/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                      "~/Contents/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                      "~/Contents/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
                      "~/Contents/vendors/jszip/dist/jszip.min.js",
                      "~/Contents/vendors/pdfmake/build/pdfmake.min.js",
                      "~/Contents/vendors/pdfmake/build/vfs_fonts.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Contents/vendors/Chart.js/Chart.min.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                "~/Contents/build/js/custom.min.js"
                      ));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Contents/vendors/bootstrap/dist/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Contents/vendors/font-awesome/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/animate").Include(
                      "~/Contents/vendors/animate.css/animate.min.css"));
            bundles.Add(new StyleBundle("~/Content/CustomThemeStyle").Include(
                      "~/Contents/build/css/custom.min.css"));
            bundles.Add(new StyleBundle("~/Content/Datatables").Include(
                      "~/Contents/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/Contents/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                      "~/Contents/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                      "~/Contents/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                      "~/Contents/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"));
        }
    }

}
