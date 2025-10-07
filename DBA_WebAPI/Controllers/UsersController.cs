using DBA_WebAPI.Data;
using DBA_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DBA_WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BookStoresDbContext _context;
        private readonly JWTSettings _jwtsettings;


        public UsersController(BookStoresDbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
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

        // GET: api/Users
        [HttpGet("Login")]
        public async Task<ActionResult<UserWithToken>> Login(User user)
        {
            user = await _context.Users
                .Include(user => user.Role)
                .Where(u => u.EmailAddress == user.EmailAddress && u.Password == user.Password).FirstOrDefaultAsync();

            UserWithToken userWithToken = new UserWithToken(user);

            if (userWithToken != null)
            {
                return NotFound();
            }

            // Sign your token here..

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress)
                }),
                Expires = DateTime.UtcNow.AddMonths(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.AccessToken = tokenHandler.WriteToken(token);

            return userWithToken;
        }
    }
}
