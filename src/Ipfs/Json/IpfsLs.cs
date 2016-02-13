using System.Collections.Generic;

namespace Ipfs.Json
{
    public class Node
    {
        public string Hash { get; set; }
        public List<Node> Links { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Type { get; set; }
    }

    public class IpfsLs
    {
        public List<Node> Objects { get; set; }
    }

}
