using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsBootstrap : IpfsCommand
    {
        internal IpfsBootstrap()
        {
        }

        internal IpfsBootstrap(string address) : base(address)
        {
        }

        internal IpfsBootstrap(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/bootstrap/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }
    }
}
