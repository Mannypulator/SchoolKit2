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
    public class StudentController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _userManager;

        public StudentController(SchoolKitContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //api/student/create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Student model)
        {
            var code = _context.StudentCodes
            .Where(x => x.Code == model.Code.Code)
            .FirstOrDefault();
            var time_check = (DateTime.UtcNow.AddHours(1) - code.Date).TotalDays;
            if(code != null && time_check < 2)
            {
                var regCount = _context.Schools
                .Where(x => x.SchoolID == model.SchoolID)
               .Select(x => x.RegNoCount).Single();
             var append = _context.Schools
              .Where(x => x.SchoolID == model.SchoolID)
             .Select(x => x.Append).Single();

             string s = append + regCount.ToString().PadLeft(4, '0');

             Student student = new Student{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                ClassArmID = model.Code.ClassArmID,
                SchoolID = model.Code.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                RegNo = s,
                UserName = s
              };
             try{
             
                var result = await _userManager.CreateAsync(student, model.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(student,"Student");
                    var regC = _context.Schools
                   .Where(x => x.SchoolID == student.SchoolID)
                   .Single();
                   regC.RegNoCount += 1;
                   regC.StudentCount += 1;

                   await _context.SaveChangesAsync();
                  
                   var term = _context.Terms
                       .Where(x => x.SchoolID == student.SchoolID && x.Current == true)
                       .Single();
                  EMethod eMethod = new EMethod();
                  eMethod.EnrollStudent(student, term, _context); 

                }
                return Ok(result);
             }
             catch(Exception ex)
             {
                throw ex;
             }
              
            }
             else{
                  return BadRequest(new {Message = "Invalid Code"});
             }
        }

        //api/student/deleteStudent
        

    }
}
