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
        public async Task<IActionResult> SetRentalSuccess(int id, string acceptedBy)
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
            rental.accepted_by = acceptedBy;

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

        //to change the rental status from pending to cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> SetRentalCancel(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            if (rental.rental_status == RentalStatus.cancelled)
            {
                return BadRequest("Rental is already cancelled.");
            }

            rental.rental_status = RentalStatus.cancelled;
            rental.accepted_by = "Not Accepted. Rental Cancelled.";

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
        //private async Task<RentalModel?> DiscountCheck(int id)
        //{
        //    UserModel? user = await _context.Users.Where(allUsers => allUsers.Id == id).FirstOrDefaultAsync();
        //    if (user == null) return null;
        //    RentalModel? rentData = await _context.Rentals.Where(allRental => allRental.Users.Id.ToString() == id.ToString()).FirstOrDefaultAsync() ?? throw new Exception("Error");

        //    //for staff
        //    if (user.Role == Role.Staff)
        //    {
        //        //rentData.available_discount = true;
        //        rentData.rental_amount = rentData.Cars.rental_cost * 0.25;
        //    }

        //    //for customer
        //    if (user.Role == Role.Customer)
        //    {
        //        if (user.last_login <= DateTime.UtcNow)
        //        {
        //            //rentData.available_discount = true;
        //            rentData.rental_amount = rentData.Cars.rental_cost * 0.10;
        //        }
        //    }


        //    _context.Update(rentData);
        //    await _context.SaveChangesAsync();
        //    return rentData;
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

            // Check if the car is damaged
            DamagedCarsModel? damagedCar = await _context.DamagedCars
                .Where(dc => dc.Cars_id == bookingModel.Cars_id && dc.settlement_status == SettlementStatus.success)
                .FirstOrDefaultAsync();

            if (damagedCar?.Id != null)
            {
                throw new Exception("Car is not available for rent due to unsettled damage.");
            }

            // Set the availability_status of the car to "unavailable"
            car.availability_status = AvailabilityStatus.unavailable;

            UserModel? user = await _context.Users.Where(users => users.Id == bookingModel.Users_id).FirstOrDefaultAsync() ?? throw new Exception("User not found!!");

            // Check if user's role is staff or customer and last_login is less than 3 months
            DateTime threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
            bool isEligibleForDiscount = user.last_login.HasValue && user.last_login.Value > threeMonthsAgo;


            if (isEligibleForDiscount)
            {
                double discountPercentage = 0;

                // Determine the discount percentage based on the user's role
                if (user.Role.Equals(Role.Customer))
                {
                    discountPercentage = 0.1; // 10% discount
                }
                else if (user.Role.Equals(Role.Staff))
                {
                    discountPercentage = 0.2; // 20% discount
                    //rental.discount = Discount.available; // Set discount status to "available"
                }

                // Calculate the discounted rental cost
                double rentalCost = ((bookingModel.end_date - bookingModel.start_date).Days+1) * car.rental_cost;
                double discountAmount = rentalCost * discountPercentage;
                double discountedRentalCost = rentalCost - discountAmount;

                // Update the rental amount in the rental model
                RentalModel? rental = bookingModel.ToBookCars();
                rental.discount = Discount.available; // Set discount status to "available"
                rental.rental_status = RentalStatus.pending;
                rental.rental_amount = discountedRentalCost;

                await _context.Rentals.AddAsync(rental);
                await _context.SaveChangesAsync();

                return Ok(rental);
            }
            else
            {
                // Calculate the rental cost
                double rentalCost = ((bookingModel.end_date - bookingModel.start_date).Days+1) * car.rental_cost;

                // Update the rental amount in the rental model
                RentalModel? rental = bookingModel.ToBookCars();
                rental.rental_status = RentalStatus.pending;
                rental.rental_amount = rentalCost;

                await _context.Rentals.AddAsync(rental);
                await _context.SaveChangesAsync();

                return Ok(rental);
            }
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