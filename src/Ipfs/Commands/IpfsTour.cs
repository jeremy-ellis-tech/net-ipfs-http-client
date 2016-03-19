using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsTour : IpfsCommand
    {
        public IpfsTour(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Show a list of IPFS Tour topics
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> List()
        {
            return await ExecuteGetAsync("list");
        }

        /// <summary>
        /// Show the next IPFS Tour topic
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Next()
        {
            return await ExecuteGetAsync("next");
        }

        /// <summary>
        /// Restart the IPFS Tour
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Restart()
        {
            return await ExecuteGetAsync("restart");
        }
    }
}
