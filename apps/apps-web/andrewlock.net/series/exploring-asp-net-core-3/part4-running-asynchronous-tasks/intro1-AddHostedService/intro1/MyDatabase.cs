using System;
using System.Threading.Tasks;

namespace intro1
{
    public class MyDatabase
    {
        public async Task MigrateAsync()
        {
            Console.WriteLine($"Migrate started on {DateTime.Now:F}.");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine($"Migrate completed on {DateTime.Now:F}.");
        }
    }
}