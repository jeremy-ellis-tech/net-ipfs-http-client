using Ipfs.Json;
using Newtonsoft.Json;
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
        public IpfsConfig(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Opens the config file for editing in $EDITOR
        /// To use 'ipfs config edit', you must have the $EDITOR environment
        /// variable set to your preferred text editor.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Edit()
        {
            return await ExecuteGetAsync("edit", null, null);
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
            return await ExecuteGetAsync("replace", ToEnumerable(file), null);
        }

        /// <summary>
        /// Outputs the content of the config file
        /// WARNING: Your private key is stored in the config file, and it will be
        /// included in the output of this command.
        /// </summary>
        /// <returns></returns>
        public async Task<IpfsConfigShow> Show()
        {
            HttpContent content = await ExecuteGetAsync("show", null, null);
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IpfsConfigShow>(json);
        }
    }
}
