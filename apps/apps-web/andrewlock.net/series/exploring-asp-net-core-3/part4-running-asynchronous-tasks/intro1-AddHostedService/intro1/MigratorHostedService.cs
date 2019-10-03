using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace intro1
{
    public class MigratorHostedService : IHostedService
    {
        // We need to inject the IServiceProvider so we can create 
        // the scoped service, MyDbContext
        private readonly IServiceProvider _serviceProvider;
        public MigratorHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"MigratorHostedService.StartAsync started on {DateTime.Now:F}.");
            // Create a new scope to retrieve scoped services
            using IServiceScope scope = _serviceProvider.CreateScope();
            // Get the DbContext instance
            MyDbContext myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            //Do the migration asynchronously
            await myDbContext.Database.MigrateAsync();
            Console.WriteLine($"MigratorHostedService.StartAsync completed on {DateTime.Now:F}.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"MigratorHostedService.StopAsync started on {DateTime.Now:F}.");
            return Task.CompletedTask;
        }
    }
}