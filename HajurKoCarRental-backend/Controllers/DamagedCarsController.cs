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
    public class DamagedCarsController : ControllerBase
    {
        private readonly AppDataContext _context;

        public DamagedCarsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/DamagedCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DamagedCarsModel>>> GetDamagedCars()
        {
          if (_context.DamagedCars == null)
          {
              return NotFound();
          }
            return await _context.DamagedCars.ToListAsync();
        }

        // GET: api/DamagedCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DamagedCarsModel>> GetDamagedCarsModel(int id)
        {
          if (_context.DamagedCars == null)
          {
              return NotFound();
          }
            var damagedCarsModel = await _context.DamagedCars.FindAsync(id);

            if (damagedCarsModel == null)
            {
                return NotFound();
            }

            return damagedCarsModel;
        }

        // PUT: api/DamagedCars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDamagedCarsModel(int id, DamagedCarsModel damagedCarsModel)
        {
            if (id != damagedCarsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(damagedCarsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DamagedCarsModelExists(id))
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

        // POST: api/DamagedCars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DamagedCarsModel>> PostDamagedCarsModel(DamagedCarsModel damagedCarsModel)
        {
          if (_context.DamagedCars == null)
          {
              return Problem("Entity set 'AppDataContext.DamagedCars'  is null.");
          }
            _context.DamagedCars.Add(damagedCarsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDamagedCarsModel", new { id = damagedCarsModel.Id }, damagedCarsModel);
        }

        // DELETE: api/DamagedCars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDamagedCarsModel(int id)
        {
            if (_context.DamagedCars == null)
            {
                return NotFound();
            }
            var damagedCarsModel = await _context.DamagedCars.FindAsync(id);
            if (damagedCarsModel == null)
            {
                return NotFound();
            }

            _context.DamagedCars.Remove(damagedCarsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DamagedCarsModelExists(int id)
        {
            return (_context.DamagedCars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
