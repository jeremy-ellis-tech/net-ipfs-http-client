using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsObject : IpfsCommand
    {
        public IpfsObject(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Outputs the raw bytes in an IPFS object
        /// 
        /// ipfs data is a plumbing command for retreiving the raw bytes stored in
        /// a DAG node.It outputs to stdout, and <key> is a base58 encoded
        /// multihash.
        /// </summary>
        /// <param name="key">Key of the object to retrieve, in base58-encoded multihash format</param>
        /// <returns></returns>
        public async Task<HttpContent> Data(string key)
        {
            return await ExecuteAsync("data", ToEnumerable(key), null);
        }

        /// <summary>
        /// Get and serialize the DAG node named by <key>
        /// 'ipfs object get' is a plumbing command for retreiving DAG nodes.
        /// It serializes the DAG node to the format specified by the "--encoding"
        /// flag.It outputs to stdout, and <key> is a base58 encoded multihash.
        /// </summary>
        /// <param name="key">Key of the object to retrieve (in base58-encoded multihash format)</param>
        /// <param name="encoding">The encoding of the data</param>
        /// <returns></returns>
        public async Task<HttpContent> Get(string key, IpfsEncoding encoding)
        {
            var flags = new Dictionary<string, string>();

            string encodingValue = null;

            switch (encoding)
            {
                case IpfsEncoding.Json:
                    encodingValue = "json";
                    break;
                case IpfsEncoding.Protobuf:
                    encodingValue = "protobuf";
                    break;
                default:
                    break;
            }

            flags.Add("encoding", encodingValue);

            return await ExecuteAsync("get", ToEnumerable(key), flags);
        }

        /// <summary>
        /// Outputs the links pointed to by the specified object
        /// 'ipfs object links' is a plumbing command for retreiving the links from
        /// a DAG node.It outputs to stdout, and <key> is a base58 encoded
        /// multihash.
        /// </summary>
        /// <param name="key">Key of the object to retrieve, in base58-encoded multihash format</param>
        /// <returns></returns>
        public async Task<HttpContent> Links(string key)
        {
            return await ExecuteAsync("links", ToEnumerable(key), null);
        }

        /// <summary>
        /// Stores input as a DAG object, outputs its key
        /// 'ipfs object put' is a plumbing command for storing DAG nodes.
        /// It reads from stdin, and the output is a base58 encoded multihash.
        /// </summary>
        /// <param name="data">Data to be stored as a DAG object</param>
        /// <param name="encoding">Encoding type of <data>, either "protobuf" or "json"</param>
        /// <returns></returns>
        public async Task<HttpContent> Put(string data, IpfsEncoding encoding)
        {
            var flags = new Dictionary<string, string>();

            string encodingValue = null;

            switch (encoding)
            {
                case IpfsEncoding.Json:
                    encodingValue = "json";
                    break;
                case IpfsEncoding.Protobuf:
                    encodingValue = "protobuf";
                    break;
                default:
                    break;
            }

            flags.Add("encoding", encodingValue);

            return await ExecuteAsync("put", ToEnumerable(data), flags);
        }

        /// <summary>
        /// Get stats for the DAG node named by <key>
        /// 'ipfs object stat' is a plumbing command to print DAG node statistics.
        /// <key> is a base58 encoded multihash.It outputs to stdout:
        /// </summary>
        /// <param name="key">Key of the object to retrieve (in base58-encoded multihash format)</param>
        /// <returns></returns>
        public async Task<HttpContent> Stat(string key)
        {
            return await ExecuteAsync("stat", ToEnumerable(key), null);
        }
    }
}
