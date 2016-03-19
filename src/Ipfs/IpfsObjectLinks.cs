using System.Collections.Generic;

namespace Ipfs
{
    public class Link
    {
        public string Name { get; set; }
        public MultiHash Hash { get; set; }
        public int Size { get; set; }
    }

    public class IpfsObjectLinks
    {
        public MultiHash Hash { get; set; }
        public List<Link> Links { get; set; }
    }
}
