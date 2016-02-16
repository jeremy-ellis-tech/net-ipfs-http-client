using Ipfs.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ipfs
{
    public abstract class IpfsCommand
    {
        private readonly Uri _commandUri;
        private readonly HttpClient _httpClient;

        public IpfsCommand(Uri commandUri, HttpClient httpClient)
        {
            _commandUri = commandUri;
            _httpClient = httpClient;
        }

        protected async Task<HttpContent> ExecuteGetAsync(string methodName, IEnumerable<string> args, IDictionary<string, string> flags)
        {
            Uri commandUri = GetSubCommandUri(methodName, args, flags);

            Debug.WriteLine(String.Format("IpfsCommand.ExecuteGetAsync: {0}", commandUri.ToString()));

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(commandUri);

            httpResponse.EnsureSuccessStatusCode();

            return httpResponse.Content;
        }

        protected async Task<HttpContent> ExecutePostAsync(string methodName, IEnumerable<string> args, IDictionary<string, string> flags, Tuple<string, Stream> file)
        {
            return await ExecutePostAsync(methodName, args, flags, new List<Tuple<string, Stream>> { file });
        }

        protected async Task<HttpContent> ExecutePostAsync(string methodName, IEnumerable<string> args, IDictionary<string, string> flags, IEnumerable<Tuple<string, Stream>> files)
        {
            Uri commandUri = GetSubCommandUri(methodName, args, flags);

            Debug.WriteLine(String.Format("IpfsCommand.ExecutePostAsync: {0}", commandUri.ToString()));

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            foreach (var file in files)
            {
                StreamContent sc = new StreamContent(file.Item2);
                sc.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                multiContent.Add(sc, "file", file.Item1);
            }

            var httpResponse = await _httpClient.PostAsync(commandUri, multiContent);

            httpResponse.EnsureSuccessStatusCode();

            return httpResponse.Content;
        }

        private Uri GetSubCommandUri(string methodName, IEnumerable<string> args, IDictionary<string, string> flags)
        {
            Uri commandUri = new Uri(_commandUri.ToString());

            if (!String.IsNullOrEmpty(methodName))
            {
                commandUri = UriHelper.AppendPath(commandUri, methodName);
            }

            if (args != null && args.Count() > 0)
            {
                commandUri = UriHelper.AppendQuery(commandUri, args.Select(x=> new Tuple<string,string>("arg", x)));
            }

            if (flags != null && flags.Count > 0)
            {
                commandUri = UriHelper.AppendQuery(commandUri, flags.Select(x=> new Tuple<string,string>(x.Key, x.Value)));
            }

            return commandUri;
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
