﻿using System;
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
    public class ProviderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProviderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Provider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
        {
          if (_context.Providers == null)
          {
              return NotFound();
          }
            return await _context.Providers.Include(p => p.RegisteringUser).ToListAsync();
        }

        // GET: api/Provider
        [HttpGet("active/")]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvidersActive()
        {
            if (_context.Providers == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers.Where(p => p.Status == 1).Include(p => p.RegisteringUser).ToListAsync();

            if (providers == null)
            {
                return NotFound();
            }

            return providers;
        }

        // GET: api/Provider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
          if (_context.Providers == null)
          {
              return NotFound();
          }
            var provider = await _context.Providers.Include(p => p.RegisteringUser).FirstOrDefaultAsync(p => p.ProviderId == id);

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        // GET: api/SearchProviders/searchTerm
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Provider>>> SearchProviders(string searchTerm)
        {
            if (_context.Providers == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers.Where(p => 
                p.ProviderName.Contains(searchTerm) ||
                p.ProviderAddress.Contains(searchTerm) ||
                p.ZipCode.Contains(searchTerm) ||
                p.State.Contains(searchTerm) ||
                p.City.Contains(searchTerm)
                ).Include(p => p.RegisteringUser).
                ToListAsync();

            if (!providers.Any())
            {
                return NotFound();
            }

            return providers;
        }

        // PUT: api/Provider/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(int id, Provider provider)
        {
            if (id != provider.ProviderId)
            {
                return BadRequest();
            }

            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
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

        // POST: api/Provider
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provider>> PostProvider(Provider provider)
        {
          if (_context.Providers == null)
          {
              return Problem("Entity set 'AppDbContext.Providers'  is null.");
          }
            
            if (provider.RegisteringUser != null)
            {
                var user = await _context.Users.FindAsync(provider.RegisteringUser.UserId);
                if (user == null)
                {
                    return NotFound("Incorrect User");
                }

                provider.RegisteringUser = user;
            }
            else
            {
                provider.RegisteringUser = null;
            }

            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvider", new { id = provider.ProviderId }, provider);
        }

        // DELETE: api/Provider/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            if (_context.Providers == null)
            {
                return NotFound();
            }
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            provider.Status = 0;
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderExists(int id)
        {
            return (_context.Providers?.Any(e => e.ProviderId == id)).GetValueOrDefault();
        }
    }
}
