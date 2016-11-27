using System;
using System.Reflection;
using Epinova.ResourceProvider.Registration;

namespace Epinova.ResourceProvider
{
    [Obsolete("Use Epinova.ResourceProvider.Views.Register()", true)]
    public static class Embedding
    {
        /// <summary>
        /// Register VPP's for all embedded <paramref name="fileTypes"/> in <paramref name="assembly"/>
        /// </summary>
        /// <example>
        /// <para>Register(Assembly.GetAssembly(typeof(MyClass)), "cshtml")</para>
        /// <para>Register(Assembly.GetAssembly(typeof(MyClass)), "cshtml", "css", ...)</para>
        /// </example>
        public static void Register(Assembly assembly, params string[] fileTypes)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly), "Supplied assembly was null");

            if (fileTypes == null || fileTypes.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(fileTypes), "You need to supply at least one file-type");

            VppRegistration.Register(assembly, fileTypes);
        }
    }
}