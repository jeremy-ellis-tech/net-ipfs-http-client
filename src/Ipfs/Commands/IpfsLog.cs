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
        public IpfsLog(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient) { }

        /// <summary>
        /// Change the logging level
        /// 
        /// 'ipfs log level' is a utility command used to change the logging
        /// output of a running daemon.
        /// </summary>
        /// <param name="subsystem">the subsystem logging identifier. Use 'all' for all subsystems.</param>
        /// <param name="level">one of: debug, info, notice, warning, error, critical</param>
        /// <returns></returns>
        public async Task<HttpContent> Level(string subsystem, IpfsLevel level)
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

            return await ExecuteGetAsync("level", new[] { subsystem, levelValue });
        }

        /// <summary>
        /// Read the logs
        /// 'ipfs log tail' is a utility command used to read log output as it is written.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpContent> Tail()
        {
            return await ExecuteGetAsync("tail");
        }
    }
}
