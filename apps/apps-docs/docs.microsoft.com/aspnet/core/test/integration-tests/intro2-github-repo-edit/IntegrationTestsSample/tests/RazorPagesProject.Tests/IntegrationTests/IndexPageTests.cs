using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RazorPagesProject.Data;
using RazorPagesProject.Services;
using RazorPagesProject.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RazorPagesProject.Tests.IntegrationTests
{
    #region snippet1
    public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public IndexPageTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        #endregion

        #region snippet2
        [Fact]
        public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            HttpResponseMessage defaultPage = await _client.GetAsync("/");
            IHtmlDocument content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            HttpResponseMessage response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.OriginalString);
        }
        #endregion

        #region snippet3
        [Fact]
        public async Task Post_DeleteMessageHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            HttpClient client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        ServiceProvider serviceProvider = services.BuildServiceProvider();

                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            IServiceProvider scopedServices = scope.ServiceProvider;
                            ApplicationDbContext db = scopedServices
                                .GetRequiredService<ApplicationDbContext>();
                            ILogger<IndexPageTests> logger = scopedServices
                                .GetRequiredService<ILogger<IndexPageTests>>();

                            try
                            {
                                Utilities.ReinitializeDbForTests(db);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred seeding " +
                                    "the database with test messages. Error: {Message}",
                                    ex.Message);
                            }
                        }
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            HttpResponseMessage defaultPage = await client.GetAsync("/");
            IHtmlDocument content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            HttpResponseMessage response = await client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
                (IHtmlButtonElement)content.QuerySelector("form[id='messages']")
                    .QuerySelector("div[class='panel-body']")
                    .QuerySelector("button"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.OriginalString);
        }
        #endregion

        [Fact]
        public async Task Post_AddMessageHandler_ReturnsSuccess_WhenMissingMessageText()
        {
            // Arrange
            HttpResponseMessage defaultPage = await _client.GetAsync("/");
            IHtmlDocument content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            string messageText = string.Empty;

            // Act
            HttpResponseMessage response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='addMessage']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='addMessageBtn']"),
                new Dictionary<string, string>
                {
                    ["Message.Text"] = messageText
                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            // A ModelState failure returns to Page (200-OK) and doesn't redirect.
            response.EnsureSuccessStatusCode();
            Assert.Null(response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Post_AddMessageHandler_ReturnsSuccess_WhenMessageTextTooLong()
        {
            // Arrange
            HttpResponseMessage defaultPage = await _client.GetAsync("/");
            IHtmlDocument content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            string messageText = new string('X', 201);

            // Act
            HttpResponseMessage response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='addMessage']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='addMessageBtn']"),
                new Dictionary<string, string>
                {
                    ["Message.Text"] = messageText
                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            // A ModelState failure returns to Page (200-OK) and doesn't redirect.
            response.EnsureSuccessStatusCode();
            Assert.Null(response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Post_AnalyzeMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            HttpResponseMessage defaultPage = await _client.GetAsync("/");
            IHtmlDocument content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            HttpResponseMessage response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='analyze']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='analyzeBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.OriginalString);
        }

        #region snippet4
        // Quote ©1975 BBC: The Doctor (Tom Baker); Pyramids of Mars
        // https://www.bbc.co.uk/programmes/p00pys55
        public class TestQuoteService : IQuoteService
        {
            public Task<string> GenerateQuote()
            {
                return Task.FromResult<string>(
                    "Something's interfering with time, Mr. Scarman, " +
                    "and time is my business.");
            }
        }
        #endregion

        #region snippet5
        [Fact]
        public async Task Get_QuoteService_ProvidesQuoteInPage()
        {
            // Arrange
            HttpClient client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped<IQuoteService, TestQuoteService>();
                    });
                })
                .CreateClient();

            //Act
            HttpResponseMessage defaultPage = await client.GetAsync("/");
            IHtmlDocument content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            IElement quoteElement = content.QuerySelector("#quote");

            // Assert
            Assert.Equal("Something's interfering with time, Mr. Scarman, " +
                "and time is my business.", quoteElement.Attributes["value"].Value);
        }
        #endregion
    }
}
