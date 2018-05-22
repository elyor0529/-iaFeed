
namespace KiaFeed.Parser
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/jquery-*",
                         "~/Scripts/modernizr-*",
                         "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
