using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class OptimizationSection : ConfigurationSection
    {
        [ConfigurationPropertyAttribute("bundles")]
        [ConfigurationCollectionAttribute(typeof (BundleElement), AddItemName = "bundle")]
        public BundlesElementCollection Bundles
        {
            get
            {
                return (BundlesElementCollection) this["bundles"];
            }
        }
    }
}