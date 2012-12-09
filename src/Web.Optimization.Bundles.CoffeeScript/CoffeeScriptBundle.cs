using System.Web.Optimization;

namespace Web.Optimization.Bundles.CoffeeScript
{
    public class CoffeeScriptBundle : BundleBase<CoffeeScriptTransform>
    {
        public CoffeeScriptBundle(string virtualPath)
            : base(virtualPath)
        { }

        public CoffeeScriptBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        { }
    }
}
