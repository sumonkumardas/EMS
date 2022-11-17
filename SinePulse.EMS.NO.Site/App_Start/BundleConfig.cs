// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using System.Web.Optimization;

namespace SinePulse.EMS.NO.Site {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
       

            bundles.Add(new StyleBundle("~/Content/css").Include(
                
                "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/assets/global/plugins/uniform/css/uniform.default.css",
                "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/jqvmap.css",
                "~/Content/assets/admin/pages/css/tasks.css",
                "~/Content/assets/global/css/components.css",
                "~/Content/assets/global/css/plugins.css",
                "~/Content/assets/admin/layout/css/layout.css",
                "~/Content/assets/admin/layout/css/themes/darkblue.csss",
                "~/Content/assets/admin/layout/css/custom.css",
                "~/Content/css/style.css"
                ));

            // NakedObjects specific bundling 
            RegisterNakedObjectsBundles(bundles);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterNakedObjectsBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/nakedobjectsajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.address-{version}.js",
                "~/Scripts/jquery.json-{version}.js",
                "~/Scripts/jstorage.js",
                "~/Scripts/NakedObjects-Ajax.js"));

            //DATE FORMATS:   This bundle specifies that the JQuery DatePicker uses the en-GB date format (dd/mm/yy).
            //To use the US format (mm/dd/yy) just remove any references to the bundle. Or to use another locale, specify the appropriate
            //version of jquery.ui.datepicker
            bundles.Add(new ScriptBundle("~/bundles/jquerydatepicker").Include(
                "~/Scripts/ui/i18n/jquery.ui.datepicker-en-GB*"));

            bundles.Add(new StyleBundle("~/Content/nakedobjectscss").Include(
                "~/Content/NakedObjects.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/core.css",
                "~/Content/themes/base/resizable.css",
                "~/Content/themes/base/selectable.css",
                "~/Content/themes/base/accordion.css",
                "~/Content/themes/base/autocomplete.css",
                "~/Content/themes/base/button.css",
                "~/Content/themes/base/dialog.css",
                "~/Content/themes/base/slider.css",
                "~/Content/themes/base/tabs.css",
                "~/Content/themes/base/datepicker.css",
                "~/Content/themes/base/progressbar.css",
                "~/Content/themes/base/theme.css"));
        }
    }
}