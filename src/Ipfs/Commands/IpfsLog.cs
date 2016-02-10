using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsLog : IpfsCommand
    {
        internal IpfsLog()
        {
        }

        internal IpfsLog(string address) : base(address)
        {
        }

        internal IpfsLog(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/log/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }
    }
}
