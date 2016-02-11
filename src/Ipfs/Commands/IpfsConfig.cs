using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsConfig : IpfsCommand
    {
        internal IpfsConfig()
        {
        }

        internal IpfsConfig(string address) : base(address)
        {
        }

        internal IpfsConfig(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/config/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Opens the config file for editing in $EDITOR
        /// To use 'ipfs config edit', you must have the $EDITOR environment
        /// variable set to your preferred text editor.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> Edit()
        {
            return await ExecuteAsync("edit");
        }

        /// <summary>
        /// Replaces the config with <file>
        /// Make sure to back up the config file first if neccessary, this operation
        /// can't be undone.
        /// </summary>
        /// <param name="file">The file to use as the new config</param>
        /// <returns></returns>
        public async Task<byte[]> Replace(string file)
        {
            return await ExecuteAsync("replace", ToEnumerable(file));
        }

        /// <summary>
        /// Outputs the content of the config file
        /// WARNING: Your private key is stored in the config file, and it will be
        /// included in the output of this command.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> Show()
        {
            return await ExecuteAsync("show");
        }
    }
}
