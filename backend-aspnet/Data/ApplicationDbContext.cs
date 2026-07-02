using Microsoft.EntityFrameworkCore;
using backend_aspnet.Models;

namespace backend_aspnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
