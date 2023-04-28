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
    public class CarsController : ControllerBase
    {
        private readonly AppDataContext _context;

        public CarsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarsModel>>> GetCars()
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarsModel>> GetCarsModel(int id)
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            var carsModel = await _context.Cars.FindAsync(id);

            if (carsModel == null)
            {
                return NotFound();
            }

            return carsModel;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarsModel(int id, CarsModel carsModel)
        {
            if (id != carsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(carsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarsModelExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarsModel>> PostCarsModel(CarsModel carsModel)
        {
          if (_context.Cars == null)
          {
              return Problem("Entity set 'AppDataContext.Cars'  is null.");
          }
            _context.Cars.Add(carsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarsModel", new { id = carsModel.Id }, carsModel);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarsModel(int id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var carsModel = await _context.Cars.FindAsync(id);
            if (carsModel == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(carsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarsModelExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
