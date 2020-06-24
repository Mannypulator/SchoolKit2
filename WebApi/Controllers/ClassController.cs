using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ClassController(SchoolKitContext context)
        {
            _context = context;
        }

        // GET: Class
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Index(string[] model)
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
            var classes = _context.Classes.OrderBy(x => x.ClassID);
            var arms = Enum.GetValues(typeof(Arms)).Cast<Arms>();
            
            foreach(var Class in classes)
            {
                foreach(var arm in arms){
                    _context.ClassArms.Add( new ClassArm{
                        ClassID = Class.ClassID,
                        Arm = arm
                    });
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: Class/Details/5
     }
}
