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
    public class ErrorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ErrorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Error
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Error>>> GetErrors()
        {
          if (_context.Errors == null)
          {
              return NotFound();
          }
            return await _context.Errors.Include(p => p.RegisteringUser).ToListAsync();
        }

        // GET: api/Error/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Error>> GetError(int id)
        {
          if (_context.Errors == null)
          {
              return NotFound();
          }
            var error = await _context.Errors.Include(p => p.RegisteringUser).FirstOrDefaultAsync(p => p.ErrorId == id);

            if (error == null)
            {
                return NotFound();
            }

            return error;
        }

        // PUT: api/Error/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutError(int id, Error error)
        {
            if (id != error.ErrorId)
            {
                return BadRequest();
            }

            _context.Entry(error).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorExists(id))
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

        // POST: api/Error
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Error>> PostError(Error error)
        {
          if (_context.Errors == null)
          {
              return Problem("Entity set 'AppDbContext.Errors'  is null.");
          }

          if (error.RegisteringUser != null)
            {
                var user = await _context.Users.FindAsync(error.RegisteringUser.UserId);
                if (user == null)
                {
                    return NotFound("Incorrect user");
                }

                error.RegisteringUser = user;
            }
          else
            {
                error.RegisteringUser = null;
            }

            _context.Errors.Add(error);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetError", new { id = error.ErrorId }, error);
        }

        // DELETE: api/Error/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteError(int id)
        {
            if (_context.Errors == null)
            {
                return NotFound();
            }
            var error = await _context.Errors.FindAsync(id);
            if (error == null)
            {
                return NotFound();
            }

            error.Status = 0;
            _context.Errors.Update(error);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrorExists(int id)
        {
            return (_context.Errors?.Any(e => e.ErrorId == id)).GetValueOrDefault();
        }
    }
}
