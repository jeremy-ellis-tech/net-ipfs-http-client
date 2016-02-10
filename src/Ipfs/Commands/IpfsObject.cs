using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsObject : IpfsCommand
    {
        internal IpfsObject()
        {
        }

        internal IpfsObject(string address) : base(address)
        {
        }

        internal IpfsObject(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if(_baseUri == null)
                {
                    UriBuilder uriBuilder = new UriBuilder(_address);
                    uriBuilder.Path += "api/v0/object/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }
    }
}
