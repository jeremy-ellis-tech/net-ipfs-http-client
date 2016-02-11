using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsName : IpfsCommand
    {
        internal IpfsName()
        {
        }

        internal IpfsName(string address) : base(address)
        {
        }

        internal IpfsName(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/name/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Publish an object to IPNS
        /// 
        /// IPNS is a PKI namespace, where names are the hashes of public keys, and
        /// the private key enables publishing new (signed) values.In publish, the
        /// default value of<name> is your own identity public key.
        /// </summary>
        /// <param name="name">The IPNS name to publish to. Defaults to your node's peerID</param>
        /// <param name="ipfsPath">IPFS path of the obejct to be published at <name></param>
        /// <returns></returns>
        public async Task<byte[]> Publish(string name, string ipfsPath)
        {
            return await ExecuteAsync("publish", ToEnumerable(name, ipfsPath));
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
        public async Task<byte[]> Resolve(string name)
        {
            return await ExecuteAsync("resolve", ToEnumerable(name));
        }
    }
}
