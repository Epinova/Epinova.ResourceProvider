using System.Configuration;

namespace Epinova.ResourceProvider.Configuration
{
    public class ModuleSection : ConfigurationSection
    {
        private static ModuleSection configuration;

        public static ModuleSection Configuration
        {
            get { return configuration ?? (configuration = ConfigurationManager.GetSection("epinova.resourceprovider") as ModuleSection ?? new ModuleSection()); }
        }

        [ConfigurationProperty("providers", IsRequired = false)]
        public virtual ProvidersCollection Providers { get { return (ProvidersCollection)this["providers"]; } }
    }
}
