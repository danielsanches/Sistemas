namespace MVC
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/_Layout").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.dataTables.min.js",

                "~/Scripts/modernizr-*",
                "~/Scripts/select2.full.min.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jquery.mask_input.js",
                "~/Scripts/jquery.maskMoney.min.js",

                "~/Scripts/bootstrap.min.js",
                "~/Scripts/dataTables.responsive.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/lib/responsive.bootstrap.min.js",
                "~/Scripts/default.js",

                "~/Scripts/moment-with-locales.js",
                "~/Scripts/bootstrap-datetimepicker.js",
                
                "~/Scripts/holder.min.js",
                "~/Scripts/ie10-viewport-bug-workaround.js",

                "~/Scripts/jquery.matchHeight-min.js",
                "~/Scripts/bootstrap-switch.min.js",

                "~/Scripts/_Layout-initialize.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/dataTablesResponsive").Include("~/Scripts/lib/dataTables.responsive.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/dataTablesBootstrapResponsive").Include("~/Scripts/lib/responsive.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/_Layout").Include(
                "~/Content/select2.min.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/bootstrap.css",

                "~/Content/dataTables.bootstrap.min.css",
                "~/Content/ie10-viewport-bug-workaround.css",
                "~/Content/style.css",
                "~/Content/themes/flat-blue.css",

                "~/Content/font-awesome.min.css",
                "~/Content/animate.min.css",
                "~/Content/checkbox3.min.css",
                "~/Content/bootstrap-switch.min.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/_Login").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/jquery.blockUI.js",

                "~/Scripts/bootstrap.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/default.js",

                "~/Scripts/holder.min.js",
                "~/Scripts/ie10-viewport-bug-workaround.js",

                "~/Scripts/_Login-initialize.js"
                ));

            bundles.Add(new StyleBundle("~/Content/_Login").Include(
                "~/Content/bootstrap.css",
                "~/Content/dataTable/dataTables.bootstrap.min.css",
                "~/Content/ie10-viewport-bug-workaround.css",
                "~/Content/style.css",
                "~/Content/themes/flat-blue.css"
            ));
        }
    }
}
