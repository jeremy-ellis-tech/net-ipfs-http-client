using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsPin : IpfsCommand
    {
        public IpfsPin(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Pins objects to local storage
        /// Retrieves the object named by <ipfs-path> and stores it locally
        /// on disk.
        /// </summary>
        /// <param name="ipfsPath">Path to object(s) to be pinned</param>
        /// <param name="recursive">Recursively pin the object linked to by the specified object(s)</param>
        /// <returns></returns>
        public async Task<HttpContent> Add(string ipfsPath, bool recursive = false)
        {
            var flags = new Dictionary<string, string>();

            if(recursive)
            {
                flags.Add("recursive", "true");
            }

            return await ExecuteGetAsync("add", ipfsPath, flags);
        }

        /// <summary>
        ///  List objects pinned to local storage
        ///  Returns a list of hashes of objects being pinned. Objects that are indirectly
        ///  or recursively pinned are not included in the list.
        /// </summary>
        /// <param name="type">The type of pinned keys to list. Can be "direct", "indirect", "recursive", or "all". Defaults to "direct"</param>
        /// <returns></returns>
        public async Task<HttpContent> Ls(IpfsType? type = null)
        {
            var flags = new Dictionary<string, string>();

            if(type != null)
            {
                string typeValue = null;

                switch (type.Value)
                {
                    case IpfsType.Direct:
                        typeValue = "direct";
                        break;
                    case IpfsType.Indirect:
                        typeValue = "indirect";
                        break;
                    case IpfsType.Recursive:
                        typeValue = "recursive";
                        break;
                    case IpfsType.All:
                        typeValue = "all";
                        break;
                    default:
                        break;
                }

                flags.Add("type", typeValue);
            }

            return await ExecuteGetAsync("ls", flags);
        }

        /// <summary>
        /// Unpin an object from local storage
        /// Removes the pin from the given object allowing it to be garbage
        /// collected if needed.
        /// </summary>
        /// <param name="ipfsPath">Path to object(s) to be unpinned</param>
        /// <param name="recursive">Recursively unpin the object linked to by the specified object(s)</param>
        /// <returns></returns>
        public async Task<HttpContent> Rm(string ipfsPath, bool recursive = false)
        {
            var flags = new Dictionary<string, string>();

            if (recursive)
            {
                flags.Add("recursive", "true");
            }

            return await ExecuteGetAsync("rm", ipfsPath, flags);
        }
    }
}
