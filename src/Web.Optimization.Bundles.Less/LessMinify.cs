using System.Web.Optimization;

namespace Web.Optimization.Bundles.Less
{
    public class LessMinify : CssMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            new LessTransform().Process(context, response);
            base.Process(context, response);
        }
    }
}