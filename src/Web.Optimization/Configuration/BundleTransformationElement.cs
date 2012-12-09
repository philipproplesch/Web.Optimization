using System;
using System.ComponentModel;
using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleTransformationElement : ConfigurationElement
    {
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("type", IsRequired = true)]
        public Type Type
        {
            get { return this["type"] as Type; }
        }
    }
}