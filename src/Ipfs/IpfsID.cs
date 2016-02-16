using System.Collections.Generic;

namespace Ipfs
{
    public class IpfsID
    {
        public MultiHash ID { get; set; }
        public string PublicKey { get; set; }
        public List<MultiAddress> Addresses { get; set; }
        public string AgentVersion { get; set; }
        public string ProtocolVersion { get; set; }
    }
}
