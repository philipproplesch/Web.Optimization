using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class BundleElement : ConfigurationElement
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

        [ConfigurationProperty("transform", IsRequired = true)]
        public string Transform
        {
            get
            {
                return (string) this["transform"];
            }
            set
            {
                this["transform"] = value;
            }
        }

        [ConfigurationProperty("files")]
        [ConfigurationCollection(typeof(AddElement), AddItemName = "add")]
        public FilesElementCollection Files
        {
            get
            {
                return (FilesElementCollection) this["files"];
            }
        }

        [ConfigurationProperty("directory")]
        public DirectoryElement Directory
        {
            get
            {
                return (DirectoryElement) this["directory"];
            }
        }
    }
}