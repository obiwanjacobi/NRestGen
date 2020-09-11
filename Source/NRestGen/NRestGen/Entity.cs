using System.Collections.Generic;

namespace NRestGen
{
    public abstract class Entity
    {
        public IEnumerable<Link> Links { get; set; }
    }
}
