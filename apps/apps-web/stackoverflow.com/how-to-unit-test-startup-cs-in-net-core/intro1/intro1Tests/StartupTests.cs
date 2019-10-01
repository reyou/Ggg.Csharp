using intro1;
using intro1.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace intro1Tests
{
    /// <summary>
    /// <see cref="Startup"/>
    /// </summary>
    [TestClass()]
    public class StartupTests
    {
        [TestMethod]
        public void ConfigureServices_RegistersControllersCorrectly()
        {
            //  Arrange
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup(configurationStub.Object);

            //  Act
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<TestController>();
            services.AddTransient<Test2Controller>();
            //  Assert
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            TestController controller = serviceProvider.GetService<TestController>();
            Test2Controller test2Controller = serviceProvider.GetService<Test2Controller>();
            Assert.IsNotNull(controller);
            Assert.IsNotNull(test2Controller);
        }


        [TestMethod]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            //  Arrange

            //  Setting up the stuff required for Configuration.GetConnectionString("DefaultConnection")
            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
            configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

            IServiceCollection services = new ServiceCollection();
            Startup target = new Startup(configurationStub.Object);

            //  Act

            target.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<TestController>();
            services.AddTransient<Test2Controller>();

            //  Assert

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            TestController controller = serviceProvider.GetService<TestController>();
            Test2Controller test2Controller = serviceProvider.GetService<Test2Controller>();
            Assert.IsNotNull(controller);
            Assert.IsNotNull(test2Controller);
        } 
        
       
    }
}