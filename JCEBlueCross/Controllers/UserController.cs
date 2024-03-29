﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCEBlueCross.Context;
using JCEBlueCross.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Cors;

namespace JCEBlueCross.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://localhost:5173", headers:"*", methods:"*")]
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

        // GET: api/User
        [HttpGet("active/")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersActive()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.Where(p => p.Status != 0).ToListAsync();

            if ( users == null)
            {
                return NotFound();
            }

            return users;
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

        // GET: api/SearchUsers/searchTerm
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string searchTerm)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.Where(u => 
                u.Name.Contains(searchTerm) ||
                u.LastName.Contains(searchTerm) ||
                u.UserAddress.Contains(searchTerm) ||
                u.ZipCode.Contains(searchTerm) ||
                u.State.Contains(searchTerm) ||
                u.City.Contains(searchTerm) ||
                u.Email.Contains(searchTerm) ||
                u.Username.Contains(searchTerm)
                ).ToListAsync();

            if (!users.Any())
            {
                return NotFound();
            }

            return users;
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

            var existingUser = await _context.Users.FindAsync(id);
            
            if (existingUser == null)
            {
                return NotFound();
            }

            // UPDATE PASSWORD
            if (user.Password != existingUser.Password)
            {
                user.Password = EncryptPassword(user.Password);
            }

            //SAVE
            _context.Entry(existingUser).State = EntityState.Detached;
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

            user.Password = EncryptPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5
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


        //LOGIN
        // POST: api/Login/5
        [HttpPost("/login")]
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            if (_context.Users == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound("Incorrect username");
            }

            if (!VerifyPassword(password, user.Password))
            {
                return NotFound("Incorrect password");
            }

            if (user.ExpireDate <= DateTime.Now)
            {
                return NotFound("User expired");
            }

            if (user.Status == 0)
            {
                return NotFound("User blocked");
            }

            return user;
        }

        // PUT: block/username
        [HttpPut("/block/{username}")]
        public async Task<ActionResult> BlockUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            user.Status = 0;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        //PASSWORD
        private static string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInputPassword = EncryptPassword(inputPassword);
            return hashedInputPassword == hashedPassword;
        }
    }

}
