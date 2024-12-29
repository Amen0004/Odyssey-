using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdysseyEventPlanner.Data;
using OdysseyEventPlanner.Models;

namespace OdysseyEventPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly OdysseyContext _context;

        public UsersController(OdysseyContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }
  // POST: api/Users/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

    if (user == null)
    {
        return Unauthorized("Invalid username or password");
    }

    // Generate a token for the authenticated user
    var token = GenerateToken(user);

    // Return the token as a JSON response
    return Ok(new { token });
        }

        private string GenerateToken(Users user)
{
   
    // This is just a placeholder example
    return $"Bearer {user.Id}_{DateTime.Now.Ticks}";
}

        // POST: api/Users/SignUp
        [HttpPost("signup")]
        public async Task<ActionResult<Users>> SignUp([FromBody] SignUpDto signUpDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Username == signUpDto.Username);
            if (userExists)
            {
                return BadRequest("Username already exists");
            }

            var newUser = new Users
            {
                Name = signUpDto.Name,
                Username = signUpDto.Username,
                Password = signUpDto.Password, // Normally you'd hash the password here
                Email = signUpDto.Email
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = newUser.Id }, newUser);
        }
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
 // DTOs (Data Transfer Objects) for Login and Sign Up
    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class SignUpDto
    {
        public required string Name { get; set; }  // Added Name field
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }