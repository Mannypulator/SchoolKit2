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
        private readonly UserManager<Student> _userManager;

        public ResultController(SchoolKitContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("compile")]
        public async Task<IActionResult> Compile(int id)
        {
            var students = _userManager.Users
            .Where(x => x.SchoolID == id);

            int termId = _context.Terms
            .Where(d => d.Current == true)
            .Select(r => r.TermID).Single();

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

            var classArms = _userManager.Users
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
                    count++;  
                }
                 await _context.SaveChangesAsync();// if positions don't show, remember to test this
           }
            return Ok();
        }

        [HttpPost]
        [Route("get")]

        public async Task<ActionResult<ResultModel>> Get(string stId)
        {
           var term = _context.Terms
           .Where(i => i.Current == true)
           .Select(t => t.TermID)
           .Single();

           var result = _context.Results
           .Where(d => d.StudentID == stId && d.TermID == term)
           .Single();

           var enrollments = _context.Enrollments
           .Where(d => d.StudentID == stId && d.TermID == term).ToList();

            var stResult = new ResultModel
            {
                Result =result,
                Enrollments = enrollments

            };

          return  await Task.Run(()=>{
                return stResult;
            });
            
        }


    }
}
