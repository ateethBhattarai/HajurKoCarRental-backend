﻿using System;
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
using Microsoft.Extensions.Hosting;
using HajurKoCarRental_backend.Extensions;


namespace HajurKoCarRental_backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDataContext _context;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment? _environment;



        public UsersController(AppDataContext context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _configuration = configuration;
            _environment = environment;

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
        private async Task<UserModel> UploadDocument(int id, IFormFile? document)
        {
            UserModel? user = await _context.Users.Where(users => users.Id == id).FirstOrDefaultAsync();
            if (user == null) throw new Exception("User Not Found!!");

            //for validating the document
            if (document == null || document.Length == 0)
            {
                return user;
            }

            if (!IsValidDocument(document))
            {
                throw new Exception("Invalid document format!!");
            }

            string documentPath = _environment?.WebRootPath + "\\images\\";
            if (!Directory.Exists(documentPath))
            {
                Directory.CreateDirectory(documentPath);
            }
            string documentLocationPath = documentPath + document.FileName;
            using (var filestream = new FileStream(documentLocationPath, FileMode.Create))
            {
                await document.CopyToAsync(filestream);
            }

            string imagePath = "\\images\\" + document.FileName;

            user.document = imagePath;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private bool IsValidDocument(IFormFile document)
        {
            //check the file format
            string[] allowedFormating = { ".pdf", ".png" };
            string fileExtension = Path.GetExtension(document.FileName).ToLowerInvariant();
            if (!allowedFormating.Contains(fileExtension)) return false;

            //check the size of the file
            long fileSize = 1_500_000; //it means 1.5 M.B.
            if (document.Length > fileSize) return false;

            return true;
        }

        private async Task<UserModel> Image(int id, IFormFile? image)
        {
            UserModel? user = await _context.Users.Where(users => users.Id == id).FirstOrDefaultAsync();
            if (user == null) throw new Exception("User not Found!!");
            if (image == null) return user;
            if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
            }
            using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + image.FileName))
            {
                image.CopyTo(filestream);
                filestream.Flush();
            }
            string imagePath = "\\Upload\\" + image.FileName;
            user.profile_picture = imagePath;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPost("registration")]
        //private async Task<UserModel>
        public async Task<UserModel> PostUserModel([FromForm] RegisterDto registerDto)
        {
            //if (_context.Users == null)
            //{
            //    return Problem("Entity set 'AppDataContext.Users'  is null.");
            //}

            UserModel user = registerDto.ToRegister();
            user.password = HashedPassword(registerDto.password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            user = await Image(user.Id, registerDto.profile_picture);
            user = await UploadDocument(user.Id, registerDto.document);

            return user;

            //return CreatedAtAction("GetUserModel", new { id = registerDto.Id }, registerDto);
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
