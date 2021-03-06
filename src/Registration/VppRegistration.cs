﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Epinova.ResourceProvider.Vpp;
using EPiServer.Logging;

namespace Epinova.ResourceProvider.Registration
{
    internal static class VppRegistration
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof (VppRegistration));
        internal static Action<VirtualPathProvider> VppRegistrator = HostingEnvironment.RegisterVirtualPathProvider;
        
        internal static void Register(Assembly assembly, string[] fileTypes)
        {
            Logger.Debug(String.Format("Registering assembly: {0}", assembly.FullName));

            if (HttpContext.Current == null)
            {
                Logger.Warning("No HttpContext found, aborting.");
                return;
            }

            string[] resourceNames = GetAssemblyManifestResourceNames(assembly, fileTypes);

            foreach (string resourceName in resourceNames)
            {
                RegisterResourcePath(resourceName, assembly);
            }
        }


        private static string[] GetAssemblyManifestResourceNames(Assembly assembly, string[] fileTypes)
        {
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

            VppRegistrator(new ResourcePathProvider(resourcePath, resourceName, assembly, false));

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