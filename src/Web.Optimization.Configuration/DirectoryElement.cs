using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class DirectoryElement : ConfigurationElement
    {

        [ConfigurationProperty("virtualPath", IsRequired = true)]
        public string VirtualPath
        {
            get
            {
                return (string) this["virtualPath"];
            }
            set
            {
                this["virtualPath"] = value;
            }
        }

        [ConfigurationProperty("searchPattern", IsRequired = true)]
        public string SearchPattern
        {
            get
            {
                return (string) this["searchPattern"];
            }
            set
            {
                this["searchPattern"] = value;
            }
        }

        [ConfigurationProperty("searchSubdirectories", IsRequired = false, DefaultValue = false)]
        public bool SearchSubdirectories
        {
            get
            {
                return (bool) this["searchSubdirectories"];
            }
            set
            {
                this["searchSubdirectories"] = value;
            }
        }

        [ConfigurationProperty("throwIfNotExist", IsRequired = false, DefaultValue = true)]
        public bool ThrowIfNotExist
        {
            get
            {
                return (bool) this["throwIfNotExist"];
            }
            set
            {
                this["throwIfNotExist"] = value;
            }
        }
    }
}