using System.Web.Optimization;

namespace Web.Optimization.Bundles.CoffeeScript
{
    public class CombinedCoffeeScriptMinify : JsMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            new CombinedCoffeeScriptTransform().Process(context, response);
            base.Process(context, response);
        }
    }
}
