using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ipfs.Test.Mocks
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _toReturn;

        public Uri RequestUri { get; private set; }

        public MockHttpMessageHandler(HttpResponseMessage toReturn)
        {
            _toReturn = toReturn;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestUri = request.RequestUri;
            return await Task.FromResult(_toReturn);
        }
    }
}
