using System;
using System.Reflection;
using Epinova.ResourceProvider.Registration;

namespace Epinova.ResourceProvider
{
    public static class Localization
    {
        /// <summary>
        /// Register xml-files embedded in <paramref name="assembly"/> in 
        /// <see cref="EPiServer.Framework.Localization.LocalizationService"/>
        /// </summary>
        public static void Register(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly), "Supplied assembly was null");

            LocalizationRegistration.Register(assembly);
        }
    }
}