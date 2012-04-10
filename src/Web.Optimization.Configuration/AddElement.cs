using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class AddElement : ConfigurationElement
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