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
    public class RentalHistoriesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public RentalHistoriesController(AppDataContext context)
        {
            _context = context;
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
