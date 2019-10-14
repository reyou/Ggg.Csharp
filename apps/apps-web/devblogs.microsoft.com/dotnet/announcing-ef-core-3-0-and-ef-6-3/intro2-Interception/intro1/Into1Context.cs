using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace intro1
{
    public class Into1Context : DbContext
    {
        public Into1Context(DbContextOptions<Into1Context> options)
            : base(options)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}