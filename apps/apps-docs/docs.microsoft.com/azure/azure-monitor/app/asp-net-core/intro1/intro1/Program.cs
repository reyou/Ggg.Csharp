using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace intro1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string instrumentationKey = File.ReadAllText("C:\\ao\\appKeys\\instrumentationKey1.txt");
            return WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights(instrumentationKey)
                .UseStartup<Startup>();
        }
    }
}
