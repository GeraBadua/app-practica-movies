using Microsoft.EntityFrameworkCore;
using backend_aspnet.Models;

namespace backend_aspnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        // El constructor recibe las opciones de configuración (como la conexión a la BD) y se las pasa a la clase base de Entity Framework.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Los DbSet representan las colecciones de nuestras entidades. 
        // A través de ellos haremos el CRUD (Select, Insert, Update, Delete).
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}