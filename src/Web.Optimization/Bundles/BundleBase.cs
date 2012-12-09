using System;
using System.Web.Optimization;

namespace Web.Optimization.Bundles
{
    public class BundleBase<TTransform> : Bundle where TTransform : IBundleTransform
    {
        public BundleBase(string virtualPath)
            : base(
                virtualPath,
                new[]
                    {
                        (IBundleTransform) 
                        Activator.CreateInstance(typeof(TTransform))
                    })
        { }

        public BundleBase(string virtualPath, string cdnPath)
            : base(
                virtualPath,
                cdnPath,
                new[]
                    {
                        (IBundleTransform) 
                        Activator.CreateInstance(typeof(TTransform))
                    })
        { }
    }
}