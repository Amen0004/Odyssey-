using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdysseyEventPlanner.Data;
using OdysseyEventPlanner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdysseyEventPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormSubmissionsController : ControllerBase
    {
        private readonly OdysseyContext _context;

        public ContactFormSubmissionsController(OdysseyContext context)
        {
            _context = context;
        }

        // GET: api/ContactFormSubmissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactFormSubmissions>>> GetContactFormSubmissions()
        {
            return await _context.ContactFormSubmissions.ToListAsync();
        }

        // GET: api/ContactFormSubmissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactFormSubmissions>> GetContactFormSubmission(int id)
        {
            var submission = await _context.ContactFormSubmissions.FindAsync(id);

            if (submission == null)
            {
                return NotFound();
            }

            return submission;
        }

        // POST: api/ContactFormSubmissions
        [HttpPost]
        public async Task<ActionResult<ContactFormSubmissions>> PostContactFormSubmission(ContactFormSubmissions submission)
        {
            _context.ContactFormSubmissions.Add(submission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactFormSubmission", new { id = submission.Id }, submission);
        }

        // PUT: api/ContactFormSubmissions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactFormSubmission(int id, ContactFormSubmissions submission)
        {
            if (id != submission.Id)
            {
                return BadRequest();
            }

            _context.Entry(submission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactFormSubmissionExists(id))
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

        // DELETE: api/ContactFormSubmissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactFormSubmission(int id)
        {
            var submission = await _context.ContactFormSubmissions.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }

            _context.ContactFormSubmissions.Remove(submission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactFormSubmissionExists(int id)
        {
            return _context.ContactFormSubmissions.Any(e => e.Id == id);
        }
    }
}
