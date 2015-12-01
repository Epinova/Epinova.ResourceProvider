using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace Epinova.ResourceProvider
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class Initializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            Register.RegisterVppResources();
        }


        public void Uninitialize(InitializationEngine context)
        {
        }


        public void Preload(string[] parameters)
        {
        }
    }
}