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
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Teacher> _userManager;

        public TeacherController(SchoolKitContext context, UserManager<Teacher> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Teacher model)
        {
            var teacher = new Teacher{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SchoolID = model.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email
            };

            try{
                 var result = await _userManager.CreateAsync(teacher, model.PasswordHash);
                 if(result.Succeeded)
                 {
                     foreach(var sub in model.TeacherSubjects)
                     {
                         var teacherSubject = new TeacherSubject{
                             ClassSubjectID = sub.ClassSubjectID,
                             TeacherID = teacher.Id
                         };
                         await _context.TeacherSubjects.AddAsync(teacherSubject);
                     }
                     await _context.SaveChangesAsync();
                 }
            }
            catch(Exception ex){
                throw ex;
            }
           
           return Ok();
        }

        [HttpGet]
        [Route("teacher")]

        public async Task<IActionResult> GetAction()
        {  
            return Ok();
        }

        // GET: Teacher/Details/5
    }
}
