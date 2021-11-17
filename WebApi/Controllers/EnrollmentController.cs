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
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/enrollment")]
    [Authorize(Roles = "Teacher,Principal,Proprietor")]
    public class EnrollmentController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<Teacher> _teacherManager;
        private readonly UserManager<Principal> _principalManager;
        public EnrollmentController(SchoolKitContext context, UserManager<Student> studentManager, UserManager<Teacher> teacherManager)
        {
            _context = context;
            _studentManager = studentManager;
            _teacherManager = teacherManager;
        }



        [HttpGet]
        [Route("my")]
        public async Task<IActionResult> MyEnrollments(string id)
        {
            int termid = _context.Terms.Where(d => d.Current == true)
            .Select(p => p.TermID)
            .Single();

           var enrollments = await _context.Enrollments
           .Where(o => o.StudentID == id && o.TermID == termid).ToListAsync();

            return Ok(enrollments);
        }


        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(ReturnEnrollment model)
        {
            var enrollment = await _context.Enrollments
            .Where(x => x.EnrollmentID == model.EnrollmentID)
            .FirstOrDefaultAsync();
            enrollment.FirstCA = model.FirstCA;
            enrollment.SecondCA = model.SecondCA;
            enrollment.ThridCA = model.ThirdCA;
            enrollment.Exam = model.Exam;
            enrollment.CA = enrollment.FirstCA + enrollment.SecondCA + enrollment.ThridCA;
               enrollment.Total = enrollment.CA + enrollment.Exam;
               if(enrollment.Total >= 70){
                   enrollment.Grade = Grade.A;
               }
               else if(enrollment.Total >= 60){
                   enrollment.Grade = Grade.B;
               }
               else if(enrollment.Total >= 50){
                   enrollment.Grade = Grade.C;
               }
               else if(enrollment.Total >= 45){
                   enrollment.Grade = Grade.D;
               }
               else{
                   enrollment.Grade = Grade.F;
               }
                _context.Enrollments.Update(enrollment);
           
           await _context.SaveChangesAsync();     
            return Ok(enrollment);
        }

    }      
}
