using System.Collections.Generic;

namespace Ipfs.Json
{
    public class Link
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public int Size { get; set; }
    }

    public class IpfsObjectLinks
    {
        public string Hash { get; set; }
        public List<Link> Links { get; set; }
    }
}
