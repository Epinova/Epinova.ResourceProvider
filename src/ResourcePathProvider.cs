using System;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Epinova.ResourceProvider
{
    internal class ResourcePathProvider : VirtualPathProvider
    {
        private readonly string _virtualPath;
        private readonly string _resourceName;
        private readonly Assembly _assembly;
        private readonly bool _physicalResource;


        public ResourcePathProvider(string virtualPath, string resourceName, Assembly assembly, bool physicalResource)
        {
            _virtualPath = virtualPath;
            _resourceName = resourceName;
            _assembly = assembly;
            _physicalResource = physicalResource;
        }


        public override bool FileExists(string virtualPath)
        {
            return PathMatches(virtualPath)
                   || Previous.FileExists(virtualPath);
        }


        public override VirtualFile GetFile(string virtualPath)
        {
            if (!PathMatches(virtualPath))
                return Previous.GetFile(virtualPath);

            return new ResourceVirtualFile(_virtualPath, _resourceName, _assembly, _physicalResource);
        }


        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (PathMatches(virtualPath))
                return null;

            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            if (PathMatches(virtualPath))
                return Guid.NewGuid().ToString();

            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }


        private bool PathMatches(string virtualPath)
        {
            return virtualPath.Equals(_virtualPath, StringComparison.OrdinalIgnoreCase)
                   || virtualPath.Equals(VirtualPathUtility.ToAbsolute(_virtualPath), StringComparison.OrdinalIgnoreCase);
        }
    }
}