using System.Web.Optimization;
using Web.Optimization.Bundles.CoffeeScript;

namespace Web.Optimization.Samples
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var combined = new Bundle(
                "~/scripts/combined",
                new CombinedCoffeeScriptTransform(),
                new JsMinify());
            
            combined.Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/coffeescript/app.coffee");

            bundles.Add(combined);
        }
    }
}