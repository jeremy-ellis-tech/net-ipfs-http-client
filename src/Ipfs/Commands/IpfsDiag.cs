using System;
using System.Net.Http;

namespace Ipfs.Commands
{
    public class IpfsDiag : IpfsCommand
    {
        internal IpfsDiag()
        {
        }

        internal IpfsDiag(string address) : base(address)
        {
        }

        internal IpfsDiag(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/diag/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }
    }
}
