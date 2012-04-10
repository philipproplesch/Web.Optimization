using System;
using System.Configuration;
using System.Web.Optimization;

namespace Web.Optimization.Configuration.Extensions
{
    public static class BundleCollectionExtensions
    {
         public static void RegisterFromConfiguration(
             this BundleCollection collection)
         {
             var config = Optimization.Config;
             if (config == null)
             {
                 throw new ConfigurationErrorsException(
                     "Could not find a section with name 'optimization'");
             }

             foreach (BundleElement element in config.Bundles)
             {
                 var type = Type.GetType(element.Transform);
                 if (type == null || !typeof(IBundleTransform).IsAssignableFrom(type))
                     continue;
                 
                 var transform = Activator.CreateInstance(type);

                 var bundle =
                     new Bundle(
                         element.VirtualPath,
                         (IBundleTransform) transform);

                 foreach (AddElement file in element.Files)
                 {
                     bundle.AddFile(file.VirtualPath, file.ThrowIfNotExist);
                 }

                 if (!string.IsNullOrEmpty(element.Directory.VirtualPath))
                 {
                     var directory = element.Directory;

                     bundle.AddDirectory(
                         directory.VirtualPath,
                         directory.SearchPattern,
                         directory.SearchSubdirectories,
                         directory.ThrowIfNotExist);
                 }

                 collection.Add(bundle);
             }
         }
    }
}