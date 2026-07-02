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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies
                .Include(m => m.Director)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound(new { message = $"Pelicula con ID {id} no encontrada." });

            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            var directorExists = await _context.Directors.AnyAsync(d => d.Id == movie.DirectorId);

            if (!directorExists)
                return BadRequest(new { message = $"El Director con ID {movie.DirectorId} no existe." });

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            await _context.Entry(movie).Reference(m => m.Director).LoadAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
                return BadRequest(new { message = "El ID de la URL no coincide con el de la pelicula." });

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                    return NotFound(new { message = "La pelicula no existe." });
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound(new { message = "La pelicula no existe." });

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Pelicula eliminada correctamente." });
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
