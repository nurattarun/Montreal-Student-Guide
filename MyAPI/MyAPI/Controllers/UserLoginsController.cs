//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MyAPI.Data;
//using MyAPI.Models;

//namespace MyAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserLoginsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _configuration;

//        public UserLoginsController(ApplicationDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        // GET: api/UserLogins
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Registration>>> GetUserLogin()
//        {
//          if (_context.UserLogin == null)
//          {
//              return NotFound();
//          }
//            return await _context.UserLogin.ToListAsync();
//        }

//        // GET: api/UserLogins/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Registration>> GetUserLoginGetUserLogin(int id)
//        {
//          if (_context.UserLogin == null)
//          {
//              return NotFound();
//          }
//            var userLogin = await _context.UserLogin.FindAsync(id);

//            if (userLogin == null)
//            {
//                return NotFound();
//            }

//            return userLogin;
//        }

//        // PUT: api/UserLogins/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutUserLogin(int id, Registration userLogin)
//        {
//            if (id != userLogin.StudentId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(userLogin).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UserLoginExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/UserLogins
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        [Route("signup")]

//        public async Task<IActionResult> Login(Registration userModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }



//            // Check if the user exists and the password is correct
//            var user = await _context.UserLogin.FirstOrDefaultAsync(u => u.Username == userModel.Username && u.Password == userModel.Password);
//            if (user == null)
//            {
//                return Unauthorized();
//            }



//            // Create a claims identity with the user ID and username
//            var claims = new[]
//                     {
//             new Claim(ClaimTypes.NameIdentifier, user.StudentId.ToString()),
//             new Claim(ClaimTypes.Name, user.Username)
//             };
//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



//            // Create the authentication properties
//            var authProperties = new AuthenticationProperties
//            {
//                AllowRefresh = true,
//                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
//                IsPersistent = true
//            };

//            // Sign in the user and return a success response
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
//            return Ok("User logged in successfully");
//        }
//        //public async Task<IActionResult<UserLogin>> PostUserLogin(UserLogin userLogin)
//        //{
//        //  if (_context.UserLogin == null)
//        //  {
//        //      return Problem("Entity set 'ApplicationDbContext.UserLogin'  is null.");
//        //  }
//        //    _context.UserLogin.Add(userLogin);
//        //    await _context.SaveChangesAsync();

//        //    return CreatedAtAction("GetUserLogin", new { id = userLogin.StudentId }, userLogin);
//        //}

//        // DELETE: api/UserLogins/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUserLogin(int id)
//        {
//            if (_context.UserLogin == null)
//            {
//                return NotFound();
//            }
//            var userLogin = await _context.UserLogin.FindAsync(id);
//            if (userLogin == null)
//            {
//                return NotFound();
//            }

//            _context.UserLogin.Remove(userLogin);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UserLoginExists(int id)
//        {
//            return (_context.UserLogin?.Any(e => e.StudentId == id)).GetValueOrDefault();
//        }
//    }
//}
