using System;
using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleElementCollection : ConfigurationElementCollection
    {
        public new BundleElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (BundleElement)BaseGet(name);
            }
        }

        public BundleElement this[int index]
        {
            get { return (BundleElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (var idx = 0; idx < Count; idx++)
            {
                if (this[idx].VirtualPath.Equals(
                    name,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return idx;
                }
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "bundle"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new BundleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var typedElement = element as BundleElement;

            return typedElement == null
                       ? string.Empty
                       : typedElement.VirtualPath;
        }
    }
}
