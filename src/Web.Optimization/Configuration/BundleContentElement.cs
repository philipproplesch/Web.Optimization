using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleContentElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string Path
        {
            get { return this["path"] as string; }
        }

        [ConfigurationProperty("searchPattern")]
        public string SearchPattern
        {
            get { return this["searchPattern"] as string; }
        }

        [ConfigurationProperty("searchSubdirectories")]
        public bool SearchSubdirectories
        {
            get { return (bool) this["searchSubdirectories"]; }
        }
    }
}