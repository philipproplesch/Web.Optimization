namespace Web.Optimization.Bundles.YUI
{
    public class YuiScriptBundle : BundleBase<YuiJsMinify>
    {
        public YuiScriptBundle(string virtualPath)
            : base(virtualPath)
        { }

        public YuiScriptBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        { }
    }
}