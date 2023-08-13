using Microsoft.EntityFrameworkCore;

namespace DotNet_EFCore.Models
{
    public class MyContext: DbContext
    {

        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Monster> Monster { get; set; }

    }
}
