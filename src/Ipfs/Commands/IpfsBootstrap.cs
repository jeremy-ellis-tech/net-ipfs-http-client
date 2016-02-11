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
        internal IpfsBootstrap()
        {
        }

        internal IpfsBootstrap(string address) : base(address)
        {
        }

        internal IpfsBootstrap(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if (_baseUri == null)
                {
                    UriBuilder uriBuilder = new UriBuilder(_address);
                    uriBuilder.Path += "api/v0/bootstrap/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

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
        public async Task<byte[]> Add(IEnumerable<string> peers, bool @default = false)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            if(@default)
            {
                args.Add("default", "true");
            }

            return await ExecuteAsync("add", peers, args);
        }

        /// <summary>
        /// Show peers in the bootstrap list
        /// Peers are output in the format '<multiaddr>/<peerID>'.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> List()
        {
            return await ExecuteAsync("list");
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
        public async Task<byte[]> Rm(IEnumerable<string> peers, bool all = false)
        {
            var flags = new Dictionary<string, string>();

            if(all)
            {
                flags.Add("all", "true");
            }

            return await ExecuteAsync("rm", peers, flags);
        }
    }
}
