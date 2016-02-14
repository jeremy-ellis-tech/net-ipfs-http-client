using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsBootstrap : IpfsCommand
    {
        public IpfsBootstrap(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Add peers to the bootstrap list
        /// 
        /// Outputs a list of peers that were added (that weren't already in the bootstrap list).
        /// 
        /// SECURITY WARNING:
        /// The bootstrap command manipulates the "bootstrap list", which contains
        /// the addresses of bootstrap nodes.These are the* trusted peers* from
        /// which to learn about other peers in the network.Only edit this list
        /// if you understand the risks of adding or removing nodes from this list.
        /// </summary>
        /// <param name="peers">A peer to add to the bootstrap list (in the format '<multiaddr>/<peerID>')</param>
        /// <param name="default">add default bootstrap nodes</param>
        /// <returns></returns>
        public async Task<HttpContent> Add(IEnumerable<string> peers, bool @default = false)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            if(@default)
            {
                args.Add("default", "true");
            }

            return await ExecuteGetAsync("add", peers, args);
        }

        /// <summary>
        /// Show peers in the bootstrap list
        /// Peers are output in the format '<multiaddr>/<peerID>'.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> List()
        {
            return await ExecuteGetAsync("list", null, null);
        }

        /// <summary>
        /// Removes peers from the bootstrap list
        /// 
        /// Outputs the list of peers that were removed.
        /// 
        /// SECURITY WARNING:
        /// The bootstrap command manipulates the "bootstrap list", which contains
        /// the addresses of bootstrap nodes.These are the* trusted peers* from
        /// which to learn about other peers in the network.Only edit this list
        /// if you understand the risks of adding or removing nodes from this list.
        /// </summary>
        /// <param name="peers">A peer to add to the bootstrap list (in the format '<multiaddr>/<peerID>')</param>
        /// <param name="all">Remove all bootstrap peers.</param>
        /// <returns></returns>
        public async Task<HttpContent> Rm(IEnumerable<string> peers, bool all = false)
        {
            var flags = new Dictionary<string, string>();

            if(all)
            {
                flags.Add("all", "true");
            }

            return await ExecuteGetAsync("rm", peers, flags);
        }
    }
}
