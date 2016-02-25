using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Epinova.ResourceProvider.Configuration;
using Epinova.ResourceProvider.Registration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Logging;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace Epinova.ResourceProvider
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class Initializer : IInitializableModule
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(Initializer));

        public void Initialize(InitializationEngine context)
        {
            foreach (AddElement include in ModuleSection.Configuration.Providers.Cast<AddElement>())
            {
                Assembly assembly = Assembly.Load(include.Assembly);
                var assemblyName = assembly.GetName().Name;

                Logger.Debug("Looking for resources in: " + assemblyName);

                if (String.IsNullOrWhiteSpace(include.FileTypes) && !include.ProvideLocalization)
                    throw new ConfigurationErrorsException(assemblyName + ": You must provide a value either for 'fileTypes' or 'provideLocalization'.");

                if (include.ProvideLocalization)
                    Localization.RegisterLocalization(assembly);
    
                if(!String.IsNullOrWhiteSpace(include.FileTypes))
                    Registration.Vpp.RegisterResources(assembly, include);
            }
        }


        public void Uninitialize(InitializationEngine context)
        {
        }


        public void Preload(string[] parameters)
        {
        }
    }
}