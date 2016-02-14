using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsBlock : IpfsCommand
    {
        public IpfsBlock(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// ipfs block get <key> - Get a raw IPFS block
        /// 'ipfs block get' is a plumbing command for retreiving raw ipfs blocks.
        /// </summary>
        /// <param name="key">The base58 multihash of an existing block to get</param>
        /// <returns></returns>
        public async Task<HttpContent> Get(string key)
        {
            return await ExecuteGetAsync("get", ToEnumerable(key), null);
        }

        /// <summary>
        /// ipfs block put <data> - Stores input as an IPFS block
        /// ipfs block put is a plumbing command for storing raw ipfs blocks.
        /// It reads from stdin, and <key> is a base58 encoded multihash.
        /// </summary>
        /// <param name="data">The data to be stored as an IPFS block</param>
        /// <returns></returns>
        public async Task<HttpContent> Put(byte[] data)
        {
            return await ExecuteGetAsync("put", ToEnumerable(data.ToString()), null);
        }

        /// <summary>
        /// Print information of a raw IPFS block
        /// </summary>
        /// <param name="key">The base58 multihash of an existing block to get</param>
        /// <returns></returns>
        public async Task<HttpContent> Stat(string key)
        {
            return await ExecuteGetAsync("get", ToEnumerable(key), null);
        }
    }
}
