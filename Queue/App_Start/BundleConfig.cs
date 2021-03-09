using System.Web;
using System.Web.Optimization;

namespace Queue
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Template files
            bundles.Add(new StyleBundle("~/css").Include(
                "~/assets/vendor_components/bootstrap/dist/css/bootstrap.css",
                "~/css/bootstrap-extend.css",
                "~/css/master_style.css",
                "~/css/skins/_all-skins.css",
                "~/assets/vendor_components/bootstrap-daterangepicker/daterangepicker.css",
                "~/assets/vendor_components/morris.js/morris.css",
                "~/assets/vendor_components/datatable/datatables.min.css",
                "~/assets/vendor_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"
                ));


            bundles.Add(new ScriptBundle("~/scripts").Include(
                "~/assets/vendor_components/jquery-3.3.1/jquery-3.3.1.js",
                "~/assets/vendor_components/jquery-ui/jquery-ui.js",
                "~/assets/vendor_components/bootstrap/dist/js/bootstrap.js",
                "~/assets/vendor_components/popper/dist/popper.min.js",
                "~/assets/vendor_components/moment/min/moment.min.js",
                "~/assets/vendor_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                "~/assets/vendor_components/bootstrap-daterangepicker/daterangepicker.js",
                "~/Scripts/jquery.blockUI.js",
                "~/assets/vendor_components/chart.js-master/Chart.min.js",
                "~/assets/vendor_components/jquery-slimscroll/jquery.slimscroll.js",
                "~/assets/vendor_components/fastclick/lib/fastclick.js.js",
                "~/assets/vendor_components/raphael/raphael.min.js",
                //"~/assets/vendor_components/morris.js/morris.min.js",
                "~/assets/vendor_components/datatable/datatables.min.js",
                "~/assets/vendor_components/select2/dist/js/select2.full.js",
                "~/assets/vendor_plugins/input-mask/jquery.inputmask.js",
                "~/assets/vendor_plugins/input-mask/jquery.inputmask.date.extensions.js",
                "~/assets/vendor_plugins/input-mask/jquery.inputmask.extensions.js",
                "~/assets/vendor_plugins/iCheck/icheck.min.js",
                "~/assets/vendor_plugins/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js",
                "~/js/template.js",
                "~/js/demo.js",
                "~/js/pages/dashboard.js",
                "~/Scripts/Global.js",
                "~/js/pages/advanced-form-element.js"
                ));


            bundles.Add(new StyleBundle("~/login/css").Include(
              "~/assets/vendor_components/bootstrap/dist/css/bootstrap.min.css",
              "~/css/bootstrap-extend.css",
              "~/css/master_style.css",
              "~/css/skins/_all-skins.css"
              ));

            bundles.Add(new ScriptBundle("~/login/scripts").Include(
               "~/assets/vendor_components/jquery-3.3.1/jquery-3.3.1.js",
               "~/assets/vendor_components/popper/dist/popper.min.js",
               "~/assets/vendor_components/bootstrap/dist/js/bootstrap.js"
               ));



        }
    }
}
