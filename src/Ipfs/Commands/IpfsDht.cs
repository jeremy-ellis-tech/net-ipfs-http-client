using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsDht : IpfsCommand
    {
        internal IpfsDht()
        {
        }

        internal IpfsDht(string address) : base(address)
        {
        }

        internal IpfsDht(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if (_baseUri == null)
                {
                    UriBuilder uriBuilder = new UriBuilder(_address);
                    uriBuilder.Path += "api/v0/dht/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }
    }
}
