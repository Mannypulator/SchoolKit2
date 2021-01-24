using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
    public class AccountController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _userManager;
        private readonly UserManager<ApplicationUser> _AuserManager;
        private readonly ApplicationSettings _appsettings;

        public AccountController(SchoolKitContext context, UserManager<Student> userManager, UserManager<ApplicationUser> AuserManager, IOptions<ApplicationSettings> appsettings)
        {
            _context = context;
            _userManager = userManager;
            _AuserManager = AuserManager;
            _appsettings = appsettings.Value;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.Users.Where(x => x.RegNo == model.UserName)
            .FirstOrDefaultAsync();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if(!user.IsActivated){
                    return Unauthorized( new {message = _appsettings.notActivated});
                }
                var roles = await _userManager.GetRolesAsync(user);
                 var role = roles.FirstOrDefault();
               var tokendescriptor = new SecurityTokenDescriptor{
                   Subject = new ClaimsIdentity(new Claim[]
                   {
                       new Claim("UserID", user.Id.ToString()),
                       new Claim("Role", role.ToString())
                   }),
                   Expires = DateTime.Now.AddHours(1),
                   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
               };
               var tokenHandler = new JwtSecurityTokenHandler();
               var securityToken = tokenHandler.CreateToken(tokendescriptor);
               var token = tokenHandler.WriteToken(securityToken);

               return Ok(new {token});
            }
            else
            {
                var Auser = await _AuserManager.FindByEmailAsync(model.UserName);
                 if (Auser != null && await _AuserManager.CheckPasswordAsync(Auser, model.Password))
               {
                   var studentcheck = await _userManager.FindByIdAsync(Auser.Id);
                 if(studentcheck != null){
                     if(!studentcheck.IsActivated){
                         return Unauthorized(new {message = _appsettings.notActivated});
                     }
                 }
                 var roles = await _AuserManager.GetRolesAsync(Auser);
                 var role = roles.FirstOrDefault();
               var tokendescriptor = new SecurityTokenDescriptor{
                   Subject = new ClaimsIdentity(new Claim[]
                   {
                       new Claim("UserID", Auser.Id.ToString()),
                       new Claim(ClaimTypes.Role, role.ToString())
                   }),
                   Expires = DateTime.Now.AddHours(1),
                   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
               };
               var tokenHandler = new JwtSecurityTokenHandler();
               var securityToken = tokenHandler.CreateToken(tokendescriptor);
               var token = tokenHandler.WriteToken(securityToken);

               return Ok(new {token});
              }  
              return BadRequest(new {message = _appsettings.failedlogin});   
            }
        }

        // GET: Account/Details/5
              // GET: Account/Create
      }
}
