using System.Web.Optimization;

namespace Web.Optimization.Bundles.CoffeeScript
{
    public class CoffeeScriptMinify : JsMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            new CoffeeScriptTransform().Process(context, response);
            base.Process(context, response);
        }
    }
}
