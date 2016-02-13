using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsRepo : IpfsCommand
    {
        public IpfsRepo(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Perform a garbage collection sweep on the repo
        /// 
        /// 'ipfs repo gc' is a plumbing command that will sweep the local
        /// set of stored objects and remove ones that are not pinned in
        /// order to reclaim hard disk space.
        /// </summary>
        /// <param name="quiet">Write minimal output</param>
        /// <returns></returns>
        public async Task<HttpContent> GC(bool quiet = false)
        {
            var flags = new Dictionary<string, string>();

            if(quiet)
            {
                flags.Add("quiet", "true");
            }

            return await ExecuteAsync("gc", null, flags);
        }
    }
}
