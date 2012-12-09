using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class OptimizationSection : ConfigurationSection
    {
        public const string SectionKeyName = "web.optimization";

        public static OptimizationSection GetSection()
        {
            return ConfigurationManager.GetSection(SectionKeyName) as OptimizationSection;
        }

        [ConfigurationProperty("bundles")]
        public BundleElementCollection Bundles
        {
            get
            {
                return (BundleElementCollection)this["bundles"];
            }
        }
    }
}
