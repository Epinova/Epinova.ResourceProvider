using System.Configuration;

namespace Epinova.ResourceProvider.Configuration
{
    public class ProvidersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AddElement();
        }
        
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AddElement)element).Assembly;
        }
    }
}
