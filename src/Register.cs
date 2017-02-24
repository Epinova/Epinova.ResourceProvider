using System;
using System.Reflection;
using Epinova.ResourceProvider.Registration;

namespace Epinova.ResourceProvider
{
    public static class Register
    {
        /// <summary>
        /// Register embedded resources in the supplied assembly.
        /// </summary>
        /// <typeparam name="T">A type in the assembly you want to scan</typeparam>
        /// <param name="type">
        /// The type(s) of resource to include. Defaults to <see cref="ResourceType.All"/>
        /// <para>Possible types include: </para>
        /// <para><see cref="ResourceType.View"/> - .cshtml files</para>
        /// <para><see cref="ResourceType.Xml"/> - .xml files, for use in localization</para>
        /// <para><see cref="ResourceType.Handler"/> - .ashx, .ascx, and .aspx files</para>
        /// <para><see cref="ResourceType.All"/> - All of the above</para>
        /// <para><see cref="ResourceType.None"/> - Nothing</para>
        /// </param>
        /// <example>
        /// <para>Register.Assembly&lt;MyType&gt;()</para>
        /// <para>Register.Assembly&lt;MyType&gt;(ResourceType.Xml)</para>
        /// <para>Register.Assembly&lt;MyType&gt;(ResourceType.View | ResourceType.Handler)</para>
        /// </example>
        public static void Assembly<T>(ResourceType type = ResourceType.All)
        {
            Assembly(typeof(T).Assembly, type);
        }

        internal static void Assembly(Assembly assembly, ResourceType type)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly), "Supplied assembly was null");

            if (type == ResourceType.None)
                return;

            if (type.HasFlag(ResourceType.Xml))
                LocalizationRegistration.Register(assembly);

            if (type.HasFlag(ResourceType.View))
                VppRegistration.Register(assembly, new[] { "cshtml" });

            if (type.HasFlag(ResourceType.Handler))
                VppRegistration.Register(assembly, new[] { "aspx", "ashx", "ascx" });
        }
    }
}