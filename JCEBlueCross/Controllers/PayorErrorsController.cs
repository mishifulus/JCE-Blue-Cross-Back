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
    public class PayorErrorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PayorErrorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PayorErrors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayorError>>> GetPayorErrors()
        {
            if (_context.PayorErrors == null)
            {
                return NotFound();
            }
            return await _context.PayorErrors.ToListAsync();
        }

        // GET: api/PayorErrors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayorError>> GetPayorError(int id)
        {
            if (_context.PayorErrors == null)
            {
                return NotFound();
            }
            var payorError = await _context.PayorErrors.FindAsync(id);

            if (payorError == null)
            {
                return NotFound();
            }

            return payorError;
        }

        // GET: api/PayorErrors/error/5
        [HttpGet("error/{errorId}")]
        public async Task<ActionResult<IEnumerable<Payor>>> GetPayorsByError(int errorId)
        {
            if (_context.PayorErrors == null)
            {
                return NotFound();
            }

            var payorErrors = await _context.PayorErrors.Include(pe => pe.Payor).ThenInclude(p => p.RegisteringUser).Where(p => p.Error.ErrorId == errorId).ToListAsync();

            if (payorErrors.Count == 0)
            {
                return NoContent();
            }

            var payors = payorErrors.Select(pe => pe.Payor).ToList();

            if (payors.Count == 0)
            {
                return NoContent();
            }

            return payors;
        }



        // PUT: api/PayorErrors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayorError(int id, PayorError payorError)
        {
            if (id != payorError.PayorErrorId)
            {
                return BadRequest();
            }

            _context.Entry(payorError).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayorErrorExists(id))
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

        // POST: api/PayorErrors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PayorError>> PostPayorError(PayorError payorError)
        {
          if (_context.PayorErrors == null)
          {
              return Problem("Entity set 'AppDbContext.PayorErrors'  is null.");
          }
            _context.PayorErrors.Add(payorError);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayorError", new { id = payorError.PayorErrorId }, payorError);
        }

        // DELETE: api/PayorErrors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayorError(int id)
        {
            if (_context.PayorErrors == null)
            {
                return NotFound();
            }
            var payorError = await _context.PayorErrors.FindAsync(id);
            if (payorError == null)
            {
                return NotFound();
            }

            _context.PayorErrors.Remove(payorError);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/PayorErrors/error/5
        [HttpDelete("error/{errorId}")]
        public async Task<IActionResult> DeletePayorErrorsByError(int errorId)
        {
            if (_context.PayorErrors == null)
            {
                return NotFound();
            }

            var payorErrorsToDelete = await _context.PayorErrors.Where(p => p.Error.ErrorId == errorId).ToListAsync();

            if (payorErrorsToDelete.Count == 0)
            {
                return NotFound();
            }

            _context.PayorErrors.RemoveRange(payorErrorsToDelete);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool PayorErrorExists(int id)
        {
            return (_context.PayorErrors?.Any(e => e.PayorErrorId == id)).GetValueOrDefault();
        }
    }
}
