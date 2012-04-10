using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class FilesElementCollection : ConfigurationElementCollection
    {

        public AddElement this[int i]
        {
            get
            {
                return (AddElement) BaseGet(i);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AddElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AddElement) element).VirtualPath;
        }
    }
}