using System;
using System.Reflection;
using Epinova.ResourceProvider.Registration;

namespace Epinova.ResourceProvider
{
    public static class Views
    {
        /// <summary>
        /// Register VPP's for all embedded views in <paramref name="assembly"/>
        /// </summary>
        /// <example>
        /// <para>Register(typeof(MyType).Assembly)</para>
        /// </example>
        public static void Register(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly), "Supplied assembly was null");

            VppRegistration.Register(assembly, new [] { "cshtml" });
        }
    }
}