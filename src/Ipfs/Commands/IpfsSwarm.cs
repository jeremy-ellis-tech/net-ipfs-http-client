using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsSwarm : IpfsCommand
    {
        public IpfsSwarm(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// List known addresses. Useful to debug.
        /// 
        /// ipfs swarm addrs lists all addresses this node is aware of.
        /// </summary>
        /// <returns>all addresses this node is aware of</returns>
        public async Task<HttpContent> Addrs()
        {
            return await ExecuteGetAsync("addrs", null, null);
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
        public async Task<HttpContent> Connect(string address)
        {
            return await ExecuteGetAsync("connect", ToEnumerable(address), null);
        }

        /// <summary>
        /// Close connection to a given address
        /// 
        /// 'ipfs swarm disconnect' closes a connection to a peer address. The address format
        /// is an ipfs multiaddr:
        /// 
        /// ipfs swarm disconnect /ip4/104.131.131.82/tcp/4001/ipfs/QmaCpDMGvV2BGHeYERUEnRQAwe3N8SzbUtfsmvsqQLuvuJ
        /// </summary>
        /// <param name="multiAddress">address of peer to disconnect from</param>
        /// <returns>'disconnect <address> successs' on success</returns>
        public async Task<IpfsPeerConnectionStatus> Disconnect(MultiAddress multiAddress)
        {
            var ret = await Disconnect(ToEnumerable(multiAddress));
            return ret.First();
        }

        /// <summary>
        /// Close connection to a given address
        /// 
        /// 'ipfs swarm disconnect' closes a connection to a peer address. The address format
        /// is an ipfs multiaddr:
        /// 
        /// ipfs swarm disconnect /ip4/104.131.131.82/tcp/4001/ipfs/QmaCpDMGvV2BGHeYERUEnRQAwe3N8SzbUtfsmvsqQLuvuJ
        /// </summary>
        /// <param name="multiAddress">address of peer to disconnect from</param>
        /// <returns>'disconnect <address> successs' on success</returns>
        public async Task<IpfsPeerConnectionStatus> Disconnect(string multiAddress)
        {
            var ret = await Disconnect(ToEnumerable(multiAddress));
            return ret.First();
        }

        /// <summary>
        /// Close connection to a given address
        /// 
        /// 'ipfs swarm disconnect' closes a connection to a peer address. The address format
        /// is an ipfs multiaddr:
        /// 
        /// ipfs swarm disconnect /ip4/104.131.131.82/tcp/4001/ipfs/QmaCpDMGvV2BGHeYERUEnRQAwe3N8SzbUtfsmvsqQLuvuJ
        /// </summary>
        /// <param name="multiAddresses">addresses of peers to disconnect from</param>
        /// <returns>'disconnect <address> successs' on success</returns>
        public async Task<ICollection<IpfsPeerConnectionStatus>> Disconnect(IEnumerable<MultiAddress> multiAddresses)
        {
            return await Disconnect(multiAddresses.Select(x=>x.ToString()));
        }

        /// <summary>
        /// Close connection to a given address
        /// 
        /// 'ipfs swarm disconnect' closes a connection to a peer address. The address format
        /// is an ipfs multiaddr:
        /// 
        /// ipfs swarm disconnect /ip4/104.131.131.82/tcp/4001/ipfs/QmaCpDMGvV2BGHeYERUEnRQAwe3N8SzbUtfsmvsqQLuvuJ
        /// </summary>
        /// <param name="multiAddress">address of peer to disconnect from</param>
        /// <returns>'disconnect <address> successs' on success</returns>
        public async Task<ICollection<IpfsPeerConnectionStatus>> Disconnect(IEnumerable<string> multiAddresses)
        {
            HttpContent content = await ExecuteGetAsync("disconnect", multiAddresses, null);

            string json = await content.ReadAsStringAsync();

            var peerConnectionStatuses = JsonConvert.DeserializeObject<IDictionary<string, IList<string>>>(json);

            return peerConnectionStatuses
                .SelectMany(x => x.Value)
                .Select(x =>
                {
                    string[] components = x.Split(' ');
                    return new IpfsPeerConnectionStatus(components[0], new MultiHash(components[1]), String.Equals("SUCCESS", components[2].ToUpperInvariant()));
                })
               .ToArray();
        }

        /// <summary>
        /// List peers with open connections
        /// ipfs swarm peers lists the set of peers this node is connected to.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<MultiAddress>> Peers()
        {
            HttpContent content = await ExecuteGetAsync("peers", null, null);
            string json = await content.ReadAsStringAsync();
            var swarmPeers = JsonConvert.DeserializeObject<IDictionary<string, IList<string>>>(json);
            return swarmPeers
                .SelectMany(x => x.Value)
                .Select(x => new MultiAddress(x)).ToArray();
        }
    }
}
