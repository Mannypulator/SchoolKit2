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
        private readonly UserManager<Principal> _principalManager;
        public Timer timer;
        public StateID ID;

        public ClassController(SchoolKitContext context, UserManager<Student> userManager, UserManager<Principal> principalManager)
        {
            _context = context;
            _userManager = userManager;
            _principalManager = principalManager;
             ID = new StateID();
             
            
        }

        // GET: Class
        [HttpPost]
        [Route("fill")]
        public async Task<IActionResult> FillClass()
        {
            var classes = Enum.GetValues(typeof(Class));
            var arms = Enum.GetValues(typeof(Arms));

           foreach(var _class in classes){
              foreach(var arm in arms){
                  await _context.ClassArms.AddAsync( new ClassArm{
                      Arm = (Arms)arm,
                      Class = (Class)_class
                  });
               }
           }
           await _context.SaveChangesAsync();
           return Ok(new {message="classArms created successfully"});

        }

        [HttpPost]
        [Route("fil")]
       // [Authorize]
        public async Task<IActionResult> Fill([FromBody]string tt)
        {
            var dateTime = DateTime.Now;
            Random randoom = new Random();
            List<DateTime> playdate = new List<DateTime>();
            for(var i = 0; i<10; i++)
            {
                var t = randoom.Next(1,25);
                playdate.Add(dateTime.AddDays(-t));
            }
            var f = playdate.OrderBy(x => x.Date).GroupBy(x => x.AddDays(-(int)x.DayOfWeek ));
            var sunday = dateTime.AddDays(-(int)dateTime.DayOfWeek);
            return Ok(f);
        }

         [HttpPost]
         [Route("getClass")]
         //[Authorize]
        public async Task<IActionResult> GetSpecificClass(ClassId i)
        { var id=0;

            if (i.schoolID != 0){
                id = i.schoolID;
            }
            else{
                 var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var  principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
            }
             var allstudents = _userManager.Users
            .Where(x => x.SchoolID == id && x.HasGraduated == false)
            .Include(x => x.ClassArm);
            
            var classArms = allstudents
                .Select(r => r.ClassArm).ToHashSet();
                
            var classes = classArms.OrderBy(x => x.Class)
            .ToList();
            var classNames = new List<ClassName>();
            foreach(var _class in classes){
               var className = new ClassName{
                   ClassArmID = _class.ClassArmID,
                   Name = Enum.GetName(typeof(Class), _class.Class),
                   Arm = Enum.GetName(typeof(Arms), _class.Arm)
               };
               classNames.Add(className);
            }
            return Ok(classNames);
        }

        [HttpPost]
         [Route("getClassArms")]
         [Authorize]
        public async Task<IActionResult> GetClassArms(ClassId i)
        {   var id=0;

            if (i.schoolID != 0){
                id = i.schoolID;
            }
            else{
                 var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var  principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
                
            }

            var school = await _context.Schools
            .Where(x => x.SchoolID == id)
            .FirstOrDefaultAsync();
            
            var classes = new List<ClassArm>();
            var classArms = await _context.ClassArms.ToListAsync();
            if(school.Type == SchoolType.Primary){
                classes = classArms
                .Where(x=> x.Class <= Class.Primary6).ToList();
            }
            else{
                classes = classArms
                .Where(x=> x.Class > Class.Primary6).ToList();
            }
                
                
            classes = classes.OrderBy(x => x.Class)
            .ToList();
            var classNames = new List<ClassName>();
            foreach(var _class in classes){
               var className = new ClassName{
                   ClassArmID = _class.ClassArmID,
                   Name = Enum.GetName(typeof(Class), _class.Class),
                   Arm = Enum.GetName(typeof(Arms), _class.Arm)
               };
               classNames.Add(className);
            }
            return Ok(classNames);
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
    
