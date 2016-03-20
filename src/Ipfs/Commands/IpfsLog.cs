using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
        /// <returns>Confirmation message</returns>
        public async Task<string> Level(string subsystem, IpfsLogLevel level)
        {
            string levelValue = null;

            switch (level)
            {
                case IpfsLogLevel.Debug:
                    levelValue = "debug";
                    break;
                case IpfsLogLevel.Info:
                    levelValue = "info";
                    break;
                case IpfsLogLevel.Notice:
                    levelValue = "notice";
                    break;
                case IpfsLogLevel.Warning:
                    levelValue = "warning";
                    break;
                case IpfsLogLevel.Error:
                    levelValue = "error";
                    break;
                case IpfsLogLevel.Critical:
                    levelValue = "critical";
                    break;
                default:
                    break;
            }

            HttpContent content = await ExecuteGetAsync("level", new[] { subsystem, levelValue });

            string json = await content.ReadAsStringAsync();

            if(String.IsNullOrEmpty(json))
            {
                return String.Empty;
            }

            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return jsonDict["Message"];
        }

        /// <summary>
        /// Read the logs
        /// 'ipfs log tail' is a utility command used to read log output as it is written.
        /// </summary>
        /// <returns>A stream to the log tail output</returns>
        public async Task<Stream> Tail()
        {
            HttpContent content = await ExecuteGetAsync("tail");

            return await content.ReadAsStreamAsync();
        }
    }
}
