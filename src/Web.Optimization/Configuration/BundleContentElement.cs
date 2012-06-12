using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleContentElement : ConfigurationElement
    {
        private static readonly ConfigurationPropertyCollection s_properties =
            new ConfigurationPropertyCollection();

        static BundleContentElement()
        {
            s_properties.Add(_s_propertyPath);
            s_properties.Add(_s_propertySearchPattern);
            s_properties.Add(_s_propertySearchSubdirectories);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }

        #region Path

        private const string PathKeyName = "path";

        private static readonly ConfigurationProperty _s_propertyPath =
            new ConfigurationProperty(
                PathKeyName,
                typeof (string),
                string.Empty,
                ConfigurationPropertyOptions.IsRequired |
                ConfigurationPropertyOptions.IsKey);

        [ConfigurationProperty(
            PathKeyName,
            IsRequired = true,
            IsKey = true)]
        public string Path
        {
            get { return base[PathKeyName] as string; }
        }

        #endregion

        #region SearchPattern

        private const string SearchPatternKeyName = "searchPattern";

        private static readonly ConfigurationProperty _s_propertySearchPattern =
            new ConfigurationProperty(
                SearchPatternKeyName,
                typeof(string),
                string.Empty,
                ConfigurationPropertyOptions.None);

        [ConfigurationProperty(SearchPatternKeyName)]
        public string SearchPattern
        {
            get { return base[SearchPatternKeyName] as string; }
        }

        #endregion

        #region SearchSubdirectories

        private const string SearchSubdirectoriesKeyName = "searchSubdirectories";

        private static readonly ConfigurationProperty _s_propertySearchSubdirectories =
            new ConfigurationProperty(
                SearchSubdirectoriesKeyName,
                typeof(bool),
                false,
                ConfigurationPropertyOptions.None);

        [ConfigurationProperty(SearchSubdirectoriesKeyName)]
        public bool SearchSubdirectories
        {
            get { return (bool) base[SearchSubdirectoriesKeyName]; }
        }

        #endregion
    }
}