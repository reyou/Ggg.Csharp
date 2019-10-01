using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using WebApiDemo.Models;
using Xunit;

namespace WebApiDemo.Tests
{
    public class PersonsControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public PersonsControllerIntegrationTests()
        {
            // Arrange
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task Persons_Get_All()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/Persons");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            // Assert
            IEnumerable<Person> persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(responseString);
            persons.Count().Should().Be(50);
        }

        [Fact]
        public async Task Persons_Get_Specific()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/Persons/16");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Person person = JsonConvert.DeserializeObject<Person>(responseString);
            person.Id.Should().Be(16);
        }

        [Fact]
        public async Task Persons_Post_Specific()
        {
            // Arrange
            Person personToAdd = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 50,
                Title = "FooBar",
                Phone = "001 123 1234567",
                Email = "john.doe@foo.bar"
            };
            string content = JsonConvert.SerializeObject(personToAdd);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/api/Persons", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            Person person = JsonConvert.DeserializeObject<Person>(responseString);
            person.Id.Should().Be(51);
        }

        [Fact]
        public async Task Persons_Post_Specific_Invalid()
        {
            // Arrange
            Person personToAdd = new Person { FirstName = "John" };
            string content = JsonConvert.SerializeObject(personToAdd);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/api/Persons", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            string responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("The Email field is required")
                .And.Contain("The LastName field is required")
                .And.Contain("The Phone field is required");
        }

        [Fact]
        public async Task Persons_Put_Specific()
        {
            // Arrange
            Person personToChange = new Person
            {
                Id = 16,
                FirstName = "John",
                LastName = "Doe",
                Age = 50,
                Title = "FooBar",
                Phone = "001 123 1234567",
                Email = "john.doe@foo.bar"
            };
            string content = JsonConvert.SerializeObject(personToChange);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PutAsync("/api/Persons/16", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }

        [Fact]
        public async Task Persons_Put_Specific_Invalid()
        {
            // Arrange
            Person personToChange = new Person { FirstName = "John" };
            string content = JsonConvert.SerializeObject(personToChange);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PutAsync("/api/Persons/16", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            string responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("The Email field is required")
                .And.Contain("The LastName field is required")
                .And.Contain("The Phone field is required");
        }

        [Fact]
        public async Task Persons_Delete_Specific()
        {
            // Arrange

            // Act
            HttpResponseMessage response = await _client.DeleteAsync("/api/Persons/16");

            // Assert
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }
    }
}
