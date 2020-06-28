using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Methods;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/enrollment")]
    public class EnrollmentController : Controller
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _userManager;

        public EnrollmentController(SchoolKitContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("begin")]

        public async Task<IActionResult> Begin(int id)
        {
            var students = _userManager.Users
            .Where(x => x.SchoolID == id);
             var term = _context.Terms
                       .Where(x => x.Current == true)
                       .Single();
            foreach(var student in students)
            {
                await Task.Run(()=>
                EMethod.EnrollStudent(student, term, _context)
                );
                   
            }
            return Ok();
        }

        [HttpPost]
        [Route("my")]
        public IEnumerable<Enrollment> MyEnrollments(string id)
        {
            int termid = _context.Terms.Where(d => d.Current == true)
            .Select(p => p.TermID)
            .Single();

           var enrollments = _context.Enrollments
           .Where(o => o.StudentID == id && o.TermID == termid).ToList();

            return enrollments;
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Enrollment enrollment)
        {
            await Task.Run(() => {
                _context.Enrollments.Update(enrollment);
            });
            
            return Ok();
        }

    }      
}
