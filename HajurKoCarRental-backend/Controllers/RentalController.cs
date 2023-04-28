using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental_backend.DataContext;
using HajurKoCarRental_backend.Model;

namespace HajurKoCarRental_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDataContext _context;

        public RentalController(AppDataContext context)
        {
            _context = context;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentalModel>> PostRentalModel(RentalModel rentalModel)
        {
          if (_context.Rentals == null)
          {
              return Problem("Entity set 'AppDataContext.Rentals'  is null.");
          }
            _context.Rentals.Add(rentalModel);
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
