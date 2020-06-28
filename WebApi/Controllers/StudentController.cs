using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Methods;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : Controller
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _userManager;

        public StudentController(SchoolKitContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Student model)
        {
            var regCount = _context.Schools
            .Where(x => x.SchoolID == model.SchoolID)
            .Select(x => x.RegNoCount).Single();
             var append = _context.Schools
            .Where(x => x.SchoolID == model.SchoolID)
            .Select(x => x.Append).Single();

           string s = append + regCount.ToString().PadLeft(4, '0');

            Student mod = new Student{
                FirstName = model.FirstName,
                LastName = model.LastName,
                ClassArmID = model.ClassArmID,
                SchoolID = model.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                RegNo = s,
                UserName = s
            };
            try{
                var result = await _userManager.CreateAsync(mod, model.PasswordHash);
                if (result.Succeeded)
                {
                    var regC = _context.Schools
                   .Where(x => x.SchoolID == model.SchoolID)
                   .Single();
                   regC.RegNoCount += 1;
                   regC.StudentCount += 1;

                   await _context.SaveChangesAsync();
                  
                   var term = _context.Terms
                       .Where(x => x.Current == true)
                       .Single();

                   EMethod.EnrollStudent(mod, term, _context); 

                }
                return Ok(result);
            }
            catch(Exception ex){
                throw ex;
            }
            
        }

    }
}
