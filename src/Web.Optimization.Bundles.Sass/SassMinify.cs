using System.Web.Optimization;

namespace Web.Optimization.Bundles.Sass
{
    public class SassMinify : CssMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            new SassTransform().Process(context, response);
            base.Process(context, response);
        }
    }
}
