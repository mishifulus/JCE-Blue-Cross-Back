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

            var payorErrors = await _context.PayorErrors
            .Where(pe => pe.ErrorId == errorId)
            .ToListAsync();

            var payorIds = payorErrors.Select(pe => pe.PayorId).ToList();

            var payors = await _context.Payors
            .Where(p => payorIds.Contains(p.PayorId))
            .ToListAsync();

            if (!payors.Any())
            {
                return NoContent();
            }

            return payors;
        }

        // POST: api/PayorErrors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PayorError>> PostPayorError(int errorId, int payorId)
        {
            var error = await _context.Errors.FindAsync(errorId);
            var payor = await _context.Payors.FindAsync(payorId);

            if (error == null)
            {
                return NotFound($"Error with ID {errorId} not found.");
            }

            if (payor == null)
            {
                return NotFound($"Payor with ID {payorId} not found.");
            }

            var payorError = new PayorError
            {
                ErrorId = errorId,
                PayorId = payorId
            };

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

            var payorErrorsToDelete = await _context.PayorErrors.Where(p => p.ErrorId == errorId).ToListAsync();

            if (payorErrorsToDelete.Count == 0)
            {
                return NotFound();
            }

            _context.PayorErrors.RemoveRange(payorErrorsToDelete);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        //DELETE: api/PayorErrors/error/5/payor/2
        [HttpDelete("error/{errorId}/payor/{payorId}")]
        public async Task<IActionResult> DeletePayorErrorsByPayor(int errorId, int payorId)
        {
            if (_context.PayorErrors == null)
            {
                return NotFound();
            }

            var payorErrorsToDelete = await _context.PayorErrors.Where(p => p.ErrorId == errorId && p.PayorId == payorId)
                .ToListAsync();

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
