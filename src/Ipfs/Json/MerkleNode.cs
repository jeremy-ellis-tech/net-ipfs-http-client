using System.Collections.Generic;

namespace Ipfs.Json
{
    public class MerkleNode
    {
        public string Hash { get; set; }
        public List<MerkleNode> Links { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Type { get; set; }
    }
}
