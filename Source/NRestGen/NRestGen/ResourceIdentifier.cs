using System;

namespace NRestGen
{
    public class ResourceIdentifier
    {
        public ResourceIdentifier(Type resourceType, int resourceId = 0)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        public Type ResourceType { get; private set; }
        public int ResourceId { get; private set; }
    }
}
