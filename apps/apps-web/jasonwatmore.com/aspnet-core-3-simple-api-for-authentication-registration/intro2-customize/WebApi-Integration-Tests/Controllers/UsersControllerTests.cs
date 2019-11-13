using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using WebApi.Controllers;
using WebApi.Helpers;
using WebApi.Models.Users;
using WebApi.Services;

namespace WebApi_Integration_Tests.Controllers
{
    /// <summary>
    /// <see cref="UsersController"/>
    /// </summary>
    [TestClass()]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        public UsersControllerTests()
        {
            DbContextOptionsBuilder<DataContext> optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=DotNetCore3CustomAuth;Trusted_Connection=True;");
            DbContextOptions<DataContext> options = optionsBuilder.Options;
            DataContext context = new DataContext(options);
            IUserService userService = new UserService(context);

            IConfigurationProvider configurationProvider = new MapperConfiguration(ConfigureMapper);
            IMapper mapper = new Mapper(configurationProvider);

            IOptions<AppSettings> appSettings = new AppSettingsOptions();
            _usersController = new UsersController(userService, mapper, appSettings);
        }

        private void ConfigureMapper(IMapperConfigurationExpression obj)
        {
            Console.WriteLine(obj);
        }

        [TestMethod()]
        public void UsersControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            AuthenticateModel model = new AuthenticateModel();
            IActionResult actionResult = _usersController.Authenticate(model);
            Console.WriteLine(JsonConvert.SerializeObject(actionResult, Formatting.Indented));
        }

        [TestMethod()]
        public void RegisterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }

    public class AppSettingsOptions : IOptions<AppSettings>
    {
        public AppSettings Value { get; }
    }
}