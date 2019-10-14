using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace intro1
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public BloggingContext(DbContextOptions<BloggingContext> optionsBuilder)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"Server=localhost;Database=Blogs;Trusted_Connection=True;",
                    options => options.EnableRetryOnFailure());
            optionsBuilder.UseLoggerFactory(Startup.MyLoggerFactory);
            optionsBuilder.EnableDetailedErrors();
           
        }
    }
}