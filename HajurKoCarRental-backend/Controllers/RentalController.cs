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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using HajurKoCarRental_backend.DTOs;
using HajurKoCarRental_backend.Extensions;

namespace HajurKoCarRental_backend.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDataContext _context;

        //private readonly IHttpContextAccessor _httpContextAccessor;
        public RentalController(AppDataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
        }


        // GET: api/Rental
        //[Authorize]
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

        //to get the data of rental pending
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<RentalModel>>> GetPendingRentals()
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }

            return await _context.Rentals
                .Include(r => r.Cars)
                .Include(r => r.Users)
                .Where(r => r.rental_status == RentalStatus.pending)
                .ToListAsync() ?? throw new Exception("No pending rental data found!!");
        }

        [HttpGet("success")]
        public async Task<ActionResult<IEnumerable<RentalModel>>> GetSuccessRentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Cars)
                .Include(r => r.Users)
                .Where(r => r.rental_status == RentalStatus.success)
                .ToListAsync();

            if (rentals == null)
            {
                return NotFound();
            }

            return Ok(rentals);
        }

        //to change the rental status from pending to success
        [HttpPut("{id}/success")]
        public async Task<IActionResult> SetRentalSuccess(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            if (rental.rental_status == RentalStatus.success)
            {
                return BadRequest("Rental is already marked as success.");
            }

            rental.rental_status = RentalStatus.success;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
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

        //to check or validate the discount managements
        private async Task<RentalModel?> DiscountCheck(int id)
        {
            UserModel? user = await _context.Users.Where(allUsers => allUsers.Id == id).FirstOrDefaultAsync();
            if (user == null) return null;
            RentalModel? rentData = await _context.Rentals.Where(allRental => allRental.Users.Id.ToString() == id.ToString()).FirstOrDefaultAsync() ?? throw new Exception("Error");

            //for staff
            if (user.Role == Role.Staff)
            {
                //rentData.available_discount = true;
                rentData.rental_amount = rentData.Cars.rental_cost * 0.25;
            }

            //for customer
            if (user.Role == Role.Customer)
            {
                if (user.last_login <= DateTime.UtcNow)
                {
                    //rentData.available_discount = true;
                    rentData.rental_amount = rentData.Cars.rental_cost * 0.10;
                }
            }


            _context.Update(rentData);
            await _context.SaveChangesAsync();
            return rentData;
        }

        //[HttpPost("booking")]
        //public async Task<ActionResult<RentalModel>> PostRentalModel(CarBookingDto bookingModel)
        //{
        //    if (_context.Rentals == null)
        //    {
        //        return Problem("Entity set 'AppDataContext.Rentals'  is null.");
        //    }

        //    RentalModel? rental = bookingModel.ToBookCars();

        //    CarsModel? car = await _context.Cars.Where(cars => cars.Id == bookingModel.Cars_id).FirstOrDefaultAsync() ?? throw new Exception("Car not found!!");
        //    UserModel? user = await _context.Users.Where(users => users.Id == bookingModel.Users_id).FirstOrDefaultAsync() ?? throw new Exception("User not found!!");

        //    await _context.Rentals.AddAsync(rental);
        //    await _context.SaveChangesAsync();

        //    return Ok(rental);
        //}

        [HttpPost("booking")]
        public async Task<ActionResult<RentalModel>> PostRentalModel(CarBookingDto bookingModel)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'AppDataContext.Rentals' is null.");
            }

            CarsModel? car = await _context.Cars
    .Where(cars => cars.Id == bookingModel.Cars_id && cars.availability_status.Equals(AvailabilityStatus.available))
    .FirstOrDefaultAsync() ?? throw new Exception("Car not available for rent.");


            DamagedCarsModel? damagedCar = await _context.DamagedCars.Where(dc => dc.Cars_id == bookingModel.Cars_id && dc.settlement_status == SettlementStatus.success).FirstOrDefaultAsync() ?? throw new Exception("Car is not available for rent due to unsettled damage.");

            UserModel? user = await _context.Users.Where(users => users.Id == bookingModel.Users_id).FirstOrDefaultAsync() ?? throw new Exception("User not found!!");

            RentalModel? rental = bookingModel.ToBookCars();

            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            return Ok(rental);
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
