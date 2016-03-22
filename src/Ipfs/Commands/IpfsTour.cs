using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Ipfs.Json;

namespace Ipfs.Commands
{
    public class IpfsTour : IpfsCommand
    {
        internal IpfsTour(Uri commandUri, HttpClient httpClient, IJsonSerializer jsonSerializer) : base(commandUri, httpClient, jsonSerializer)
        {
        }

        /// <summary>
        /// Show a list of IPFS Tour topics
        /// </summary>
        /// <returns>Stream to tour topcs</returns>
        public async Task<Stream> List()
        {
            HttpContent content = await ExecuteGetAsync("list");

            return await content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Show the next IPFS Tour topic
        /// </summary>
        /// <returns>Next tour topic</returns>
        public async Task<Stream> Next()
        {
            HttpContent content = await ExecuteGetAsync("next");

            return await content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Restart the IPFS Tour
        /// </summary>
        public async Task Restart()
        {
            await ExecuteGetAsync("restart");
        }
    }
}
