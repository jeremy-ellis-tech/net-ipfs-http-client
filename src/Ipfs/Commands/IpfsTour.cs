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
        internal IpfsTour()
        {
        }

        internal IpfsTour(string address) : base(address)
        {
        }

        internal IpfsTour(string address, HttpClient httpClient) : base(address, httpClient)
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
                    builder.Path += "/api/v0/tour/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Show a list of IPFS Tour topics
        /// </summary>
        /// <returns></returns>
        public async Task<string> List()
        {
            return await ExecuteAsync("list");
        }

        /// <summary>
        /// Show the next IPFS Tour topic
        /// </summary>
        /// <returns></returns>
        public async Task<string> Next()
        {
            return await ExecuteAsync("next");
        }

        /// <summary>
        /// Restart the IPFS Tour
        /// </summary>
        /// <returns></returns>
        public async Task<string> Restart()
        {
            return await ExecuteAsync("restart");
        }
    }
}
