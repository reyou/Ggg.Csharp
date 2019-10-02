using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace intro1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder defaultBuilder = Host.CreateDefaultBuilder(args);
            IHostBuilder configureWebHostDefaults = defaultBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            configureWebHostDefaults.UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                    options.ValidateOnBuild = true;
                });
            return configureWebHostDefaults;
        }
    }
}
