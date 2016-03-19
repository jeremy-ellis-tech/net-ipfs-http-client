using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsFile : IpfsCommand
    {
        public IpfsFile(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient)
        {
        }

        /// <summary>
        /// List directory contents for Unix-filesystem objects
        /// 
        /// Retrieves the object named by <ipfs-or-ipns-path> and displays the
        /// contents.
        /// 
        /// The JSON output contains size information.For files, the child size
        /// is the total size of the file contents.For directories, the child
        /// size is the IPFS link size.
        /// </summary>
        /// <param name="path">The path to the IPFS object(s) to list links from</param>
        /// <returns></returns>
        public async Task<HttpContent> Ls(string path)
        {
            return await ExecuteGetAsync("ls", null, null);
        }
    }
}
