using Ipfs.Commands;
using Ipfs.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ipfs.Test
{
    [TestClass]
    public class CommandTests
    {
        //The client won't actually make any connections during tests.
        //The request gets caught by our MessageHandlers
                
        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectly()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);
            string mockAddress = "http://127.0.0.1:5001";
            string expectedRequestUri = String.Format("{0}/api/v0/commands", mockAddress);

            using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
            {
                client.Commands().Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }

        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectlyWithArgs()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent("{\"Objects\":[{\"Hash\":\"QmXarR6rgkQ2fDSHjSY5nM2kuCXKYGViky5nohtwgF65Ec\",\"Links\":[{\"Name\":\"about\",\"Hash\":\"QmZTR5bcpQD7cFgTorqxZDYaew1Wqgfbd2ud9QqGPAkK2V\",\"Size\":1688,\"Type\":2},{\"Name\":\"contact\",\"Hash\":\"QmYCvbfNbCwFR45HiNP45rwJgvatpiW38D961L5qAhUM5Y\",\"Size\":200,\"Type\":2},{\"Name\":\"help\",\"Hash\":\"QmY5heUM5qgRubMDD1og9fhCPA6QdkMp3QCwd4s7gJsyE7\",\"Size\":322,\"Type\":2},{\"Name\":\"quick-start\",\"Hash\":\"QmXifYTiYxz8Nxt3LmjaxtQNLYkjdh324L4r81nZSadoST\",\"Size\":1707,\"Type\":2},{\"Name\":\"readme\",\"Hash\":\"QmPZ9gcCEpqKTo6aq61g2nXGUhM4iCL3ewB6LDXZCtioEB\",\"Size\":1102,\"Type\":2},{\"Name\":\"security-notes\",\"Hash\":\"QmTumTjvcYCAvRRwQ8sDRxh8ezmrcr88YFU7iYNroGGTBZ\",\"Size\":1027,\"Type\":2}]}]}\n");

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

            string mockAddress = "http://127.0.0.1:5001";
            string mockHash = "QmXarR6rgkQ2fDSHjSY5nM2kuCXKYGViky5nohtwgF65Ec";
            string expectedRequestUri = String.Format("{0}/api/v0/ls?arg={1}", mockAddress, mockHash);

            using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
            {
                client.Ls(mockHash).Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }

        //[TestMethod]
        //public void RequestUriShouldBeBuiltCorrectlyWithArgsAndFlags()
        //{
        //    var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
        //    mockResponse.Content = new StringContent(String.Empty);

        //    var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

        //    string mockAddress = "http://127.0.0.1:5001";
        //    string mockFileLocation = @"MyFilePath.txt";
        //    string expectedRequestUri = String.Format("{0}/api/v0/add?arg={1}&recursive=true&quiet=true", mockAddress, mockFileLocation);

        //    using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
        //    {
        //        client.Add(mockFileLocation, true, true).Wait();
        //    }

        //    Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        //}

        [TestMethod]
        public void RequestUriShouldBeBuiltCorrectlyWithFlagsNoArgs()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(String.Empty);

            var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse);

            string mockAddress = "http://127.0.0.1:5001";
            string mockFileLocation = @"MyFilePath.txt";
            string expectedRequestUri = String.Format("{0}/api/v0/diag/net?timeout=1&vis=d3", mockAddress, mockFileLocation);

            using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
            {
                client.Diag.Net("1", IpfsVis.D3).Wait();
            }

            Assert.IsTrue(Equals(mockHttpMessageHandler.LastRequest.RequestUri, expectedRequestUri));
        }

        [TestMethod]
        public void ShouldBeAbleToCancelGetRequest()
        {
            try
            {
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
                mockResponse.Content = new StringContent(String.Empty);

                var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse, TimeSpan.FromSeconds(5));
                string mockAddress = "http://127.0.0.1:5001";

                var cts = new CancellationTokenSource();                
                using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
                {
                    var task = client.Commands(cts.Token);
                    cts.Cancel();
                    task.Wait();
                    throw new Exception("The operation was not cancelled");
                }
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("The operation has been canceled properly");                
            }
        }

        [TestMethod]
        public void ShouldBeAbleToCancelPostRequest()
        {
            try
            {
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
                mockResponse.Content = new StringContent(String.Empty);

                var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse, TimeSpan.FromSeconds(5));
                string mockAddress = "http://127.0.0.1:5001";

                var cts = new CancellationTokenSource();
                using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
                {
                    var task = client.ConfigCommand("test", "test", true, cts.Token);
                    //var task = client.Object.Put(new MerkleNode(), cts.Token);                    
                    cts.Cancel();                    
                    task.Wait();
                    throw new Exception("The operation was not cancelled");
                }
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("The operation has been canceled properly");
            }
        }

        [TestMethod]
        public void ShouldBeAbleToCancelIpfsObjectGetRequest()
        {
            try
            {
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
                mockResponse.Content = new StringContent(String.Empty);

                var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse, TimeSpan.FromSeconds(5));
                string mockAddress = "http://127.0.0.1:5001";

                var cts = new CancellationTokenSource();
                using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
                {
                    var task = client.Object.Get("somekey", IpfsEncoding.Base64, cts.Token);
                    cts.Cancel();
                    task.Wait();
                    throw new Exception("The operation was not cancelled");
                }
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("The operation has been canceled properly");
            }
        }

        [TestMethod]
        public void ShouldBeAbleToCancelIpfsObjectPostRequest()
        {
            try
            {
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
                mockResponse.Content = new StringContent(String.Empty);

                var mockHttpMessageHandler = new MockHttpMessageHandler(mockResponse, TimeSpan.FromSeconds(5));
                string mockAddress = "http://127.0.0.1:5001";

                var cts = new CancellationTokenSource();
                using (var client = new IpfsClient(new Uri(mockAddress), new HttpClient(mockHttpMessageHandler)))
                {                    
                    var task = client.Object.Put(new MerkleNode(), cts.Token);                    
                    cts.Cancel();
                    task.Wait();
                    throw new Exception("The operation was not cancelled");
                }
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("The operation has been canceled properly");
            }
        }
    }
}
