using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/class")]
    public class ClassController : Controller
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _userManager;

        public ClassController(SchoolKitContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Class
        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult<IEnumerable<Class>>> Index(string[] model)
        {
            foreach(var name in model){
                _context.Classes.Add( new Class{
                    ClassName = name
                });
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("fill")]
        public async Task<IActionResult> Fill()
        {
             var classArms = _userManager.Users
            .Where(u => u.SchoolID == 1)
            .Select(r => r.ClassArmID).ToList();

               
                
            return Ok(classArms);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<IEnumerable<Class>>> Del()
        {
            var classArms = _context.ClassArms;
           foreach(var arm in classArms){
               _context.ClassArms.Remove(arm);
           }
            await _context.SaveChangesAsync();
       
            return Ok();
        }
     }
}
