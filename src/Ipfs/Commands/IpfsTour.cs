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
                    builder.Path += "/api/v0/swarm/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }
    }
}
