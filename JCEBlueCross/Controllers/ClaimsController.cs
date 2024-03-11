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
    public class ClaimsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClaimsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Claims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claim>>> GetClaims()
        {
          if (_context.Claims == null)
          {
              return NotFound();
          }
            return await _context.Claims.ToListAsync();
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Claim>> GetClaim(int id)
        {
          if (_context.Claims == null)
          {
              return NotFound();
          }
            var claim = await _context.Claims.FindAsync(id);

            if (claim == null)
            {
                return NotFound();
            }

            return claim;
        }

        // PUT: api/Claims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClaim(int id, Claim claim)
        {
            if (id != claim.ClaimId)
            {
                return BadRequest();
            }

            _context.Entry(claim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimExists(id))
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

        // POST: api/Claims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Claim>> PostClaim(Claim claim)
        {
          if (_context.Claims == null)
          {
              return Problem("Entity set 'AppDbContext.Claims'  is null.");
          }
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClaim", new { id = claim.ClaimId }, claim);
        }

        // DELETE: api/Claims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            if (_context.Claims == null)
            {
                return NotFound();
            }
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = 0;
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClaimExists(int id)
        {
            return (_context.Claims?.Any(e => e.ClaimId == id)).GetValueOrDefault();
        }
    }
}
