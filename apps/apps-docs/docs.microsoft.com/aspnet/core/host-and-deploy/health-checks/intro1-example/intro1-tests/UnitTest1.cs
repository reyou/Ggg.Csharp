using System;
using intro1;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace intro1_tests
{
    /// <summary>
    /// <see cref="Startup"/>
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public UnitTest1()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/");
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }

        [TestMethod]
        public async Task HealthTest()
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/health");
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(readAsStringAsync);
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.AreEqual("text/plain", httpResponseMessage.Content.Headers.ContentType.MediaType);
            Assert.AreEqual("Healthy", readAsStringAsync);
        }  
        
        [TestMethod]
        public async Task ReadyTest()
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // wait for 3 seconds before check
            int _delaySeconds = 3;
            await Task.Delay(_delaySeconds * 1000);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/health/ready");
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(readAsStringAsync);
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.AreEqual("text/plain", httpResponseMessage.Content.Headers.ContentType.MediaType);
            Assert.AreEqual("Healthy", readAsStringAsync);
        }
    }
}
