using System.Collections.Generic;

namespace Ipfs
{
    public class IpfsPeer
    {
        public List<MultiAddress> Addresses { get; set; }
        public MultiHash PeerId { get; set; }
    }
}
