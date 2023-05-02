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
    public class OffersController : ControllerBase
    {
        private readonly AppDataContext _context;

        public OffersController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Offers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OffersModel>>> GetOffersModel()
        {
          if (_context.OffersModel == null)
          {
              return NotFound();
          }
            return await _context.OffersModel.ToListAsync();
        }

        // GET: api/Offers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OffersModel>> GetOffersModel(int id)
        {
          if (_context.OffersModel == null)
          {
              return NotFound();
          }
            var offersModel = await _context.OffersModel.FindAsync(id);

            if (offersModel == null)
            {
                return NotFound();
            }

            return offersModel;
        }

        // PUT: api/Offers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffersModel(int id, OffersModel offersModel)
        {
            if (id != offersModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(offersModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffersModelExists(id))
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

        // POST: api/Offers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OffersModel>> PostOffersModel(OffersModel offersModel)
        {
          if (_context.OffersModel == null)
          {
              return Problem("Entity set 'AppDataContext.OffersModel'  is null.");
          }
            _context.OffersModel.Add(offersModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffersModel", new { id = offersModel.Id }, offersModel);
        }

        // DELETE: api/Offers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffersModel(int id)
        {
            if (_context.OffersModel == null)
            {
                return NotFound();
            }
            var offersModel = await _context.OffersModel.FindAsync(id);
            if (offersModel == null)
            {
                return NotFound();
            }

            _context.OffersModel.Remove(offersModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OffersModelExists(int id)
        {
            return (_context.OffersModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
