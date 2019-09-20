using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
            // https://github.com/MicrosoftDocs/azure-docs/issues/27395
            return WebHost.CreateDefaultBuilder(args)
                // .UseApplicationInsights()
                .UseStartup<Startup>();
        }
    }
}
