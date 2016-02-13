using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsUpdate : IpfsCommand
    {
        public IpfsUpdate(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Checks if updates are available
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Check()
        {
            return await ExecuteAsync("check", null, null);
        }

        /// <summary>
        /// List the changelog for the latest versions of IPFS
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Log()
        {
            return await ExecuteAsync("log", null, null);
        }
    }
}
