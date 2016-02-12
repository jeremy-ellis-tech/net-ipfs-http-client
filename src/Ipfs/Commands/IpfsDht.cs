using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsDht : IpfsCommand
    {
        internal IpfsDht(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/dht/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Run a 'FindPeer' query through the DHT
        /// </summary>
        /// <param name="peerID">The peer to search for</param>
        /// <returns></returns>
        public async Task<string> FindPeer(string peerID)
        {
            return await ExecuteAsync("findpeer", ToEnumerable(peerID));
        }

        /// <summary>
        /// Run a 'FindProviders' query through the DHT
        /// FindProviders will return a list of peers who are able to provide the value requested.
        /// </summary>
        /// <param name="key">The key to find providers for</param>
        /// <param name="verbose">Write extra information</param>
        /// <returns></returns>
        public async Task<string> FindProvs(string key, bool verbose = false)
        {
            var flags = new Dictionary<string, string>();

            if(verbose)
            {
                flags.Add("verbose", "true");
            }

            return await ExecuteAsync("findprovs", ToEnumerable(key), flags);
        }

        /// <summary>
        /// Run a 'findClosestPeers' query through the DHT
        /// </summary>
        /// <param name="peerID">The peerID to run the query against</param>
        /// <param name="verbose">Write extra information</param>
        /// <returns></returns>
        public async Task<string> Query(string peerID, bool verbose = false)
        {
            var flags = new Dictionary<string, string>();

            if (verbose)
            {
                flags.Add("verbose", "true");
            }

            return await ExecuteAsync("findprovs", ToEnumerable(peerID), flags);
        }
    }
}
