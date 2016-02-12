using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsBlock : IpfsCommand
    {
        internal IpfsBlock(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if(_baseUri == null)
                {
                    UriBuilder builder = new UriBuilder(_address);
                    builder.Path += "/api/v0/block/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// ipfs block get <key> - Get a raw IPFS block
        /// 'ipfs block get' is a plumbing command for retreiving raw ipfs blocks.
        /// </summary>
        /// <param name="key">The base58 multihash of an existing block to get</param>
        /// <returns></returns>
        public async Task<string> Get(string key)
        {
            return await ExecuteAsync("get", ToEnumerable(key));
        }

        /// <summary>
        /// ipfs block put <data> - Stores input as an IPFS block
        /// ipfs block put is a plumbing command for storing raw ipfs blocks.
        /// It reads from stdin, and <key> is a base58 encoded multihash.
        /// </summary>
        /// <param name="data">The data to be stored as an IPFS block</param>
        /// <returns></returns>
        public async Task<string> Put(byte[] data)
        {
            return await ExecuteAsync("put", ToEnumerable(data.ToString()));
        }

        /// <summary>
        /// Print information of a raw IPFS block
        /// </summary>
        /// <param name="key">The base58 multihash of an existing block to get</param>
        /// <returns></returns>
        public async Task<string> Stat(string key)
        {
            return await ExecuteAsync("get", ToEnumerable(key));
        }
    }
}
