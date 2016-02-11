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
        internal IpfsObject()
        {
        }

        internal IpfsObject(string address) : base(address)
        {
        }

        internal IpfsObject(string address, HttpClient httpClient) : base(address, httpClient)
        {
        }

        private Uri _baseUri;
        protected override Uri CommandUri
        {
            get
            {
                if(_baseUri == null)
                {
                    UriBuilder uriBuilder = new UriBuilder(_address);
                    uriBuilder.Path += "api/v0/object/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Outputs the raw bytes in an IPFS object
        /// 
        /// ipfs data is a plumbing command for retreiving the raw bytes stored in
        /// a DAG node.It outputs to stdout, and <key> is a base58 encoded
        /// multihash.
        /// </summary>
        /// <param name="key">Key of the object to retrieve, in base58-encoded multihash format</param>
        /// <returns></returns>
        public async Task<byte[]> Data(string key)
        {
            return await ExecuteAsync("data", ToEnumerable(key));
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
        public async Task<byte[]> Get(string key, IpfsEncoding encoding)
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
        public async Task<byte[]> Links(string key)
        {
            return await ExecuteAsync("links", ToEnumerable(key));
        }

        /// <summary>
        /// Stores input as a DAG object, outputs its key
        /// 'ipfs object put' is a plumbing command for storing DAG nodes.
        /// It reads from stdin, and the output is a base58 encoded multihash.
        /// </summary>
        /// <param name="data">Data to be stored as a DAG object</param>
        /// <param name="encoding">Encoding type of <data>, either "protobuf" or "json"</param>
        /// <returns></returns>
        public async Task<byte[]> Put(string data, IpfsEncoding encoding)
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
        public async Task<byte[]> Stat(string key)
        {
            return await ExecuteAsync("stat", ToEnumerable(key));
        }
    }
}
