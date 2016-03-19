using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsTar : IpfsCommand
    {
        public IpfsTar(Uri commandUri, HttpClient httpClient) : base(commandUri, httpClient)
        {
        }

        //public async Task<HttpContent> Add()
        //{

        //}

        //public async Task<HttpContent> Cat()
        //{

        //}
    }
}
