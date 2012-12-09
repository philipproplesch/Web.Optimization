namespace Web.Optimization.Bundles.YUI
{
    public class YuiStyleBundle : BundleBase<YuiCssMinify>
    {
        public YuiStyleBundle(string virtualPath)
            : base(virtualPath)
        { }

        public YuiStyleBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        { }
    }
}
