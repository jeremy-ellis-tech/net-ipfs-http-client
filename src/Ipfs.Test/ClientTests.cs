using Ipfs.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;

namespace Ipfs.Test
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ClientShouldThrowIfMethodCalledAfterBeingDisposed()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);
            var client = new IpfsClient(String.Empty, new HttpClient(mockHttpMessageHandler));

            client.Dispose();

            try
            {
                client.Commands().Wait();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
