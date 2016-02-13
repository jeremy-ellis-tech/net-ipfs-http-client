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
            string mockAddress = "http://127.0.0.1:5001";
            var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler));

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
