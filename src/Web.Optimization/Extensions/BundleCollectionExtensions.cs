using System;
using System.Configuration;
using System.Web.Optimization;
using Web.Optimization.Configuration;

namespace Web.Optimization.Extensions
{
    public static class BundleCollectionExtensions
    {
        public static void RegisterConfigurationBundles(
            this BundleCollection bundles)
        {
            var section = OptimizationSection.GetSection();
            if (section == null)
            {
                throw new ConfigurationErrorsException(
                    "Could not find a section with name 'web.optimization'.");
            }

            foreach (BundleElement bundleElement in section.Bundles)
            {
                var type = bundleElement.Transform;
                if (type == null || !typeof(IBundleTransform).IsAssignableFrom(type))
                    continue;

                var transform = Activator.CreateInstance(type);
                
                var isNewBundle = false;

                // Check if there is a bundle with the same virtual path.
                var bundle = bundles.GetBundleFor(bundleElement.VirtualPath);
                if (bundle == null)
                {
                    isNewBundle = true;

                    bundle = new Bundle(
                        bundleElement.VirtualPath,
                        (IBundleTransform)transform);
                }
                
                foreach (BundleContentElement contentElement in bundleElement.Content)
                {
                    if (string.IsNullOrEmpty(contentElement.SearchPattern))
                    {
                        bundle.AddFile(
                            contentElement.VirtualPath,
                            contentElement.ThrowIfNotExist);
                    }
                    else
                    {
                        bundle.AddDirectory(
                            contentElement.VirtualPath,
                            contentElement.SearchPattern,
                            contentElement.SearchSubdirectories,
                            contentElement.ThrowIfNotExist);
                    }
                }

                if (isNewBundle)
                    bundles.Add(bundle);
            }
        }
    }
}