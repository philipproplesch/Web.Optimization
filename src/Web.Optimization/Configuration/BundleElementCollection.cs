using System.Configuration;

namespace Web.Optimization.Configuration
{
    [ConfigurationCollection(typeof(BundleElement), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "bundle")]
    public class BundleElementCollection : ConfigurationElementCollection
    {
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
