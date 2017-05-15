using System;

namespace Epinova.ResourceProvider
{
    [Flags]
    public enum ResourceType
    {
        None = 0,
        All = 1,
        View = 2,
        Xml = 4,
        Handler = 8
    }
}