using Final01Demo.Models;
using Microsoft.EntityFrameworkCore;
namespace Final01Demo.Data
{
    public class FinalDbContext : DbContext
    {
        public FinalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CanHo> canHos { get; set; }
        public DbSet<ToaNha> toaNha { get; set; }
    }
}
