using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsRefs : IpfsCommand
    {
        public IpfsRefs(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Lists all local references
        /// Displays the hashes of all local objects.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Local()
        {
            return await ExecuteGetAsync("local", null, null);
        }
    }
}
