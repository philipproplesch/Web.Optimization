using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class OptimizationSection : ConfigurationSection
    {
        public const string SectionKeyName = "web.optimization";

        private readonly static OptimizationSection _s_section;

        private static readonly ConfigurationPropertyCollection _s_properties =
            new ConfigurationPropertyCollection();
        
        static OptimizationSection()
        {
            _s_properties.Add(_s_propertyBundles);

            _s_section =
               ConfigurationManager.GetSection(
                   SectionKeyName)
                       as OptimizationSection;
        }

        public static OptimizationSection GetSection()
        {
            return _s_section;
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _s_properties; }
        }

        #region Bundles

        private const string BundlesKeyName = "bundles";

        private static readonly ConfigurationProperty _s_propertyBundles =
            new ConfigurationProperty(
                BundlesKeyName,
                typeof(BundleElementCollection));

        /// <summary>
        /// Gets or sets the routes.
        /// </summary>
        /// <value>The routes.</value>
        [ConfigurationProperty(
            BundlesKeyName)]
        public BundleElementCollection Bundles
        {
            get { return (BundleElementCollection)base[BundlesKeyName]; }
        }

        #endregion
    }
}
