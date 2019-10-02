using System;
using System.Collections.Generic;
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
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

       
    }
}
