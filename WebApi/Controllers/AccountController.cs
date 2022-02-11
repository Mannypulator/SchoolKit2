using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    [EnableCors("SiteCorsPolicy")]
    public class AccountController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<ApplicationUser> _AuserManager;
        private readonly ApplicationSettings _appsettings;

        public AccountController(SchoolKitContext context, UserManager<Student> studentManager, UserManager<ApplicationUser> AuserManager, IOptions<ApplicationSettings> appsettings)
        {
            _context = context;
            _studentManager = studentManager;
            _AuserManager = AuserManager;
            _appsettings = appsettings.Value;
        }

        [HttpPost]
        [Route("studentlogin")]
        public async Task<IActionResult> StudentLogin(LoginModel model)
        {
            var student = await _studentManager.Users.Where(x => x.RegNo == model.UserName)
            .FirstOrDefaultAsync();
            if (student != null && await _studentManager.CheckPasswordAsync(student, model.Password))
            {
               /* if (!student.IsActivated)
                {
                    return Unauthorized(new { message = _appsettings.notActivated });
                }*/
                var roles = await _studentManager.GetRolesAsync(student);
                var role = roles.FirstOrDefault();
                var tokendescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                       new Claim("UserID", student.Id.ToString()),
                       new Claim(ClaimTypes.Role, role.ToString())
                    }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokendescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
             return BadRequest(new { message = _appsettings.failedlogin });
        }

        [HttpPost]
        [Route("stafflogin")]
        public async Task<IActionResult> StaffLogin(LoginModel model)
        {
            var Auser = await _AuserManager.FindByEmailAsync(model.UserName);
            if (Auser != null && await _AuserManager.CheckPasswordAsync(Auser, model.Password))
            {
                //var usercheck = await _userManager.FindByIdAsync(Auser.Id);
                if (Auser == null)
                {

                    return BadRequest(new { message = _appsettings.failedlogin });

                }
                var roles = await _AuserManager.GetRolesAsync(Auser);
                
              
                var tokendescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                       new Claim("UserID", Auser.Id.ToString()),
                      
                       //new Claim(ClaimTypes.Role, "superadmin")
                    }),
                    Expires = DateTime.Now.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
               foreach(var role in roles){
                   var claim = new Claim(ClaimTypes.Role, role.ToString());
                   tokendescriptor.Subject.AddClaim(claim); 
               }
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokendescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            return BadRequest(new { message = _appsettings.failedlogin });

        }

         [HttpGet]
        [Route("getStates")]
        public async Task<IActionResult> GetStates(){
            var states = await _context.States
            .Include(x => x.LGAs)
            .ToListAsync();

            return Ok(states);
        }

        [HttpPost]
        [Route("getLgas")]
        public async Task<IActionResult> GetLGAs([FromBody] StateModel state){
            var Lgas = await _context.States
            .Include(x => x.LGAs)
            .Where(x => x.StateID == state.StateID)
            .SelectMany(x=> x.LGAs)
            .ToListAsync();

            return Ok(Lgas);
        }

        // GET: Account/Details/5
        // GET: Account/Create
    }
}
