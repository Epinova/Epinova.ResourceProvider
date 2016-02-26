using System.Configuration;

namespace Epinova.ResourceProvider.Configuration
{
    public class AddElement : ConfigurationElement
    {
        [ConfigurationProperty("assembly")]
        public virtual string Assembly
        {
            get { return (string)this["assembly"]; }
            internal set { this["assembly"] = value; }
        }

        [ConfigurationProperty("filetypes")]
        public virtual string FileTypes
        {
            get { return (string)this["filetypes"]; }
        }

        [ConfigurationProperty("provideLocalization", DefaultValue = false)]
        public virtual bool ProvideLocalization
        {
            get { return (bool)this["provideLocalization"]; }
        }
    }
}