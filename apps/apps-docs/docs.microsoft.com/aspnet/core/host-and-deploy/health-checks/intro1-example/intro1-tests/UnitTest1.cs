using intro1;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace intro1_tests
{
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
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/health");
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            Assert.AreEqual("Healthy", readAsStringAsync);
        }
    }
}
