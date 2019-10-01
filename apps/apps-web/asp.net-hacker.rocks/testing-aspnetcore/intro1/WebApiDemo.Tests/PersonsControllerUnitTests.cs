using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiDemo.Controllers;
using WebApiDemo.Interfaces;
using WebApiDemo.Models;
using Xunit;

namespace WebApiDemo.Tests
{
    public class PersonsControllerUnitTests
    {
        [Fact]
        public async Task Persons_Get_From_Moq()
        {
            // Arrange
            Mock<IPersonService> serviceMock = new Mock<IPersonService>();
            serviceMock.Setup(x => x.GetAll()).Returns(() => new List<Person>
            {
                new Person{Id=1, FirstName="Foo", LastName="Bar"},
                new Person{Id=2, FirstName="John", LastName="Doe"},
                new Person{Id=3, FirstName="Juergen", LastName="Gutsch"},
            });
            PersonsController controller = new PersonsController(serviceMock.Object);

            // Act
            IActionResult result = await controller.Get();

            // Assert
            OkObjectResult okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            IEnumerable<Person> persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Person>>().Subject;

            persons.Count().Should().Be(3);
        }

    }
}
