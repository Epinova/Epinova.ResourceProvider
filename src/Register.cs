using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Epinova.ResourceProvider.Configuration;
using EPiServer.Logging;

namespace Epinova.ResourceProvider
{
    internal static class Register
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof (Register));

        internal static void RegisterVppResources()
        {
            if (HttpContext.Current == null)
                return;

            foreach (AddElement include in ModuleSection.Configuration.Providers.Cast<AddElement>())
            {
                Assembly assembly = Assembly.Load(include.Assembly);

                if (String.IsNullOrWhiteSpace(include.FileTypes))
                    throw new ConfigurationErrorsException("You must provide a value for 'fileTypes'.");

                Logger.Debug("Looking for resources in: " + assembly.GetName().Name);

                string[] resourceNames = GetResourceNames(include, assembly);

                foreach (string resourceName in resourceNames)
                {
                    RegisterResourcePath(resourceName, assembly);
                }
            }
        }


        private static void Log(string message)
        {
        }


        private static string[] GetResourceNames(AddElement include, Assembly assembly)
        {
            string[] fileTypes = include.FileTypes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    type =>
                    {
                        string fileType = type.Trim();

                        if (fileType.StartsWith("*"))
                            fileType = fileType.Substring(1);
                        if (fileType.StartsWith("."))
                            fileType = fileType.Substring(1);

                        return fileType;
                    })
                .ToArray();

            return assembly.GetManifestResourceNames()
                .Where(
                    name =>
                    {
                        string extension = Path.GetExtension(name);
                        return extension != null && fileTypes.Contains(extension.Substring(1));
                    })
                .ToArray();
        }


        private static void RegisterResourcePath(string resourceName, Assembly assembly)
        {
            Logger.Debug("Found resource: " + resourceName);

            string resourcePath = GetResourcePath(resourceName, assembly);

            HostingEnvironment.RegisterVirtualPathProvider(new ResourcePathProvider(resourcePath, resourceName, assembly, false));

            Logger.Debug("Registered as : " + resourcePath);
        }

        private static string GetResourcePath(string resourceName, Assembly assembly)
        {
            string assemblyName = assembly.GetName().Name;
            string fileName = Path.GetFileNameWithoutExtension(resourceName) ?? string.Empty;
            string fileExt = Path.GetExtension(resourceName) ?? string.Empty;

            if (fileName.StartsWith(assemblyName))
                fileName = fileName.Substring(assemblyName.Length);

            return string.Concat("~", fileName.Replace(".", "/"), fileExt);
        }
    }
}