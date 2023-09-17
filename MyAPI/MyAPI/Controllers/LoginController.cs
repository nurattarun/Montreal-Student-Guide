using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAPI.Controllers
{

    [Route("api/[controller]/{action}")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Login>> Get()
        {
            var logins = _context.Logins.ToList();

            if (logins == null)
            {
                return NotFound();
            }
            return Ok(new { logins });

        }

        [HttpPost]
        public ActionResult Signup(Login login) //asking for a login content/body
        {
            var verifyLogin = _context.Logins.FirstOrDefault(x => x.UserName == login.UserName);
            if (verifyLogin != null)
            {
                return BadRequest(new {error_message = "Sorry, a username with the same name already exists!"});
            }
            try
            {
                _context.Logins.Add(login);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Something went wrong while saving!" });
            }
            return Ok(login);
        }

        [HttpPost]

        public ActionResult Login(SignIn signIn)
        {
            var verifyLogin = _context.Logins.FirstOrDefault(x => x.UserName == signIn.UserName && x.Password == signIn.Password);
           if(verifyLogin == null)
            {
                return BadRequest(new { error_message = "Invalid Username/PaSSword" });
            }
            return Ok(signIn);
        }
        


    }
}
