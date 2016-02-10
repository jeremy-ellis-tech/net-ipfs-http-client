using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsBlock : IpfsCommand
    {
        internal IpfsBlock()
        {
        }

        internal IpfsBlock(string address) : base(address)
        {
        }

        internal IpfsBlock(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if(_baseUri == null)
                {
                    UriBuilder builder = new UriBuilder(_address);
                    builder.Path += "/api/v0/block/";
                    _baseUri = builder.Uri;
                }

                return _baseUri;
            }
        }

        public async Task<byte[]> Get(string key)
        {
            var args = new Dictionary<string, string>
            {
                {"arg", key }
            };

            return await ExecuteAsync("get");
        }

        public async Task<byte[]> Put(byte[] data)
        {
            var args = new Dictionary<string, string>
            {
                {"arg", data.ToString() }
            };

            return await ExecuteAsync("get");
        }

        public async Task<byte[]> Stat(string key)
        {
            var args = new Dictionary<string, string>
            {
                {"arg", key }
            };

            return await ExecuteAsync("get");
        }
    }
}
