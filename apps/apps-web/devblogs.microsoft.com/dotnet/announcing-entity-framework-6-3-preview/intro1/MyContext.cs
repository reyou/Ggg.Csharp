using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace intro1
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MyContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}