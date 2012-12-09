namespace Web.Optimization.Bundles.CoffeeScript
{
    public class CombinedCoffeeScriptBundle : BundleBase<CombinedCoffeeScriptTransform>
    {
        public CombinedCoffeeScriptBundle(string virtualPath)
            : base(virtualPath)
        { }

        public CombinedCoffeeScriptBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        { }
    }
}