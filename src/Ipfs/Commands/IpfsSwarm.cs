using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsSwarm : IpfsCommand
    {
        internal IpfsSwarm()
        {
        }

        internal IpfsSwarm(string address) : base(address)
        {
        }

        internal IpfsSwarm(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if (_baseUri == null)
                {
                    UriBuilder builder = new UriBuilder(_address);
                    builder.Path += "/api/v0/swarm/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// List known addresses. Useful to debug.
        /// 
        /// ipfs swarm addrs lists all addresses this node is aware of.
        /// </summary>
        /// <returns>all addresses this node is aware of</returns>
        public async Task<byte[]> Addrs()
        {
            return await ExecuteAsync("addrs");
        }

        /// <summary>
        /// Open connection to a given address
        /// 
        /// 'ipfs swarm connect' opens a connection to a peer address. The address format
        /// is an ipfs multiaddr:
        /// 
        /// ipfs swarm connect /ip4/104.131.131.82/tcp/4001/ipfs/QmaCpDMGvV2BGHeYERUEnRQAwe3N8SzbUtfsmvsqQLuvuJ
        /// </summary>
        /// <param name="address">address of peer to connect to</param>
        /// <returns></returns>
        public async Task<byte[]> Connect(string address)
        {
            return await ExecuteAsync("connect", ToEnumerable(address));
        }

        public async Task<byte[]> Disconnect(string address)
        {
            return await ExecuteAsync("connect", ToEnumerable(address));
        }

        /// <summary>
        /// List peers with open connections
        /// ipfs swarm peers lists the set of peers this node is connected to.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> Peers()
        {
            return await ExecuteAsync("peers");
        }
    }
}
