using System;
using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleContentElementCollection 
        : ConfigurationElementCollection
    {
        public new BundleContentElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (BundleContentElement)BaseGet(name);
            }
        }

        public BundleContentElement this[int index]
        {
            get { return (BundleContentElement)BaseGet(index); }
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
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        protected override string ElementName
        {
            get { return string.Empty; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new BundleContentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var typedElement = element as BundleContentElement;

            return typedElement == null
                       ? string.Empty
                       : typedElement.VirtualPath;
        }
    }
}