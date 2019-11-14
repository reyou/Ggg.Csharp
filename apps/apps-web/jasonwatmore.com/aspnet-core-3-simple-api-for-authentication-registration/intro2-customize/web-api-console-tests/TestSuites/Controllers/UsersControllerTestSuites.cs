using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models.Users;

namespace WebApi_Console_Tests.TestSuites.Controllers
{
    /// <summary>
    /// <see cref="UsersController"/>
    /// </summary>
    public class UsersControllerTestSuites : ITestSuite
    {
        public async Task RegisterUser(ApplicationEnvironment environment)
        {
            HttpClient httpClient = new HttpClient();
            RegisterModel registerModel = new RegisterModel()
            {
                FirstName = $"First{Guid.NewGuid().ToString().Substring(0, 5)}",
                LastName = $"Last{Guid.NewGuid().ToString().Substring(0, 5)}",
                Username = $"User{Guid.NewGuid().ToString().Substring(0, 5)}",
                Password = "www"
            };
            string stringContent = JsonConvert.SerializeObject(registerModel, Formatting.Indented);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("http://localhost:4000/users/register", httpContent);
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            TestUtilities.ConsoleWriteJson(new
            {
                httpResponseMessage.StatusCode,
                httpResponseMessage.ReasonPhrase,
                ResponseBody = readAsStringAsync
            });

        }

        public async Task AuthenticateUser(ApplicationEnvironment environment)
        {
            HttpClient httpClient = new HttpClient();
            AuthenticateModel authenticateModel = new AuthenticateModel()
            {
                Username = $"UpdatedUser82007",
                Password = "www"
            };
            string stringContent = JsonConvert.SerializeObject(authenticateModel, Formatting.Indented);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("http://localhost:4000/users/authenticate", httpContent);
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            TestUtilities.ConsoleWriteJson(new
            {
                httpResponseMessage.StatusCode,
                httpResponseMessage.ReasonPhrase,
                ResponseBody = readAsStringAsync
            });
        }

        public async Task GetAllUsers(ApplicationEnvironment environment)
        {
            string token = await GetToken("UpdatedUser82007", "www");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:4000/users");
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            TestUtilities.ConsoleWriteJson(new
            {
                httpResponseMessage.StatusCode,
                httpResponseMessage.ReasonPhrase,
                ResponseBody = readAsStringAsync
            });
        }

        private async Task<string> GetToken(string userName, string password)
        {
            HttpClient httpClient = new HttpClient();
            AuthenticateModel authenticateModel = new AuthenticateModel()
            {
                Username = userName,
                Password = password
            };
            string stringContent = JsonConvert.SerializeObject(authenticateModel, Formatting.Indented);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("http://localhost:4000/users/authenticate", httpContent);
            string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            dynamic authResponse = JsonConvert.DeserializeObject<dynamic>(readAsStringAsync);
            string token = authResponse.token;
            return token;
        }
    }
}
