using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental_backend.DataContext;
using HajurKoCarRental_backend.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using HajurKoCarRental_backend.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;


namespace HajurKoCarRental_backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDataContext _context;
        private IConfiguration _configuration;


        public UsersController(AppDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userModel = await _context.Users.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        //for hashing the password
        private string HashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        // POST: api/Users/registration
        [HttpPost("registration")]
        public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDataContext.Users'  is null.");
            }
            userModel.password = HashedPassword(userModel.password);
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //for login purposes
        //for checking the user credentials accuracy
        private async Task<UserModel?> AuthenticateUser(LoginDto users)
        {
            //for checking existing user
            UserModel? availableUser = await _context.Users.FirstOrDefaultAsync(mail => mail.email_address == users.email_address);

            if (availableUser == null) return null;

            //for checking the existing user's password
            bool isPasswordMatched = BCrypt.Net.BCrypt.Verify(users.password, availableUser.password);

            if (!isPasswordMatched) return null;
            await GenerateToken(users);
            return availableUser;



        }

        //For managing JWT Token for login purposes
        private async Task<UserModel> GenerateToken(LoginDto users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"], 
                _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            UserModel? approvedUser = await _context.Users.Where(mainUser => mainUser.email_address == users.email_address).FirstOrDefaultAsync();
            approvedUser.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return approvedUser;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<UserModel> Login(LoginDto users)
        {
            UserModel? user_ = await AuthenticateUser(users);
            if (user_ == null)
            {
                throw new Exception("No user Found!!");
            }
            return user_;
        }

        private bool UserModelExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
