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
    public class NotificationsController : ControllerBase
    {
        private readonly AppDataContext _context;

        public NotificationsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationModel>>> GetNotificationModels()
        {
          if (_context.NotificationModels == null)
          {
              return NotFound();
          }
            return await _context.NotificationModels.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationModel>> GetNotificationModel(int id)
        {
          if (_context.NotificationModels == null)
          {
              return NotFound();
          }
            var notificationModel = await _context.NotificationModels.FindAsync(id);

            if (notificationModel == null)
            {
                return NotFound();
            }

            return notificationModel;
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificationModel(int id, NotificationModel notificationModel)
        {
            if (id != notificationModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(notificationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationModelExists(id))
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

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotificationModel>> PostNotificationModel(NotificationModel notificationModel)
        {
          if (_context.NotificationModels == null)
          {
              return Problem("Entity set 'AppDataContext.NotificationModels'  is null.");
          }
            _context.NotificationModels.Add(notificationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificationModel", new { id = notificationModel.Id }, notificationModel);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationModel(int id)
        {
            if (_context.NotificationModels == null)
            {
                return NotFound();
            }
            var notificationModel = await _context.NotificationModels.FindAsync(id);
            if (notificationModel == null)
            {
                return NotFound();
            }

            _context.NotificationModels.Remove(notificationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationModelExists(int id)
        {
            return (_context.NotificationModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
