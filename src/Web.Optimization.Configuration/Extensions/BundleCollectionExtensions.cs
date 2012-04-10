using System;
using System.Configuration;
using System.Web.Optimization;

namespace Web.Optimization.Configuration.Extensions
{
    public static class BundleCollectionExtensions
    {
        public static void RegisterFromConfiguration(
            this BundleCollection bundles)
        {
            var section = OptimizationSection.GetSection();
            if (section == null)
            {
                throw new ConfigurationErrorsException(
                    "Could not find a section with name 'optimization'");
            }

            foreach (BundleElement bundleElement in section.Bundles)
            {
                var type = bundleElement.Transform;
                if (type == null || !typeof(IBundleTransform).IsAssignableFrom(type))
                    continue;

                var transform = Activator.CreateInstance(type);

                var bundle =
                    new Bundle(
                        bundleElement.VirtualPath,
                        (IBundleTransform)transform);

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

                bundles.Add(bundle);
            }
        }
    }
}