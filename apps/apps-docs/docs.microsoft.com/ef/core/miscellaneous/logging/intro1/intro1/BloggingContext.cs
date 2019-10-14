using Microsoft.EntityFrameworkCore;

namespace intro1
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blog.db");
            // Warning: Do not create a new ILoggerFactory instance each time
            optionsBuilder.UseLoggerFactory(Startup.MyLoggerFactory); 
        }
    }
}