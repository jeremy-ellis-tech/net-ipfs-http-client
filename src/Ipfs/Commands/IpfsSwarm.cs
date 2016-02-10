using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsSwarm : IpfsCommand
    {
        internal IpfsSwarm()
        {
        }

        internal IpfsSwarm(string address) : base(address)
        {
        }

        internal IpfsSwarm(string address, HttpClient httpClient) : base(address, httpClient)
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
                    builder.Path += "/api/v0/swarm/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        public async Task<byte[]> Connect(string address)
        {
            return await ExecuteAsync("connect", Singleton(address));
        }
    }
}
