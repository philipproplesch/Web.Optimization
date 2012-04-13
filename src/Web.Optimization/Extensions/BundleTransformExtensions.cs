using System.Web.Optimization;
using Web.Optimization.Bundles;

namespace Web.Optimization.Extensions
{
    public static class BundleTransformExtensions
    {
        // Stolen from @howard_dierking ;) => https://gist.github.com/1998379

        public static IBundleTransform Then(
            this IBundleTransform instance, IBundleTransform then)
        {
            return new ChainedBundle(instance, then);
        }
    }
}