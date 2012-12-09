using System.Configuration;

namespace Web.Optimization.Configuration
{
    [ConfigurationCollection(
        typeof(BundleContentElement), 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class BundleContentElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BundleContentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BundleContentElement) element).Path;
        }
    }
}