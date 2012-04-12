using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleElement : ConfigurationElement
    {
        private static readonly ConfigurationPropertyCollection _s_properties =
            new ConfigurationPropertyCollection();

        static BundleElement()
        {
            _s_properties.Add(_s_propertyVirtualPath);
            _s_properties.Add(_s_propertyTransform);
            _s_properties.Add(_s_propertyContent);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _s_properties; }
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

        #region Transform

        private const string TransformKeyName = "transform";

        private static readonly ConfigurationProperty _s_propertyTransform =
            new ConfigurationProperty(
                TransformKeyName,
                typeof(string),
                string.Empty,
                ConfigurationPropertyOptions.None);

        // [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty(TransformKeyName, IsRequired = false)]
        public string Transform
        {
            get { return (string) base[TransformKeyName]; }
        }

        #endregion

        #region Content

        private const string ContentKeyName = "content";

        private static readonly ConfigurationProperty _s_propertyContent =
            new ConfigurationProperty(
                ContentKeyName,
                typeof(BundleContentElementCollection),
                null,
                ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty(ContentKeyName, IsDefaultCollection = true)]
        public BundleContentElementCollection Content
        {
            get { return (BundleContentElementCollection)base[ContentKeyName]; }
        }

        #endregion
    }
}