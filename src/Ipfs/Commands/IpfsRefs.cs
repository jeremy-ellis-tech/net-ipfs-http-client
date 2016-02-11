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
        internal IpfsRefs()
        {
        }

        internal IpfsRefs(string address) : base(address)
        {
        }

        internal IpfsRefs(string address, HttpClient httpClient) : base(address, httpClient)
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
                    builder.Path += "/api/v0/refs/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Lists all local references
        /// Displays the hashes of all local objects.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Local()
        {
            return await ExecuteAsync("local");
        }
    }
}
