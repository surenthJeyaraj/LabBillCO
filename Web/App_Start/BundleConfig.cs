using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*", "~/Scripts/jquery.json-2.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundle/toastr").Include(
                "~/Scripts/toastr.js"
                ));
            bundles.Add(new ScriptBundle("~/bundle/duallistbox").Include(
               "~/Scripts/jquery.bootstrap-duallistbox.js"
               ));
            bundles.Add(new StyleBundle("~/Content/toastr").Include("~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/duallistbox").Include("~/Content/bootstrap-duallistbox.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryaddons").Include(
                        "~/Scripts/JqueryAddOn/jquery.widget.min.js*",
                        "~/Scripts/JqueryAddOn/jquery.mousewheel.js",
                        "~/Scripts/JqueryAddOn/jquery_002.js",
                         "~/Scripts/JqueryAddOn/jquery_003.js",
                         "~/Script/JqueryAddOn/jquery-3.1.1.slim"
                        ));

        

            //bundles.Add(new ScriptBundle("~/bundles/metro").Include("~/Scripts/metro.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/metro").Include("~/Scripts/metro.js", "~/Scripts/metro/metro-hint.js", "~/Scripts/metro/metro-datepicker.js", "~/Scripts/metro/metro-calendar.js", "~/Scripts/metro/metro-dialog.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                "~/Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js",
                "~/Scripts/jquery.dataTables.columnFilter.js",
                "~/Scripts/dataTables.bootstrap.js",
                "~/Scripts/bootstrap-switch.js",
                 "~/Scripts/chosen.jquery.js",
                 "~/Scripts/jquery.idletimeout.js",
                 "~/Scripts/jquery.idletimer.js"
                ));
            //bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
            //   "~/Scripts/jquery.tablesorter.pager.js"
            //   )); 
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js" 
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css", "~/Content/daterangepicker-bs2.css", "~/Content/daterangepicker-bs3.css"));
            bundles.Add(new StyleBundle("~/Content/metro").Include("~/Content/metro.css").Include("~/Content/metro-icons.css").Include("~/Content/metro-colors.css").Include("~/Content/metro-rtl.css").Include("~/Content/metro-schemes.css"));
            //bundles.Add(new StyleBundle("~/Content/metro").Include("~/Content/metro-bootstrap.css").Include("~/Content/metro-bootstrap-responsive.css").Include("~/Content/metro-icons.css").Include("~/Content/metro-colors.css").Include("~/Content/metro-rtl.css").Include("~/Content/metro-schemes.css"));

            //bundles.Add(new StyleBundle("~/Content/metro").Include("~/Content/metro-bootstrap.css").Include("~/Content/metro-bootstrap-responsive.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css").Include("~/Content/bootstrap-theme.css").Include("~/Content/bootstrap-switch.css").Include("~/Content/Chosen.css"));
           // bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css").Include("~/Content/bootstrap-theme.css"));


            bundles.Add(new StyleBundle("~/Content/jquerydatatable").Include(
                "~/Content/DataTables-1.9.4/media/css/jquery.dataTables.css",
                "~/Content/DataTables-1.9.4/media/css/jquery.dataTables_themeroller.css"));

            bundles.Add(new ScriptBundle("~/bundles/jsrender").Include("~/Scripts/jsrender.js"));

           

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}