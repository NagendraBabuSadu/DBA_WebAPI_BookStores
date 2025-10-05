using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBA_WebAPI.Data;
using DBA_WebAPI.Models;

namespace DBA_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookStoresDbContext _context;

        public PublishersController(BookStoresDbContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
                return NotFound();

            return publisher;
        }

        // POST: api/Publishers
        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.PubId }, publisher);
        }

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, Publisher publisher)
        {
            if (id != publisher.PubId)
                return BadRequest();

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return NotFound();

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.PubId == id);
        }
    }
}