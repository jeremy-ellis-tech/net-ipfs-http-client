using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ipfs.Json;

namespace Ipfs.Commands
{
    public class IpfsDht : IpfsCommand
    {
        internal IpfsDht(Uri commandUri, HttpClient httpClient, IJsonSerializer jsonSerializer) : base(commandUri, httpClient, jsonSerializer)
        {
        }

        /// <summary>
        /// Run a 'FindPeer' query through the DHT
        /// </summary>
        /// <param name="peerID">The peer to search for</param>
        /// <returns></returns>
        public async Task<HttpContent> FindPeer(string peerID)
        {
            return await ExecuteGetAsync("findpeer", peerID);
        }

        /// <summary>
        /// Run a 'FindProviders' query through the DHT
        /// FindProviders will return a list of peers who are able to provide the value requested.
        /// </summary>
        /// <param name="key">The key to find providers for</param>
        /// <param name="verbose">Write extra information</param>
        /// <returns></returns>
        public async Task<HttpContent> FindProvs(string key, bool verbose = false)
        {
            var flags = new Dictionary<string, string>();

            if(verbose)
            {
                flags.Add("verbose", "true");
            }

            return await ExecuteGetAsync("findprovs", key, flags);
        }

        /// <summary>
        /// Run a 'findClosestPeers' query through the DHT
        /// </summary>
        /// <param name="peerID">The peerID to run the query against</param>
        /// <param name="verbose">Write extra information</param>
        /// <returns></returns>
        public async Task<HttpContent> Query(string peerID, bool verbose = false)
        {
            var flags = new Dictionary<string, string>();

            if (verbose)
            {
                flags.Add("verbose", "true");
            }

            return await ExecuteGetAsync("findprovs", peerID, flags);
        }
    }
}
