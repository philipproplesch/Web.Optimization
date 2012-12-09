namespace Web.Optimization.Bundles.Sass
{
    public class SassStyleBundle : BundleBase<SassTransform>
    {
        public SassStyleBundle(string virtualPath)
            : base(virtualPath)
        { }

        public SassStyleBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        { }
    }
}
