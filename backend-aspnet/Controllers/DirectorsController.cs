using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_aspnet.Data;
using backend_aspnet.Models;

namespace backend_aspnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors()
        {
            return await _context.Directors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);

            if (director == null)
                return NotFound(new { message = $"Director con ID {id} no encontrado." });

            return director;
        }

        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDirector), new { id = director.Id }, director);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, Director director)
        {
            if (id != director.Id)
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del cuerpo." });

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                    return NotFound(new { message = "El director a actualizar ya no existe." });
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return NotFound(new { message = "El director a eliminar no existe." });

            try
            {
                _context.Directors.Remove(director);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException != null && ex.InnerException.Message.Contains("foreign key constraint"))
            {
                return Conflict(new { message = "No se puede eliminar el director porque tiene peliculas asociadas. Elimine primero las peliculas del director." });
            }

            return Ok(new { message = "Director eliminado correctamente." });
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.Id == id);
        }
    }
}
