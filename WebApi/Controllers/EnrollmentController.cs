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
            var teacherId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var teacher = await _teacherManager.FindByIdAsync(teacherId);
            var schoolId = teacher.SchoolID;

            var scoreScheme = await _context.ScoreSchemes
            .Where(x=> x.SchoolID == schoolId)
            .SingleOrDefaultAsync();

            var enrollment = await _context.Enrollments
            .Where(x => x.EnrollmentID == model.EnrollmentID)
            .FirstOrDefaultAsync();

            if(scoreScheme.Test1 >= model.FirstCA){
                enrollment.FirstCA = model.FirstCA;
            }
            else{
                return BadRequest(new {Message = "First test score must not be greater than" + scoreScheme.Test1});
            }

            if(scoreScheme.Test2 >= model.SecondCA){
                enrollment.SecondCA = model.SecondCA;
            }
            else{
                return BadRequest(new {Message = "Second test score must not be greater than" + scoreScheme.Test2});
            }

            if(scoreScheme.Test3 >= model.ThirdCA){
                enrollment.ThridCA = model.ThirdCA;
            }
            else{
                return BadRequest(new {Message = "Third test score must not be greater than" + scoreScheme.Test3});
            }

            if(scoreScheme.Exam >= model.Exam){
                enrollment.Exam = model.Exam;
            }
            else{
                return BadRequest(new {Message = "Exam score must not be greater than" + scoreScheme.Exam});
            }
            
            enrollment.CA = enrollment.FirstCA + enrollment.SecondCA + enrollment.ThridCA;
               enrollment.Total = enrollment.CA + enrollment.Exam;
               if(enrollment.Total >= scoreScheme.MinA && enrollment.Total <= scoreScheme.MaxA){
                   enrollment.Grade = Grade.A;
               }
               else if(enrollment.Total >= scoreScheme.MinB && enrollment.Total <= scoreScheme.MaxB){
                   enrollment.Grade = Grade.B;
               }
               else if(enrollment.Total >= scoreScheme.MinC && enrollment.Total <= scoreScheme.MaxC){
                   enrollment.Grade = Grade.C;
               }
               else if(enrollment.Total >= scoreScheme.MinD && enrollment.Total <= scoreScheme.MaxD){
                   enrollment.Grade = Grade.D;
               }
              else if(enrollment.Total >= scoreScheme.MinE && enrollment.Total <= scoreScheme.MaxE){
                   enrollment.Grade = Grade.E;
               }
               else if(enrollment.Total >= scoreScheme.MinP && enrollment.Total <= scoreScheme.MaxP){
                   enrollment.Grade = Grade.P;
               }
                else if(enrollment.Total >= scoreScheme.MinF && enrollment.Total <= scoreScheme.MaxF){
                   enrollment.Grade = Grade.F;
               }
                _context.Enrollments.Update(enrollment);
           
           await _context.SaveChangesAsync();     
            return Ok(enrollment);
        }

        [HttpGet]
        [Route("getUnEnrolled")]
        public async Task<IActionResult> getUnEnrolled(int id)
        {
             var teacherId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var teacher = await _teacherManager.FindByIdAsync(teacherId);
            var schoolId = teacher.SchoolID;

            var currentTerm = await _context.Sessions
            .Where(x=> x.SchoolID == schoolId && x.Current == true )
            .Include(t => t.Terms)
            .SelectMany(t => t.Terms)
            .Where(x=> x.Current == true)
            .Select(x=> x.TermID)
            .FirstOrDefaultAsync();

            var allStudents = await _context.ClassSubjects
            .Where(x=> x.ClassSubjectID == id)
            .Include(x => x.ClassArm)
            .ThenInclude(x=> x.Students)
            .SelectMany(x => x.ClassArm.Students)
            .Where(x => x.SchoolID == schoolId) 
            .ToListAsync();

             var enrolledStudents = await _context.Enrollments
            .Where(x => x.ClassSubjectID == id && x.TermID == currentTerm)
            .Include(x=> x.Student)
            .Select(x=> x.Student)
            .ToListAsync();

            var unEnrolled = allStudents
            .Where(x => enrolledStudents.All(s => s.Id != x.Id))
            .Select( x => new ReturnStudent{
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            })
            .ToList();
            return Ok(unEnrolled);
        }

    }      
}
