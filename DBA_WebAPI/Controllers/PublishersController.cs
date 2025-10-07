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
        // GET: api/Publishers/5
        [HttpGet("GetPublisherDetails/{id}")]
        public async Task<ActionResult<Publisher>> GetPublisherDetails(int id)
        {
            // Eager Loading
            //var publisher = _context.Publishers
            //                    .Include(pub => pub.Books)
            //                    .Include(pub => pub.Users)
            //                    .Where(pub => pub.PubId == id)
            //                    .FirstOrDefault();

            // Explicit Loading
            var publisher = await _context.Publishers.SingleAsync(pub => pub.PubId ==  id);

            _context.Entry(publisher)
                .Collection(pub => pub.Users)
                .Query()
                .Where(user => user.EmailAddress.Contains("kar"))
                .Load();

            _context.Entry(publisher)
                .Collection(pub => pub.Books)
                .Query()
                .Include(book => book.Sales)
                .Load();

            var user = await _context.Users.SingleAsync(user => user.UserId == 1);

            _context.Entry(user)
                .Reference(user => user.Role)
                .Load();

            if (publisher == null)
                return NotFound();

            return publisher;
        }
        // GET: api/Publishers
        [HttpGet("PostPublisherDetails/")]
        public async Task<ActionResult<Publisher>> PostPublisherDetails()
        {
            var publisher = new Publisher();
            publisher.PublisherName = "Harper & Bros";
            publisher.City = "New York City";
            publisher.State = "NY";
            publisher.Country = "USA";

            Book book1 = new Book();
            book1.Title = "Moon Lighting 1";
            book1.Type = "Fiction";
            book1.PublishedDate = DateTime.Now;

            Book book2 = new Book();
            book2.Title = "Moon Lighting 2";
            book2.Type = "Fiction";
            book2.PublishedDate = DateTime.Now;

            Sale sale1 = new Sale();
            sale1.Quantity = 1;
            sale1.StoreId = "1231";
            sale1.OrderNum = "SY#";
            sale1.OrderDate = DateTime.Now;
            sale1.PayTerms = "Pay Terms";

            book1.Sales.Add(sale1);

            publisher.Books.Add(book1);
            publisher.Books.Add(book2);      

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            var publishers = await _context.Publishers
                                        .Include(pub => pub.Books)                                            
                                        .Where(pub => pub.PubId == publisher.PubId)
                                        .FirstOrDefaultAsync();

            if (publishers == null)
                return NotFound();

            return publishers;
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