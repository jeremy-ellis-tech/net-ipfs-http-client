using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsUpdate : IpfsCommand
    {
        internal IpfsUpdate(string address, HttpClient httpClient) : base(address, httpClient)
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
                    builder.Path += "/api/v0/update/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Checks if updates are available
        /// </summary>
        /// <returns></returns>
        public async Task<string> Check()
        {
            return await ExecuteAsync("check");
        }

        /// <summary>
        /// List the changelog for the latest versions of IPFS
        /// </summary>
        /// <returns></returns>
        public async Task<string> Log()
        {
            return await ExecuteAsync("log");
        }
    }
}
