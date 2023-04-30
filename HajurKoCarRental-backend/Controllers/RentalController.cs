using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental_backend.DataContext;
using HajurKoCarRental_backend.Model;
using System.Reflection.Metadata.Ecma335;

namespace HajurKoCarRental_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDataContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public RentalController(AppDataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: api/Rental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalModel>>> GetRentals()
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            return await _context.Rentals.ToListAsync();
        }

        // GET: api/Rental/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalModel>> GetRentalModel(int id)
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rentalModel = await _context.Rentals.FindAsync(id);

            if (rentalModel == null)
            {
                return NotFound();
            }

            return rentalModel;
        }

        // PUT: api/Rental/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalModel(int id, RentalModel rentalModel)
        {
            if (id != rentalModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalModelExists(id))
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

        // POST: api/Rental
        [HttpPost]
        private async Task<RentalModel?> DiscountCheck(int id) //to check or validate the discount management
        {
            UserModel? user = await _context.Users.Where(allUsers => allUsers.Id == id).FirstOrDefaultAsync();
            if (user == null) return null;
            RentalModel? rentData = await _context.Rentals.Where(allRental => allRental.Users.Id.ToString() == id.ToString()).FirstOrDefaultAsync() ?? throw new Exception("Error");

            //for staff
            if (user.role == Role.Staff)
            {
                rentData.available_discount = true;
                rentData.rental_amount = rentData.Cars.rental_cost * 0.25;
            }

            //for customer
            if (user.role == Role.Customer)
            {
                if (user.last_login <= DateTime.UtcNow)
                {
                    rentData.available_discount = true;
                    rentData.rental_amount = rentData.Cars.rental_cost * 0.10;
                }
            }

            _context.Update(rentData);
            await _context.SaveChangesAsync();
            return rentData;
        }

        public async Task<ActionResult<RentalModel>> PostRentalModel(RentalModel rentalModel)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'AppDataContext.Rentals'  is null.");
            }

            //rentalModel.discount_percentage = UserData(rentalModel);
            await _context.Rentals.AddAsync(rentalModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalModel", new { id = rentalModel.Id }, rentalModel);
        }

        // DELETE: api/Rental/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalModel(int id)
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rentalModel = await _context.Rentals.FindAsync(id);
            if (rentalModel == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rentalModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalModelExists(int id)
        {
            return (_context.Rentals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
