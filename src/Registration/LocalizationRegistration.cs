using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using EPiServer.Framework.Localization;
using EPiServer.Framework.Localization.XmlResources;
using EPiServer.Logging;
using EPiServer.ServiceLocation;

namespace Epinova.ResourceProvider.Registration
{
    internal static class LocalizationRegistration
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(LocalizationRegistration));

        internal static void Register(Assembly assembly)
        {
            try
            {
                Logger.Debug(String.Format("Looking for resources in assembly '{0}'", assembly.FullName));

                var assemblyName = assembly.GetName().Name;

                Logger.Log(Level.Debug, "{0}: Looking for XML-resources...", assemblyName);

                string[] xmlFiles = assembly
                    .GetManifestResourceNames()
                    .Where(r => r.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();

                if (!xmlFiles.Any())
                {
                    Logger.Log(Level.Debug, "{0}: No XML-resources found.", assemblyName);
                    return;
                }

                ProviderBasedLocalizationService providerService =
                    ServiceLocator.Current.GetInstance<LocalizationService>() as ProviderBasedLocalizationService;

                if (providerService == null)
                {
                    Logger.Log(Level.Warning, "{0}: Could not load ProviderBasedLocalizationService.", assemblyName);
                    return;
                }

                var provider = new XmlLocalizationProvider();
                provider.Initialize("XML-provider for " + assemblyName, new NameValueCollection());

                foreach (string xmlResource in xmlFiles)
                {
                    Logger.Log(Level.Debug, "{0}: Loading resource '{1}'", assemblyName, xmlResource);
                    provider.Load(assembly.GetManifestResourceStream(xmlResource));
                }

                providerService.AddProvider(provider);
            }
            catch (Exception ex)
            {
                Logger.Log(Level.Error, "Failed to register localization", ex);
            }
        }
    }
}