using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs
{
    public abstract class IpfsCommand : IDisposable
    {
        private static string DefaultAddress = "http://127.0.0.1:5001";
        private static HttpClient DefaultHttpClient = new HttpClient();

        protected readonly string _address;
        protected readonly HttpClient _httpClient;

        internal IpfsCommand() : this(DefaultAddress, DefaultHttpClient)
        {

        }

        internal IpfsCommand(string address) : this(address, DefaultHttpClient)
        {

        }

        internal IpfsCommand(string address, HttpClient httpClient)
        {
            _address = address;
            _httpClient = httpClient;
        }

        protected abstract Uri CommandUri { get; }

        protected internal async Task<byte[]> ExecuteAsync(string method, IEnumerable<string> args = null, Dictionary<string,string> flags = null)
        {
            if(_disposed)
            {
                throw new ObjectDisposedException("IpfsCommand");
            }

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

            return await httpResponse.Content.ReadAsByteArrayAsync();
        }

        protected string Utf8Decode(byte[] data)
        {
            return Encoding.UTF8.GetString(data, 0, data.Length);
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

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_httpClient != null) _httpClient.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
