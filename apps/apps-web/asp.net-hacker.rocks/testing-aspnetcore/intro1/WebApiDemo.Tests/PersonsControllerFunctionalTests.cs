using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Controllers;
using WebApiDemo.Models;
using WebApiDemo.Services;
using Xunit;

namespace WebApiDemo.Tests
{
    public class PersonsControllerFunctionalTests
    {
        [Fact]
        public async Task Values_Get_All()
        {
            // Arrange
            PersonsController controller = new PersonsController(new PersonService());

            // Act
            IActionResult result = await controller.Get();

            // Assert
            OkObjectResult okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            IEnumerable<Person> persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Person>>().Subject;

            persons.Count().Should().Be(50);
        }

        [Fact]
        public async Task Values_Get_Specific()
        {
            // Arrange
            PersonsController controller = new PersonsController(new PersonService());

            // Act
            IActionResult result = await controller.Get(16);

            // Assert
            OkObjectResult okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            Person person = okResult.Value.Should().BeAssignableTo<Person>().Subject;
            person.Id.Should().Be(16);
        }

        [Fact]
        public async Task Persons_Add()
        {
            // Arrange
            PersonsController controller = new PersonsController(new PersonService());
            Person newPerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 50,
                Title = "FooBar",
                Email = "john.doe@foo.bar"
            };

            // Act
            IActionResult result = await controller.Post(newPerson);

            // Assert
            CreatedAtActionResult okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            Person person = okResult.Value.Should().BeAssignableTo<Person>().Subject;
            person.Id.Should().Be(51);
        }

        [Fact]
        public async Task Persons_Change()
        {
            // Arrange
            PersonService service = new PersonService();
            PersonsController controller = new PersonsController(service);
            Person newPerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 50,
                Title = "FooBar",
                Email = "john.doe@foo.bar"
            };

            // Act
            IActionResult result = await controller.Put(20, newPerson);

            // Assert
            NoContentResult okResult = result.Should().BeOfType<NoContentResult>().Subject;

            Person person = service.Get(20);
            person.Id.Should().Be(20);
            person.FirstName.Should().Be("John");
            person.LastName.Should().Be("Doe");
            person.Age.Should().Be(50);
            person.Title.Should().Be("FooBar");
            person.Email.Should().Be("john.doe@foo.bar");
        }

        [Fact]
        public async Task Persons_Delete()
        {
            // Arrange
            PersonService service = new PersonService();
            PersonsController controller = new PersonsController(service);

            // Act
            IActionResult result = await controller.Delete(20);

            // Assert
            NoContentResult okResult = result.Should().BeOfType<NoContentResult>().Subject;

            // should throw an eception, 
            // because the person with id==20 doesn't exist enymore
            //AssertionExtensions.ShouldThrow<InvalidOperationException>(
            //    () => service.Get(20));

        }
    }
}
