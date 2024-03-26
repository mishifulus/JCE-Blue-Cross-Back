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
    public class PayorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PayorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Payor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payor>>> GetPayor()
        {
          if (_context.Payor == null)
          {
              return NotFound();
          }
            return await _context.Payor.Include(p => p.RegisteringUser).ToListAsync();  
        }

        // GET: api/Payor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payor>> GetPayor(int id)
        {
          if (_context.Payor == null)
          {
              return NotFound();
          }
            var payor = await _context.Payor.Include(p => p.RegisteringUser).FirstOrDefaultAsync(p => p.PayorId == id);

            if (payor == null)
            {
                return NotFound();
            }

            return payor;
        }

        // GET: api/SearchPayors/searchTerm
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Payor>>> SearchPayors(string searchTerm)
        {
            if (_context.Payor == null)
            {
                return NotFound();
            }

            var payors = await _context.Payor.Where(p =>
                p.PayorName.Contains(searchTerm) ||
                p.PayorAddress.Contains(searchTerm) ||
                p.ZipCode.Contains(searchTerm) ||
                p.State.Contains(searchTerm) ||
                p.City.Contains(searchTerm)
                ).Include(p => p.RegisteringUser)
                .ToListAsync();

            if (!payors.Any())
            {
                return NotFound();
            }

            return payors;
        }

        // PUT: api/Payor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayor(int id, Payor payor)
        {
            if (id != payor.PayorId)
            {
                return BadRequest();
            }

            _context.Entry(payor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayorExists(id))
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

        // POST: api/Payor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payor>> PostPayor(Payor payor)
        {
          if (_context.Payor == null)
          {
              return Problem("Entity set 'AppDbContext.Payor'  is null.");
          }

          if (payor.RegisteringUser != null)
            {
                var user = await _context.Users.FindAsync(payor.RegisteringUser.UserId);
                if (user == null)
                {
                    return NotFound("Incorrect user");
                }

                payor.RegisteringUser = user;
            }
          else
            {
                payor.RegisteringUser = null;
            }

            _context.Payor.Add(payor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayor", new { id = payor.PayorId }, payor);
        }

        // DELETE: api/Payor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayor(int id)
        {
            if (_context.Payor == null)
            {
                return NotFound();
            }
            var payor = await _context.Payor.FindAsync(id);
            if (payor == null)
            {
                return NotFound();
            }

            payor.Status = 0;
            _context.Payor.Update(payor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayorExists(int id)
        {
            return (_context.Payor?.Any(e => e.PayorId == id)).GetValueOrDefault();
        }
    }
}
