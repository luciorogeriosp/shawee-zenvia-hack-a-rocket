using System.Web;
using System.Web.Optimization;

namespace FrontEnd
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/css/bootstrap.min.css",
                "~/css/font-awesome.min.css",
                "~/css/smartadmin-production.min.css",
                "~/css/smartadmin-skins.min.css",
                "~/css/demo.min.css",
                "~/css/your_style.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/js/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/frontend").Include(
                "~/js/membro.js",
                "~/js/venda.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/js/app.config.js",
                "~/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "~/js/bootstrap/bootstrap.min.js",
                "~/js/notification/SmartNotification.min.js",
                "~/js/smartwidgets/jarvis.widget.min.js",
                "~/js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                "~/js/plugin/sparkline/jquery.sparkline.min.js",
                "~/js/plugin/jquery-validate/jquery.validate.min.js",
                "~/js/plugin/masked-input/jquery.maskedinput.min.js",
                "~/js/plugin/select2/select2.min.js",
                "~/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
                "~/js/plugin/msie-fix/jquery.mb.browser.min.js",
                "~/js/plugin/fastclick/fastclick.min.js",
                "~/js/demo.min.js",
                "~/js/app.min.js",
                "~/js/plugin/datatables/jquery.dataTables.min.js",
                "~/js/plugin/datatables/dataTables.colVis.min.js",
                "~/js/plugin/datatables/dataTables.tableTools.min.js",
                "~/js/plugin/datatables/dataTables.bootstrap.min.js",
                "~/js/plugin/datatable-responsive/datatables.responsive.min.js",
                "~/js/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/js/plugin/fullcalendar/jquery.fullcalendar.min.js"));

                BundleTable.EnableOptimizations = true;
            /*
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            */


        }
    }
}
