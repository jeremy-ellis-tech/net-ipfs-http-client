using Ipfs.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsConfig : IpfsCommand
    {
        internal IpfsConfig(Uri commandUri, HttpClient httpClient, IJsonSerializer jsonSerializer) : base(commandUri, httpClient, jsonSerializer)
        {
        }

        /// <summary>
        /// Opens the config file for editing in $EDITOR
        /// To use 'ipfs config edit', you must have the $EDITOR environment
        /// variable set to your preferred text editor.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Edit()
        {
            return await ExecuteGetAsync("edit");
        }

        /// <summary>
        /// Replaces the config with <file>
        /// Make sure to back up the config file first if neccessary, this operation
        /// can't be undone.
        /// </summary>
        /// <param name="file">The file to use as the new config</param>
        /// <returns></returns>
        public async Task<HttpContent> Replace(string file)
        {
            return await ExecuteGetAsync("replace", file);
        }

        /// <summary>
        /// Outputs the content of the config file
        /// WARNING: Your private key is stored in the config file, and it will be
        /// included in the output of this command.
        /// </summary>
        /// <returns></returns>
        public async Task<IpfsConfigShow> Show()
        {
            HttpContent content = await ExecuteGetAsync("show");
            var json = await content.ReadAsStringAsync();
            return _jsonSerializer.Deserialize<IpfsConfigShow>(json);
        }
    }
}
