using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/class")]
    public class ClassController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _userManager;
        public Timer timer;
        public StateID ID;

        public ClassController(SchoolKitContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
             ID = new StateID();
             
            
        }

        // GET: Class
        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult<IEnumerable<Class>>> Index(string[] model)
        {
            /*foreach(var name in model){
                _context.Classes.Add( new Class{
                    ClassName = name
                });
            }
            await _context.SaveChangesAsync();*/
            return Ok();
            

        }

        [HttpPost]
        [Route("fill")]
       // [Authorize]
        public async Task<IActionResult> Fill([FromBody]string tt)
        {
            string u = "r";
            string y = "e";
            string t = null;
            List<string> f = new List<string>{u, y, t};
            return Ok();
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

        public async void AddLGA(object TestID)
        {
            var id = TestID as StateID;

            var lga = new LGA{
                Name = "Ezza",
                StateID = id.Id
            };
            using (SchoolKitContext context = new SchoolKitContext()){
               // await context.LGAs.AddAsync(lga);
                await context.SaveChangesAsync();
            }
           timer.Dispose();
           
        }
     }
}

public class StateID{
    public int Id;
    
}
