using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsLog : IpfsCommand
    {
        internal IpfsLog()
        {
        }

        internal IpfsLog(string address) : base(address)
        {
        }

        internal IpfsLog(string address, HttpClient httpClient) : base(address, httpClient)
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
                    uriBuilder.Path += "api/v0/log/";
                    _baseUri = uriBuilder.Uri;
                }

                return _baseUri;
            }
        }

        /// <summary>
        /// Change the logging level
        /// 
        /// 'ipfs log level' is a utility command used to change the logging
        /// output of a running daemon.
        /// </summary>
        /// <param name="subsystem">the subsystem logging identifier. Use 'all' for all subsystems.</param>
        /// <param name="level">one of: debug, info, notice, warning, error, critical</param>
        /// <returns></returns>
        public async Task<string> Level(string subsystem, IpfsLevel level)
        {
            string levelValue = null;

            switch (level)
            {
                case IpfsLevel.Debug:
                    levelValue = "debug";
                    break;
                case IpfsLevel.Info:
                    levelValue = "info";
                    break;
                case IpfsLevel.Notice:
                    levelValue = "notice";
                    break;
                case IpfsLevel.Warning:
                    levelValue = "warning";
                    break;
                case IpfsLevel.Error:
                    levelValue = "error";
                    break;
                case IpfsLevel.Critical:
                    levelValue = "critical";
                    break;
                default:
                    break;
            }

            return await ExecuteAsync("level", ToEnumerable(subsystem, levelValue));
        }

        /// <summary>
        /// Read the logs
        /// 'ipfs log tail' is a utility command used to read log output as it is written.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Tail()
        {
            return await ExecuteAsync("tail");
        }
    }
}
