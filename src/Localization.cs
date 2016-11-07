using System.Reflection;
using Epinova.ResourceProvider.Registration;

namespace Epinova.ResourceProvider
{
    public static class Localization
    {
        /// <summary>
        /// Register xml-files from the calling assembly in 
        /// <see cref="EPiServer.Framework.Localization.LocalizationService"/>
        /// </summary>
        public static void Register()
        {
            LocalizationRegistration.Register(Assembly.GetCallingAssembly());
        }
    }
}