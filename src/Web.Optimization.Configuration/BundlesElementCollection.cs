using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundlesElementCollection : ConfigurationElementCollection
    {

        public BundleElement this[int i]
        {
            get
            {
                return (BundleElement) BaseGet(i);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new BundleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BundleElement) element).VirtualPath;
        }
    }
}