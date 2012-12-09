using System;
using System.ComponentModel;
using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleElement : ConfigurationElement
    {
        [ConfigurationProperty("virtualPath", IsRequired = true, IsKey = true)]
        public string VirtualPath
        {
            get { return this["virtualPath"] as string; }
        }

        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("transform", IsRequired = false)]
        public Type Transform
        {
            get { return this["transform"] as Type; }
        }

        [ConfigurationProperty("content", IsRequired = true)]
        public BundleContentElementCollection Content
        {
            get { return (BundleContentElementCollection) this["content"]; }
        }

        [ConfigurationProperty("transformations", IsRequired = false)]
        public BundleTransformationCollection Transformations
        {
            get { return (BundleTransformationCollection) this["transformations"]; }
        }
    }
}