using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleContentElement : ConfigurationElement
    {
        private static readonly ConfigurationPropertyCollection s_properties =
            new ConfigurationPropertyCollection();

        static BundleContentElement()
        {
            s_properties.Add(_s_propertyVirtualPath);
            s_properties.Add(_s_propertySearchPattern);
            s_properties.Add(_s_propertySearchSubdirectories);
            s_properties.Add(_s_propertyThrowIfNotExist);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }

        #region VirtualPath

        private const string VirtualPathKeyName = "virtualPath";

        private static readonly ConfigurationProperty _s_propertyVirtualPath =
            new ConfigurationProperty(
                VirtualPathKeyName,
                typeof(string),
                string.Empty,
                ConfigurationPropertyOptions.IsRequired
                | ConfigurationPropertyOptions.IsKey);

        [ConfigurationProperty(
            VirtualPathKeyName,
            IsRequired = true,
            IsKey = true)]
        public string VirtualPath
        {
            get { return base[VirtualPathKeyName] as string; }
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

        #region ThrowIfNotExist

        private const string ThrowIfNotExistKeyName = "throwIfNotExist";

        private static readonly ConfigurationProperty _s_propertyThrowIfNotExist =
            new ConfigurationProperty(
                ThrowIfNotExistKeyName,
                typeof(bool),
                true,
                ConfigurationPropertyOptions.None);

        [ConfigurationProperty(ThrowIfNotExistKeyName)]
        public bool ThrowIfNotExist
        {
            get { return (bool)base[ThrowIfNotExistKeyName]; }
        }

        #endregion
    }
}