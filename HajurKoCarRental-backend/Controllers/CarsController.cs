using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental_backend.DataContext;
using HajurKoCarRental_backend.Model;
using Microsoft.AspNetCore.Authorization;
using HajurKoCarRental_backend.DTOs;
using HajurKoCarRental_backend.Extensions;

namespace HajurKoCarRental_backend.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IWebHostEnvironment? _environment;


        public CarsController(AppDataContext context, IWebHostEnvironment? environment)
        {
            _context = context;
            _environment = environment;
        }

        //[AllowAnonymous]
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
        //[AllowAnonymous]
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

        //change the avilability of the car to true
        [HttpPost("{id}/availability")]
        public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            car.availability_status = isAvailable ? AvailabilityStatus.available : AvailabilityStatus.unavailable;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Staff")]
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
        //[Authorize(Roles = "Staff")]
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

        private async Task<CarsModel> Image(int id, IFormFile? image)
        {
            CarsModel? car = await _context.Cars.Where(cars => cars.Id == id).FirstOrDefaultAsync();
            if (car == null) throw new Exception("Car not Found!!");
            if (image == null) return car;
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
            car.photo = imagePath;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        [HttpPost("registration")]
        //private async Task<UserModel>
        public async Task<CarsModel> PostUserModel([FromForm] CarRegisterDto carsRegisterDto)
        {
            CarsModel cars = carsRegisterDto.ToRegisterCars();
            //user.password = HashedPassword(registerDto.password);
            await _context.Cars.AddAsync(cars);
            await _context.SaveChangesAsync();
            cars = await Image(cars.Id, carsRegisterDto.photo);

            return cars;
        }
        // DELETE: api/Cars/5
        //[Authorize(Roles = "Staff")]
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
