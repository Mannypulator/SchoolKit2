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
               Grade =d.Grade,
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
           .OrderBy(x => x.TermID)
           .ToList();

           List<ResultModel> results = new List<ResultModel>();
           foreach(var term in terms)
           {
               var result = _context.Results
                        .Where(d => d.StudentID == userID && d.TermID == term.TermID);

                foreach(var res in result){
                    if(res.Type == ResultType.Term)
                    {
                        var enrollments = await _context.Enrollments
                            .Include(x => x.ClassSubject)
                            .ThenInclude(f => f.Subject)
                            .Where(d => d.StudentID == userID && d.TermID == term.TermID)
                            .Select(d => new EnrollmentModel{
                                SubjectName = d.ClassSubject.Subject.Title,
                                CA = d.CA,
                                Exam = d.Exam,
                                Total = d.Total,
                                Grade =d.Grade,
                                }).ToListAsync();

                        var stResult = new ResultModel
                        {
                            Result = res,
                            Enrollments = enrollments

                        };
                        results.Add(stResult);
                    }
                    else if(res.Type == ResultType.Annual)
                    {
                        var annualEnrollments = await _context.AnnualEnrollments
                            .Include(x => x.ClassSubject)
                            .ThenInclude(f => f.Subject)
                            .Where(d => d.StudentID == userID && d.TermID == term.TermID)
                            .Select(d => new AnnualEnrollmentModel{
                                SubjectName = d.ClassSubject.Subject.Title,
                                FirstTerm = d.FirstTerm,
                                SecondTerm = d.SecondTerm,
                                ThirdTerm = d.ThirdTerm,
                                Total = d.Total,
                                Grade =d.Grade,
                                }).ToListAsync();
                        var AnnualResult = new ResultModel
                        {
                            Result = res,
                            AnnualEnrollments = annualEnrollments

                        };
                        results.Add(AnnualResult);
                    }
                }    

                
           }
           
             return Ok(results);
        }

    }
}
