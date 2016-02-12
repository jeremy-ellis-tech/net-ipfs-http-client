using System.Collections.Generic;

namespace Ipfs.Json
{
    public class Link
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public int Size { get; set; }
        public int Type { get; set; }
    }

    public class IpfsNode
    {
        public string Hash { get; set; }
        public IList<Link> Links { get; set; }
    }
}
