using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesProject.Services;
using RazorPagesProject.Tests.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RazorPagesProject.Tests.IntegrationTests
{
    #region snippet1
    /// <summary>
    /// WebApplicationFactory: Factory for bootstrapping an application in memory for functional end to end tests.
    /// </summary>
    public class BasicTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BasicTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/About")]
        [InlineData("/Privacy")]
        [InlineData("/Contact")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        #endregion

        #region snippet2
        [Fact]
        public async Task Get_SecurePageRequiresAnAuthenticatedUser()
        {
            // Arrange
            HttpClient client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            // Act
            HttpResponseMessage response = await client.GetAsync("/SecurePage");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/Identity/Account/Login",
                response.Headers.Location.OriginalString);
        }
        #endregion

        #region snippet3
        [Fact]
        public async Task CanGetAGithubUser()
        {
            // Arrange
            void ConfigureTestServices(IServiceCollection services) =>
                services.AddSingleton<IGithubClient>(new TestGithubClient());
            HttpClient client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(ConfigureTestServices))
                .CreateClient();

            // Act
            HttpResponseMessage profile = await client.GetAsync("/GithubProfile");
            Assert.Equal(HttpStatusCode.OK, profile.StatusCode);
            IHtmlDocument profileHtml = await HtmlHelpers.GetDocumentAsync(profile);

            HttpResponseMessage profileWithUserName = await client.SendAsync(
                (IHtmlFormElement)profileHtml.QuerySelector("#user-profile"),
                new Dictionary<string, string> { ["Input_UserName"] = "user" });

            // Assert
            Assert.Equal(HttpStatusCode.OK, profileWithUserName.StatusCode);
            IHtmlDocument profileWithUserHtml =
                await HtmlHelpers.GetDocumentAsync(profileWithUserName);
            IElement userLogin = profileWithUserHtml.QuerySelector("#user-login");
            Assert.Equal("user", userLogin.TextContent);
        }

        public class TestGithubClient : IGithubClient
        {
            public Task<GithubUser> GetUserAsync(string userName)
            {
                if (userName == "user")
                {
                    return Task.FromResult(
                        new GithubUser
                        {
                            Login = "user",
                            Company = "Contoso Blockchain",
                            Name = "John Doe"
                        });
                }
                else
                {
                    return Task.FromResult<GithubUser>(null);
                }
            }
        }
        #endregion
    }
}
