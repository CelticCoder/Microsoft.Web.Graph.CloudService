using System.Web;
using System.Web.Optimization;

namespace Microsoft.Web.Graph.WebRole
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

            bundles.Add(new StyleBundle("~/bundles/graph-explorer-styles").Include(
                    "~/Content/graph-explorer-styles/bootstrap.css",
                    "~/Content/graph-explorer-styles/api-explorer.css",
                    "~/Content/graph-explorer-styles/ngProgress.css"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/graph-explorer-scripts").Include(
                "~/Scripts/graph-explorer-scripts/angular.js",
                "~/Scripts/graph-explorer-scripts/angular-animate.js",
                "~/Scripts/graph-explorer-scripts/angular-route.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-helpers.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-jsviewer.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-jseditor.js",
                "~/Scripts/graph-explorer-scripts/constants.js",
                "~/Scripts/graph-explorer-scripts/adal.js",
                "~/Scripts/graph-explorer-scripts/adal-angular.js",
                "~/Scripts/graph-explorer-scripts/ngprogress.min.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-init.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-app.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-svc.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-msgraph.js",
                "~/Scripts/graph-explorer-scripts/api-explorer-ctrl.js",
                "~/Scripts/graph-explorer-scripts/bower-components/angular-bootstrap/ui-bootstrap-tpls.js",
                "~/Scripts/graph-explorer-scripts/bower-components/angular-bootstrap/ui-bootstrap-tpls.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/graph-explorer-scripts/aceCDN",
                "https://cdnjs.cloudflare.com/ajax/libs/ace/1.2.0/ace.js")
                .Include("~/Scripts/graph-explorer-scripts/ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/graph-explorer-scripts/mode-js-cdn",
                "https://cdnjs.cloudflare.com/ajax/libs/ace/1.2.0/mode-javascript.js")
                .Include("~/Scripts/graph-explorer-scripts/mode-javascript.js"));

        }
    }
}
