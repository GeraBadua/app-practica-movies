using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_aspnet.Data;
using backend_aspnet.Models;

namespace backend_aspnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/movies (Leer todas con su Director)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            // Usamos .Include para traer los datos del director asociado (Equivalente a un SQL JOIN)
            return await _context.Movies
                .Include(m => m.Director)
                .ToListAsync();
        }

        // GET: api/movies/5 (Leer una sola con su Director)
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound(new { message = $"Película con ID {id} no encontrada." });
            }

            return movie;
        }

        // POST: api/movies (Crear una película)
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            // VALIDACIÓN CORREGIDA: Usamos solo DirectorId que es el nombre real en la clase C#
            var directorExists = await _context.Directors.AnyAsync(d => d.Id == movie.DirectorId);

            if (!directorExists)
            {
                return BadRequest(new { message = $"El Director con ID {movie.DirectorId} no existe. No se puede crear la película." });
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/movies/5 (Actualizar)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest(new { message = "El ID de la URL no coincide con el de la película." });
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound(new { message = "La película no existe." });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/movies/5 (Eliminar)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound(new { message = "La película no existe." });
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Película eliminada correctamente." });
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}