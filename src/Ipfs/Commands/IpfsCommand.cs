using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public abstract class IpfsCommand
    {
        protected readonly string _address;
        protected readonly HttpClient _httpClient;

        internal IpfsCommand(string address, HttpClient httpClient)
        {
            _address = address;
            _httpClient = httpClient;
        }

        protected abstract Uri CommandUri { get; }

        /// <summary>
        /// Send a command to the specified IPFS API interface
        /// </summary>
        /// <param name="method">The name of the method</param>
        /// <param name="args">Any arguments</param>
        /// <param name="flags">Any named parameters</param>
        /// <returns>The HttpMessageContent</returns>
        protected internal async Task<string> ExecuteAsync(string method, IEnumerable<string> args = null, Dictionary<string,string> flags = null)
        {
            UriBuilder uriBuilder = new UriBuilder(CommandUri);
            uriBuilder.Path += method;

            bool addArgs = args != null && args.Count() > 0;
            bool addFlags = flags != null && flags.Count > 0;

            string query = String.Empty;

            if(addArgs)
            {
                query += String.Join("&", args.Select(x => String.Format("arg={0}", x)));

                if (addFlags)
                {
                    query += "&";
                }
            }

            if(addFlags)
            {
                query += String.Join("&", flags.Select(x => String.Format("{0}={1}", x.Key, x.Value)));
            }

            uriBuilder.Query += query;

            Uri queryUri = uriBuilder.Uri;

            Debug.WriteLine(String.Format("Querying: {0}", queryUri.ToString()));

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(queryUri);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Helper method to return values in Enumerable
        /// </summary>
        /// <typeparam name="T">The type of the enumerable elements</typeparam>
        /// <param name="values">Element of the singleton enumerable</param>
        /// <returns>Enumerable containing values</returns>
        protected IEnumerable<T> ToEnumerable<T>(params T[] values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}
