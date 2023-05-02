using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental_backend.DataContext;
using HajurKoCarRental_backend.Model;
using System.Security.Claims;

namespace HajurKoCarRental_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalHistoriesController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RentalHistoriesController(AppDataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/RentalHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalHistory>>> GetRentalHistory()
        {
            if (_context.RentalHistory == null)
            {
                return NotFound();
            }
            return await _context.RentalHistory.ToListAsync();
        }

        // GET: api/RentalHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalHistory>> GetRentalHistory(int id)
        {
            if (_context.RentalHistory == null)
            {
                return NotFound();
            }
            var rentalHistory = await _context.RentalHistory.FindAsync(id);

            if (rentalHistory == null)
            {
                return NotFound();
            }

            return rentalHistory;
        }

        // PUT: api/RentalHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalHistory(int id, RentalHistory rentalHistory)
        {
            if (id != rentalHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalHistoryExists(id))
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

        private async Task<UserModel> GetUser()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            if (currentUser == null) throw new Exception("No User Found!!");

            var userEmail = currentUser?.FindFirstValue(ClaimTypes.Email);
            UserModel? users = await _context.Users.Where(user => user.email_address == userEmail).FirstOrDefaultAsync();
            if (users == null) throw new Exception("User Not Found!!");

            return users;
        }

        [HttpGet("rentalData")]
        //to check or validate the discount managements
        public async Task<RentalHistory?> Discount(int id)
        {
            UserModel? user = await GetUser();
            if (user == null) return null;
            RentalHistory? rentHistoryData = await _context.RentalHistory.Where(allRental => allRental.Users.Id.ToString() == id.ToString()).FirstOrDefaultAsync() ?? throw new Exception("Error");

            //for staff
            if (user.Role == Role.Staff)
            {
                rentHistoryData.rental_charge = rentHistoryData.Cars.rental_cost * 0.25;
            }

            //for customer
            if (user.Role == Role.Customer)
            {
                if (user.last_login <= DateTime.UtcNow)
                {
                    rentHistoryData.rental_charge = rentHistoryData.Cars.rental_cost * 0.1;
                }
            }

            _context.Update(rentHistoryData);
            await _context.SaveChangesAsync();
            return rentHistoryData;
        }

        // POST: api/RentalHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentalHistory>> PostRentalHistory(RentalHistory rentalHistory)
        {
            if (_context.RentalHistory == null)
            {
                return Problem("Entity set 'AppDataContext.RentalHistory'  is null.");
            }
            _context.RentalHistory.Add(rentalHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalHistory", new { id = rentalHistory.Id }, rentalHistory);
        }

        // DELETE: api/RentalHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalHistory(int id)
        {
            if (_context.RentalHistory == null)
            {
                return NotFound();
            }
            var rentalHistory = await _context.RentalHistory.FindAsync(id);
            if (rentalHistory == null)
            {
                return NotFound();
            }

            _context.RentalHistory.Remove(rentalHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalHistoryExists(int id)
        {
            return (_context.RentalHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
