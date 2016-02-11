using Ipfs.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;

namespace Ipfs.Test
{
    [TestClass]
    public class RequestUriTests
    {
        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectly()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

            //The client won't actually make any connections during tests.
            //The request gets caught by our MessageHandler
            string mockAddress = "http://127.0.0.1:5001";
            string expectedRequestUri = String.Format("{0}/api/v0/commands", mockAddress);

            using (var client = new IpfsClient(mockAddress, new HttpClient(mockHttpMessageHandler)))
            {
                client.Commands().Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }

        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectlyWithArgs()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

            string mockAddress = "http://127.0.0.1:5001";
            string mockHash = "QmPXME1oRtoT627YKaDPDQ3PwA8tdP9rWuAAweLzqSwAWT";
            string expectedRequestUri = String.Format("{0}/api/v0/ls?arg={1}", mockAddress, mockHash);

            using (var client = new IpfsClient(mockAddress, new HttpClient(mockHttpMessageHandler)))
            {
                client.Ls(mockHash).Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }

        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectlyWithArgsAndFlags()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

            string mockAddress = "http://127.0.0.1:5001";
            string mockFileLocation = @"MyFilePath.txt";
            string expectedRequestUri = String.Format("{0}/api/v0/add?arg={1}&recursive=true&quiet=true", mockAddress, mockFileLocation);

            using (var client = new IpfsClient(mockAddress, new HttpClient(mockHttpMessageHandler)))
            {
                client.Add(mockFileLocation, true, true).Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }

        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectlyWithFlagsNoArgs()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

            string mockAddress = "http://127.0.0.1:5001";
            string mockFileLocation = @"MyFilePath.txt";
            string expectedRequestUri = String.Format("{0}/api/v0/diag/net?timeout=1&vis=d3", mockAddress, mockFileLocation);

            using (var client = new IpfsClient(mockAddress, new HttpClient(mockHttpMessageHandler)))
            {
                client.Diag.Net("1", IpfsVis.D3).Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }
    }
}
