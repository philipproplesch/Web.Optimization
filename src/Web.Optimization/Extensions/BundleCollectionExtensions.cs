using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Optimization;
using Web.Optimization.Configuration;

namespace Web.Optimization.Extensions
{
    public static class BundleCollectionExtensions
    {
        public static void EnableConfigurationBundles(this BundleCollection bundleCollection)
        {
            var section = OptimizationSection.GetSection();
            if (section == null)
            {
                throw new ConfigurationErrorsException(
                    "Could not find a section with name 'web.optimization'.");
            }

            Func<Type, bool> isBundleTransformType =
                type =>
                    {
                        if (!typeof (IBundleTransform).IsAssignableFrom(type))
                        {
                            throw new ConfigurationErrorsException(
                                string.Format(
                                    "Invalid transform type: '{0}'.",
                                    type.Name));

                        }

                        return true;
                    };

            foreach (BundleElement bundleElement in section.Bundles)
            {
                var transforms = new List<IBundleTransform>();

                // Check "transform" attribute.
                var type = bundleElement.Transform;
                if (type != null && isBundleTransformType(type))
                {
                    transforms.Add(
                        (IBundleTransform)
                        Activator.CreateInstance(type));
                }
                else
                {
                    // Check "transformations" node.
                    foreach (BundleTransformationElement transformation in bundleElement.Transformations)
                    {
                        if (!isBundleTransformType(transformation.Type))
                        {
                            continue;
                        }

                        transforms.Add(
                            (IBundleTransform)
                            Activator.CreateInstance(transformation.Type));
                    }
                }

                var isNewBundle = false;

                // Check if the requested bundle is already configured.
                var bundle = bundleCollection.GetBundleFor(bundleElement.VirtualPath);
                if (bundle == null)
                {
                    isNewBundle = true;
                    bundle = new Bundle(bundleElement.VirtualPath);
                }
                
                if (transforms.Count > 0)
                {
                    // Apply transformations.
                    bundle.Transforms.Clear();
                    transforms.ForEach(transform => bundle.Transforms.Add(transform));
                }
                
                foreach (BundleContentElement contentElement in bundleElement.Content)
                {
                    if (string.IsNullOrEmpty(contentElement.SearchPattern))
                    {
                        Uri uri;
                        // Remote file (e.g. CDN hosted)
                        if (Uri.TryCreate(contentElement.Path, UriKind.Absolute, out uri))
                        {
                            bundle.IncludeRemoteFile(uri);
                        }
                        // Local file
                        else
                        {
                            bundle.Include(contentElement.Path);
                        }
                    }
                    else
                    {
                        bundle.IncludeDirectory(
                            contentElement.Path,
                            contentElement.SearchPattern,
                            contentElement.SearchSubdirectories);
                    }
                }

                if (isNewBundle)
                {
                    bundleCollection.Add(bundle);
                }
            }
        }
    }
}