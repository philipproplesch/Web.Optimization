namespace Web.Optimization.Bundles.Less
{
    public class LessStyleBundle : BundleBase<LessTransform>
    {
        public LessStyleBundle(string virtualPath)
            : base(virtualPath)
        { }

        public LessStyleBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        { }
    }
}
