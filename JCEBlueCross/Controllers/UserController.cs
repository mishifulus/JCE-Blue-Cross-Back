using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCEBlueCross.Context;
using JCEBlueCross.Models;

namespace JCEBlueCross.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
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
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'AppDbContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5 //BLOCK USER
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Status = 0;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }


        //CUSTOM SERVICES

        // PUT: api/QuestionsUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> QuestionsUser(int id, string question, string response)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            switch (question)
            {
                case "Mother":
                    user.MotherQuestion = response;
                    break;
                case "Chilhood":
                    user.ChilhoodQuestion = response;
                    break;
                case "City":
                    user.CityQuestion = response;
                    break;
                case "Car":
                    user.CarQuestion = response;
                    break;
                case "University":
                    user.UniversityQuestion = response;
                    break;
                case "Sport":
                    user.SportQuestion = response;
                    break;
                case "Boss":
                    user.BossQuestion = response;
                    break;
                case "Band":
                    user.BandQuestion = response;
                    break;
                default:
                    break;
            }


            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT: api/ModifyUsername/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyUsername(int id, string username)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            user.Username = username;

            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT: api/ModifyPassword/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPassword(int id, string password)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            user.Password = password;

            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // GET: api/Login/5
        [HttpGet("Login")]
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return NotFound("User not found or incorrect password");
            }

            return user;
        }


    }
}
