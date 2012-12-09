using System.Configuration;

namespace Web.Optimization.Configuration
{
    [ConfigurationCollection(
        typeof(BundleTransformationElement), 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class BundleTransformationCollection : ConfigurationElementCollection
    {
        public BundleTransformationElement this[int index]
        {
            get
            {
                return BaseGet(index) as BundleTransformationElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new BundleTransformationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BundleTransformationElement) element).Type;
        }
    }
}