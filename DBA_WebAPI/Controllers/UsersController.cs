using DBA_WebAPI.Data;
using DBA_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBA_WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BookStoresDbContext _context;

        public UsersController(BookStoresDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            string emailAddress = HttpContext.User.Identity.Name;


            var user = await _context.Users.Where(user => user.EmailAddress == emailAddress).FirstOrDefaultAsync();

            user.Password = null;

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }    

    }
}
