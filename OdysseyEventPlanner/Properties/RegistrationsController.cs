using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdysseyEventPlanner.Data;
using OdysseyEventPlanner.Models;

namespace OdysseyEventPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly OdysseyContext _context;

        public RegistrationsController(OdysseyContext context)
        {
            _context = context;
        }

        // POST: api/registration/fromscratch
        [HttpPost("fromscratch")]
        public async Task<IActionResult> CreateFromScratchRegistration([FromBody] Registrations registration)
        {
            if (registration == null)
            {
                return BadRequest("Registration data is null.");
            }

            // Validate required fields for from scratch registration
            if (string.IsNullOrEmpty(registration.Username))
            {
                return BadRequest("Username is required for from scratch registration.");
            }
            if (string.IsNullOrEmpty(registration.EventType))
            {
                return BadRequest("EventType is required for from scratch registration.");
            }
            if (registration.EventDate == default)
            {
                return BadRequest("EventDate is required for from scratch registration.");
            }
            if (registration.NumberOfGuests == null || registration.NumberOfGuests <= 0)
            {
                return BadRequest("NumberOfGuests must be greater than 0 for from scratch registration.");
            }
            if (string.IsNullOrEmpty(registration.Location))
            {
                return BadRequest("Location is required for from scratch registration.");
            }
            if (string.IsNullOrEmpty(registration.Decor))
            {
                return BadRequest("Decor is required for from scratch registration.");
            }
            if (string.IsNullOrEmpty(registration.Transportation))
            {
                return BadRequest("Transportation is required for from scratch registration.");
            }
            if (string.IsNullOrEmpty(registration.SpecialRequest))
            {
                return BadRequest("SpecialRequest is required for from scratch registration.");
            }
            if (string.IsNullOrEmpty(registration.Accommodation))
            {
                return BadRequest("Accommodation is required for from scratch registration.");
            }

            // Add the registration to the database
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistration), new { id = registration.RegistrationID }, registration);
        }

        // POST: api/registration/package
        [HttpPost("package")]
        public async Task<IActionResult> CreatePackageRegistration([FromBody] Registrations registration)
        {
            if (registration == null)
            {
                return BadRequest("Registration data is null.");
            }

            // Validate required fields for package registration
            if (string.IsNullOrEmpty(registration.Username))
            {
                return BadRequest("Username is required for package registration.");
            }
            if (string.IsNullOrEmpty(registration.EventName))
            {
                return BadRequest("EventName is required for package registration.");
            }
            if (registration.EventDate == default)
            {
                return BadRequest("EventDate is required for package registration.");
            }
            if (string.IsNullOrEmpty(registration.Package))
            {
                return BadRequest("Package is required for package registration.");
            }
            if (string.IsNullOrEmpty(registration.Accommodation))
            {
                return BadRequest("Accommodation is required for package registration.");
            }

            // Set other fields not applicable for package registration to null
            registration.EventType = null;
            registration.NumberOfGuests = null;
            registration.Location = null;
            registration.Decor = null;
            registration.Transportation = null;
            registration.SpecialRequest = null;

            // Add the registration to the database
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistration), new { id = registration.RegistrationID }, registration);
        }

        // GET: api/registration/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Registrations>> GetRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // GET: api/registration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registrations>>> GetAllRegistrations()
        {
            return await _context.Registrations.ToListAsync();
        }

        // PUT: api/registration/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistration(int id, [FromBody] Registrations registration)
        {
            if (id != registration.RegistrationID)
            {
                return BadRequest("Registration ID mismatch.");
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/registration/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationID == id);
        }
    }
}
