using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace intro1
{
    /// <summary>
    /// https://andrewlock.net/exploring-the-new-project-file-program-and-the-generic-host-in-asp-net-core-3/
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /* Host.CreateDefaultBuilder().
        This configures the 
        app configuration, 
        logging, 
        and dependency injection container. */
        /* IHostBuilder.ConfigureWebHostDefaults().
         This adds everything else needed for a typical ASP.NET Core application, 
         such as configuring Kestrel and using a 
         Startup.cs to configure your DI container and 
         middleware pipeline. */
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder defaultBuilder = Host.CreateDefaultBuilder(args);
            IHostBuilder hostBuilder = defaultBuilder.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
            return hostBuilder;
        }

        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            HostBuilder builder = new HostBuilder();

            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.ConfigureHostConfiguration(config =>
            {
                // Uses DOTNET_ environment variables and command line args
            });

            builder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // JSON files, User secrets, environment variables and command line arguments
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    // Adds loggers for console, debug, event source, and EventLog (Windows only)
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    // Configures DI provider validation
                });

            return builder;
        }
    }
}
