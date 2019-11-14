using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using WebApi.Entities;
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


            IConfigurationProvider configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterModel, User>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UpdateModel, User>();
            });
            IMapper mapper = new Mapper(configurationProvider);

            IOptions<AppSettings> appSettings = new AppSettingsOptions();
            _usersController = new UsersController(userService, mapper, appSettings);
        }


        [TestMethod()]
        public void AuthenticateTest()
        {
            AuthenticateModel model = new AuthenticateModel()
            {
                Username = $"User69163",
                Password = "www"
            };
            // Act
            IActionResult actionResult = _usersController.Authenticate(model);
            Console.WriteLine(JsonConvert.SerializeObject(actionResult, Formatting.Indented));
        }

        [TestMethod()]
        public void RegisterTest()
        {
            RegisterModel model = new RegisterModel()
            {
                FirstName = $"First{Guid.NewGuid().ToString().Substring(0, 5)}",
                LastName = $"Last{Guid.NewGuid().ToString().Substring(0, 5)}",
                Username = $"User{Guid.NewGuid().ToString().Substring(0, 5)}",
                Password = "www"
            };
            // Act
            IActionResult actionResult = _usersController.Register(model);
            Console.WriteLine(JsonConvert.SerializeObject(actionResult, Formatting.Indented));
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IActionResult actionResult = _usersController.GetAll();
            Console.WriteLine(JsonConvert.SerializeObject(actionResult, Formatting.Indented));
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            IActionResult actionResult = _usersController.GetById(2);
            Console.WriteLine(JsonConvert.SerializeObject(actionResult, Formatting.Indented));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            UpdateModel model = new UpdateModel()
            {
                FirstName = $"UpdatedFirst{Guid.NewGuid().ToString().Substring(0, 5)}",
                LastName = $"UpdatedLast{Guid.NewGuid().ToString().Substring(0, 5)}",
                Username = $"UpdatedUser{Guid.NewGuid().ToString().Substring(0, 5)}",
                Password = "www"
            };
            IActionResult actionResult = _usersController.Update(2, model);
            Console.WriteLine(JsonConvert.SerializeObject(actionResult, Formatting.Indented));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IActionResult actionResult = _usersController.GetAll();
            IList<UserModel> userModels = (IList<UserModel>)((ObjectResult)actionResult).Value;
            UserModel userModel = userModels.Last();
            IActionResult delete = _usersController.Delete(userModel.Id);
            Console.WriteLine(JsonConvert.SerializeObject(delete, Formatting.Indented));
        }
    }
}