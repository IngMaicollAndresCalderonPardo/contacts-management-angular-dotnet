using ContactosApp.Server.Dtos;
using ContactosApp.Server.Models;
using ContactosApp.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApp.Server.Controllers
{
    [ApiController]  
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repo;

        public ContactsController (IContactRepository repo)
        {
            _repo = repo;
        }

        // GET /api/contacts?search=Juan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAll([FromQuery] string? search)
        {
            var items = await _repo.GetAllAsync(search);
            var dtos = items.Select(c => new ContactDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                BirthDate = c.BirthDate,
                Address = c.Address,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            });

            return Ok(dtos);
        }

        // GET /api/contacts/{id}  (para el CreatedAtAction)
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            var found = await _repo.GetByIdAsync(id);
            if (found is null) return NotFound();

            return Ok(new ContactDto
            {
                Id = found.Id,
                Name = found.Name,
                Email = found.Email,
                Phone = found.Phone,
                BirthDate = found.BirthDate,
                Address = found.Address
            });

        }

        // POST /api/contacts
        [HttpPost]
        public async Task<ActionResult<ContactDto>> Create([FromBody] ContactCreateDto input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            // Validación de correo duplicado
            var exists = await _repo.ExistsByEmailAsync(input.Email!);
            if (exists)
            {
                return Conflict(new { message = "Ya existe un contacto con este correo." });
            }

            var entity = new Contact
            {
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                BirthDate = input.BirthDate,
                Address = input.Address
            };

            var created = await _repo.AddAsync(entity);

            var dto = new ContactDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                Phone = created.Phone,
                BirthDate = created.BirthDate,
                Address = created.Address
            };

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // PUT /api/contacts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactCreateDto input)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Evitar duplicar email
            var allContacts = await _repo.GetAllAsync();
            if (allContacts.Any(c => c.Email == input.Email && c.Id != id))
                return BadRequest(new { message = "El correo ya existe." });

            existing.Name = input.Name;
            existing.Email = input.Email;
            existing.Phone = input.Phone;
            existing.BirthDate = input.BirthDate;
            existing.Address = input.Address;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);

            return NoContent();
        }


        // DELETE /api/contacts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

    }
}
