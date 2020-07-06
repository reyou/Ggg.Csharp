using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApplicationJsonMergePatch;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public UnitTest1()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>();
        }

        [Fact]
        public async Task Test1()
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/api/values");
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }

        public class PatchRequestCollection
        {
            public List<PatchRequest> PatchRequests { get; set; }

            public PatchRequestCollection()
            {
                PatchRequests = new List<PatchRequest>();
            }

            public void Add(PatchRequest pathRequest)
            {
                this.PatchRequests.Add(pathRequest);
            }
        }

        public class PatchRequest
        {
            [JsonProperty("op")]
            public string Op { get; set; }

            [JsonProperty("path")]
            public string Path { get; set; }

            [JsonProperty("value")]
            public object Value { get; set; }

            public PatchRequest(string op, string path, object value = null)
            {
                Op = op;
                Path = path;
                Value = value;
            }
        }

        [Fact]
        public async Task PatchTest1()
        {
            HttpClient httpClient = _webApplicationFactory.CreateClient();

            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = null,
                BirthDate = DateTimeOffset.Now
            };
        
            PatchRequest patchRequest1 = new PatchRequest("replace", "/firstName", user.FirstName);
            PatchRequest patchRequest2 = new PatchRequest("add", "/birthDate", user.BirthDate);
            PatchRequest patchRequest3 = new PatchRequest("remove", "/lastName");
            
            PatchRequestCollection collection = new PatchRequestCollection();
            collection.Add(patchRequest1);
            collection.Add(patchRequest2);
            collection.Add(patchRequest3);
         
            string contentString = JsonConvert.SerializeObject(collection.PatchRequests, Formatting.Indented);
            HttpContent content = new StringContent(contentString, Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage httpResponseMessage = await httpClient.PatchAsync("/api/values/user", content);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }
    }
}
