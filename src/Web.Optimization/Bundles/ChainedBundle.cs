using System.Web.Optimization;

namespace Web.Optimization.Bundles
{
    internal class ChainedBundle : IBundleTransform
    {
        private readonly IBundleTransform _instance;
        private readonly IBundleTransform _then;

        public ChainedBundle(
            IBundleTransform instance, IBundleTransform then)
        {
            _instance = instance;
            _then = then;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            _instance.Process(context, response);
            _then.Process(context, response);
        }
    }
}