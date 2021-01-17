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
    [Route("api/result")]
    public class ResultController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _studentManager;

        public ResultController(SchoolKitContext context, UserManager<Student> studentManager)
        {
            _context = context;
            _studentManager = studentManager;
        }

        [HttpPost]
        [Route("compile")]
        //authorize for principals
        public async Task<IActionResult> Compile(int id)
        {
            var students = _studentManager.Users
            .Where(x => x.SchoolID == id);

            int termId = _context.Terms
            .Where(d => d.SchoolID == id & d.Current == true)
            .Select(r => r.TermID).SingleOrDefault();

            foreach(var student in students)
            {
                var enrollments = _context.Enrollments
                .Where(u => u.StudentID == student.Id && u.TermID == termId);//get enrollments for current student

               int total = enrollments.Sum(j => j.Total); //sum of all enrollments total scores for current student
               int count = enrollments.Count();
               double average = (double) total/count;

               var result = new Result{
                   StudentID = student.Id,
                   TermID = termId,
                   Total = total,
                   Average = average
               };
               await _context.Results.AddAsync(result); //add result for current student
            }
            await _context.SaveChangesAsync();

            var classArms = _studentManager.Users
            .Where(u => u.SchoolID == id)
            .Select(r => r.ClassArmID).ToHashSet(); //get classArms id for students in current school

           foreach(var classArm in classArms)
           {
                var classResults = _context.Results.Include(x => x.Student)
                 .Where(i => i.Student.SchoolID == id 
                 && i.TermID == termId 
                 && i.Student.ClassArmID == classArm)
                 .OrderBy(x => x.Average);

                int count = 1;
                foreach(var result in classResults)
                {
                    result.ClassPosition = count;
                    _context.Update(result);
                    count++;  
                }
                 await _context.SaveChangesAsync();// if positions don't show, remember to test this
           }
            return Ok();
        }

        [HttpGet]
        [Route("getLatestResult")]
        //authorise for students
        public async Task<ResultModel> GetLastestResult()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var  student = await _studentManager.FindByIdAsync(userID);
           var term = await _context.Terms
           .Where(i => i.SchoolID == student.SchoolID && i.Current != true)
           .OrderBy(x => x.TermID)
           .Select(t => t.TermID)
           .LastOrDefaultAsync();

           var result = await _context.Results
           .Where(d => d.StudentID == userID && d.TermID == term)
           .SingleOrDefaultAsync();

           var enrollments = await _context.Enrollments
           .Include(x=> x.ClassSubject)
           .ThenInclude(f => f.Subject)
           .Where(d => d.StudentID == userID && d.TermID == term)
           .Select(d => new EnrollmentModel{
               SubjectName = d.ClassSubject.Subject.Title,
               CA = d.CA,
               Exam = d.Exam,
               Grade =d.grade,
              }).ToListAsync();

            var stResult = new ResultModel
            {
                Result =result,
                Enrollments = enrollments

            };

             return stResult;
        }

        [HttpGet]
        [Route("getAllResults")]
        //authorise for students
        public async Task<IActionResult> GetAllResults()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var  student = await _studentManager.FindByIdAsync(userID);

           var terms =  _context.Terms
           .Where(i => i.SchoolID == student.SchoolID && i.Current != true)
           .Select(t => t.TermID)
           .OrderBy(x => x)
           .ToList();

           List<ResultModel> results = new List<ResultModel>();
           foreach(var term in terms)
           {
                var result = await _context.Results
                .Where(d => d.StudentID == userID && d.TermID == term)
                .SingleOrDefaultAsync();

                var enrollments = await _context.Enrollments
                .Include(x=> x.ClassSubject)
                .ThenInclude(f => f.Subject)
                .Where(d => d.StudentID == userID && d.TermID == term)
                .Select(d => new EnrollmentModel{
                    SubjectName = d.ClassSubject.Subject.Title,
                    CA = d.CA,
                    Exam = d.Exam,
                    Grade =d.grade,
                    }).ToListAsync();

                var stResult = new ResultModel
                {
                    Result =result,
                    Enrollments = enrollments

                };
                results.Add(stResult);
           }

           
             return Ok(results);
        }



    }
}
