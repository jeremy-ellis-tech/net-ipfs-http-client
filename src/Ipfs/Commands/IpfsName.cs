using Ipfs.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsName : IpfsCommand
    {
        internal IpfsName(Uri commandUri, HttpClient httpClient, IJsonSerializer jsonSerializer) : base(commandUri, httpClient, jsonSerializer)
        {
        }

        /// <summary>
        /// Publish an object to IPNS
        /// 
        /// IPNS is a PKI namespace, where names are the hashes of public keys, and
        /// the private key enables publishing new (signed) values.In publish, the
        /// default value of<name> is your own identity public key.
        /// </summary>
        /// <param name="name">The IPNS name to publish to. Defaults to your node's peerID</param>
        /// <param name="ipfsPath">IPFS path of the obejct to be published at <name> </param>
        /// <returns></returns>
        public async Task<IpfsNamePublish> Publish(string name, string ipfsPath)
        {
            HttpContent content = await ExecuteGetAsync("publish", new[] { name, ipfsPath });
            string json = await content.ReadAsStringAsync();
            Json.IpfsNamePublish ret = _jsonSerializer.Deserialize<Json.IpfsNamePublish>(json);
            return new IpfsNamePublish { Name = ret.Name, Value = new MultiHash(ret.Value)};
        }

        /// <summary>
        /// Gets the value currently published at an IPNS name
        /// 
        /// IPNS is a PKI namespace, where names are the hashes of public keys, and
        /// the private key enables publishing new (signed) values.In resolve, the
        /// default value of<name> is your own identity public key.
        /// </summary>
        /// <param name="name">The IPNS name to resolve. Defaults to your node's peerID.</param>
        /// <returns></returns>
        public async Task<string> Resolve(string name)
        {
            HttpContent content = await ExecuteGetAsync("resolve", name);
            string json = await content.ReadAsStringAsync();
            Json.IpfsNameResolve resolve = _jsonSerializer.Deserialize<Json.IpfsNameResolve>(json);
            return resolve.Path;
        }
    }
}
