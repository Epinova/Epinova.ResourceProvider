using System;
using System.Reflection;
using Epinova.ResourceProvider.Registration;

namespace Epinova.ResourceProvider
{
    public static class Embedding
    {
        /// <summary>
        /// Register VPP's for all embedded <paramref name="fileTypes"/> in the calling assembly
        /// </summary>
        /// <example>
        /// <para>Register("cshtml")</para>
        /// <para>Register("cshtml", "css")</para>
        /// </example>
        public static void Register(params string[] fileTypes)
        {
            if(fileTypes == null || fileTypes.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(fileTypes), "You need to supply at least one file-type");

            VppRegistration.Register(Assembly.GetCallingAssembly(), fileTypes);
        }
    }
}