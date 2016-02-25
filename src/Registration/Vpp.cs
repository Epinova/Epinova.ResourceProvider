using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Epinova.ResourceProvider.Configuration;
using Epinova.ResourceProvider.Vpp;
using EPiServer.Logging;

namespace Epinova.ResourceProvider.Registration
{
    internal static class Vpp
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof (Vpp));

        internal static void RegisterResources(Assembly assembly, AddElement include)
        {
            if (HttpContext.Current == null)
            {
                Logger.Warning("No HttpContext found, aborting.");
                return;
            }

            string[] resourceNames = GetResourceNames(include.FileTypes, assembly);

            foreach (string resourceName in resourceNames)
            {
                RegisterResourcePath(resourceName, assembly);
            }
        }


        private static string[] GetResourceNames(string fileTypesRaw, Assembly assembly)
        {
            string[] fileTypes = fileTypesRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
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