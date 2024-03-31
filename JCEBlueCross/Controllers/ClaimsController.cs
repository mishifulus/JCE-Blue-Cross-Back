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
            return await _context.Claims.Include(p => p.Member).Include(p => p.Provider).ThenInclude(p => p.RegisteringUser).Include(p => p.Payor).ThenInclude(p => p.RegisteringUser).ToListAsync();
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Claim>> GetClaim(int id)
        {
          if (_context.Claims == null)
          {
              return NotFound();
          }
            var claim = await _context.Claims.Include(p => p.Member).Include(p => p.Provider).ThenInclude(p => p.RegisteringUser).Include(p => p.Payor).ThenInclude(p => p.RegisteringUser).FirstOrDefaultAsync(p => p.ClaimId == id);

            if (claim == null)
            {
                return NotFound();
            }

            return claim;
        }

        // GET: api/SearchClaim/searchTerm
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Claim>>> SearchClaims(string searchTerm)
        {
            if (_context.Claims == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims.Where(c =>
                c.ClaimNumber.Contains(searchTerm) ||
                c.InstitutionalClaimCode.Contains(searchTerm) ||
                c.ProfessionalClaimCode.Contains(searchTerm) ||
                c.TypeBill.Contains(searchTerm) ||
                c.ReferalNum.Contains(searchTerm) ||
                c.ServiceCode.Contains(searchTerm) ||
                c.AuthCode.Contains(searchTerm) ||
                c.MedicalRecordNumber.Contains(searchTerm) ||
                c.PayerClaimControlNumber.Contains(searchTerm) ||
                c.AutoAccidentState.Contains(searchTerm) ||
                c.FileInf.Contains(searchTerm) ||
                c.ClaimNote.Contains(searchTerm) ||
                c.BillingNote.Contains(searchTerm) ||
                c.PrincipalDiagnosisCode.Contains(searchTerm) ||
                c.AdmitingDiagnosisCode.Contains(searchTerm) ||
                c.PatientReasonCode.Contains(searchTerm) ||
                c.ExternalCausesCode.Contains(searchTerm) ||
                c.DiagnosisRelatedCode.Contains(searchTerm) ||
                c.OtherDiagnosisCode.Contains(searchTerm) ||
                c.PrincipalProcedureCode.Contains(searchTerm) ||
                c.OtherProcedureCode.Contains(searchTerm) ||
                c.OccurrenceSpamCode.Contains(searchTerm) ||
                c.OccurrenceInformationCode.Contains(searchTerm) ||
                c.ValueInformationCode.Contains(searchTerm) ||
                c.ConditionInformationCode.Contains(searchTerm) ||
                c.TreatmentCodeCode.Contains(searchTerm) ||
                c.ClaimPricingCode.Contains(searchTerm)
                ).Include(p => p.Member).Include(p => p.Provider).ThenInclude(p => p.RegisteringUser).Include(p => p.Payor).ThenInclude(p => p.RegisteringUser)
                .ToListAsync();

            if (!claims.Any())
            {
                return NotFound();
            }

            return claims;
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
            
            if (claim.Member != null)
            {
                var user = await _context.Users.FindAsync(claim.Member.UserId);
                if (user == null)
                {
                    return NotFound("Incorrect member");
                }

                claim.Member = user;
            }
            else
            {
                claim.Member = null;
            }

            if (claim.Provider != null)
            {
                var provider = await _context.Providers.FindAsync(claim.Provider.ProviderId);
                if (provider == null)
                {
                    return NotFound("Incorrect provider");
                }

                claim.Provider = provider;
            }
            else
            {
                claim.Provider = null;
            }

            if (claim.Payor != null)
            {
                var payor = await _context.Payors.FindAsync(claim.Payor.PayorId);
                if (payor == null)
                {
                    return NotFound("Incorrect payor");
                }

                claim.Payor = payor;
            }
            else
            {
                claim.Payor = null;
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
