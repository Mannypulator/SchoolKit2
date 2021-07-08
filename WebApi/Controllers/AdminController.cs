using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Methods;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [EnableCors("SiteCorsPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Principal> _principalManager;
        private readonly UserManager<Proprietor> _proprietorManager;
        private readonly UserManager<Admin> _adminManager;
        public AdminController(SchoolKitContext context, RoleManager<IdentityRole> roleManager,UserManager<Principal> principalManager, UserManager<Admin> adminManager, UserManager<Proprietor> proprietorManager)
        {
            _context = context;
            _roleManager = roleManager;
            _principalManager = principalManager;
            _adminManager = adminManager;
            _proprietorManager = proprietorManager;
        }

        [HttpPost]
        [Route("createRole")]
        //authorize for admin
        public async Task<IActionResult> Role([FromBody]SaveModel model)
        {
            try{
                bool exist = await _roleManager.RoleExistsAsync(model.name);
            if (!exist)
            {
             // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = model.name;
                await _roleManager.CreateAsync(role);
                return Ok(new {Message = "Role created"});
            }
            return BadRequest(new {Message = "Role already exists"});
            }
            catch(Exception e){
                throw e;
            }
            
            
        }

        [HttpPost]
        [Route("createSchool")]
        //authorize for admin
        public async Task<IActionResult> School([FromBody] SchoolModel model)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;;
            if(role != "Admin")
            {
                var check = _context.SchoolRegCodes
                .Where(x => x.Code == model.school.Code)
                .SingleOrDefault();
                if(check != null)
                {
                    await _context.Schools.AddAsync(model.school);

                    _context.SchoolRegCodes.Remove(check);
                   await _context.SaveChangesAsync();
                    return Ok(new{Message = "School successfully created"});
                }
                else{
                    return BadRequest( new{Message = "Please provide a valid code"});
                }
            }
                await _context.Schools.AddAsync(model.school);
                await _context.SaveChangesAsync();
                model.principal.SchoolID = model.school.SchoolID;
                
                PrincipalMethods pMethod = new PrincipalMethods(); 
                var result = await pMethod.Principal(model.principal,_context,_principalManager, "Proprietor");
                if(!result.Succeeded)
                {
                    _context.Schools.Remove(model.school);
                    await _context.SchoolRegCodes.AddAsync(new SchoolRegCode {Code = model.school.Code});
                    await _context.SaveChangesAsync();
                    return BadRequest(result);
                }
            return Ok(new{Message = "School successfully created"});
        }

        [HttpPost]
        [Route("createAdmin")]
        //authorise for only admin
        public async Task<IActionResult> Admin([FromBody] Admin model)
        {
           Admin admin = new Admin{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                //LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,
                Email = model.Email
                
            };
             try{
                 var result = await _adminManager.CreateAsync(admin, model.PasswordHash);
                 if(result.Succeeded)
                 {
                    await _adminManager.AddToRoleAsync(admin,model.Role);

                    await _context.SaveChangesAsync();
                     
                 }
                 return Ok(result);
                }
            catch(Exception ex){
                throw ex;
            }
           
            
        }

        [HttpPost]
        [Route("generateCode")]
        //authorise for only admin
        public async Task<IActionResult> Code()
        {
           Random random = new Random();
           string t = random.Next(9999,100000).ToString();
           await _context.SchoolRegCodes.AddAsync(new SchoolRegCode{Code = t});
           await _context.SaveChangesAsync();
           return Ok(t);
        }

        //for admin only
        
        [HttpDelete]
        [Route("deleteAdmin")]
        //authorise for superadmin only
        public async Task<IActionResult> DeleteAdmin([FromBody] String adminId)
        {/// remember to test this method, see if admin is deleted without deleting school
        /// remember to test this method, see if admin is deleted without deleting school
        /// remember to test this method, see if admin is deleted without deleting school
            var admin = _adminManager.Users.Where(x => x.Id == adminId)
            .Include(o => o.Schools);
            var roles = await _adminManager.GetRolesAsync((Admin)admin);
            foreach(var role in roles){
                 await _adminManager.RemoveFromRoleAsync((Admin)admin, role);
            }
            _context.Users.Remove((Admin)admin);
            /////remember to test
           await _context.SaveChangesAsync();
           return Ok("Successfully deleted");
           //if this method doesn't work, then try tying the school to another admin
           //if this method doesn't work, then try tying the school to another admin
           //if this method doesn't work, then try tying the school to another admin
        }

        [HttpPost]
        [Route("addProprietor")]
        public async Task<IdentityResult> Principal(Proprietor model)
        {
            var proprietor = new Proprietor{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Email = model.Email,
                //LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,

            };

            try{
                 var result = await _proprietorManager.CreateAsync(proprietor, model.PasswordHash);
                 if(result.Succeeded)
                 {
                     await _proprietorManager.AddToRoleAsync(proprietor, "Proprietor");

                     return result;
                 }
                 else{
                     return result;
                 }
            }
            catch(Exception ex){
                throw ex;
            }
           
        }



    }

    public class SaveModel 
{
   public string name {get; set;}
}
}
