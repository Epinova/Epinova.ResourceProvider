using System.IO;
using System.Reflection;
using System.Web.Hosting;

namespace Epinova.ResourceProvider.Vpp
{
    public class ResourceVirtualFile : VirtualFile
    {
        private readonly string _resourceName;
        private readonly Assembly _assembly;
        private readonly bool _physicalResource;


        public ResourceVirtualFile(string virtualPath, string resourceName, Assembly assembly, bool physicalResource)
            : base(virtualPath)
        {
            _resourceName = resourceName;
            _assembly = assembly;
            _physicalResource = physicalResource;
        }


        public override Stream Open()
        {
            if (_physicalResource)
                return File.OpenRead(_resourceName);

            return _assembly.GetManifestResourceStream(_resourceName);
        }
    }
}
