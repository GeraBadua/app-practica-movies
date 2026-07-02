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

        // Inyección de Dependencias: .NET nos pasa automáticamente el DbContext configurado
        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/directors (Leer todos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors()
        {
            return await _context.Directors.ToListAsync();
        }

        // 2. GET: api/directors/5 (Leer uno solo)
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);

            if (director == null)
            {
                return NotFound(new { message = $"Director con ID {id} no encontrado." });
            }

            return director;
        }

        // 3. POST: api/directors (Crear)
        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            // Retorna un código 201 Created y la URL donde se puede consultar el nuevo objeto
            return CreatedAtAction(nameof(GetDirector), new { id = director.Id }, director);
        }

        // 4. PUT: api/directors/5 (Actualizar)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, Director director)
        {
            if (id != director.Id)
            {
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del cuerpo." });
            }

            // Le dice a EF que este objeto fue modificado para que actualice los campos
            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return NotFound(new { message = "El director a actualizar ya no existe." });
                }
                throw;
            }

            return NoContent(); // Código 204: Actualización exitosa, no hay contenido que devolver
        }

        // 5. DELETE: api/directors/5 (Eliminar)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound(new { message = "El director a eliminar no existe." });
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Director eliminado correctamente." });
        }

        // Método auxiliar privado para verificar existencia
        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.Id == id);
        }
    }
}