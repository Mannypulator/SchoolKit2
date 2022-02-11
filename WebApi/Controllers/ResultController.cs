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
    [Route("api/result")]
    public class ResultController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<Principal> _principalManager;
        private readonly UserManager<Teacher> _teacherManager;

        public ResultController(SchoolKitContext context, UserManager<Student> studentManager, UserManager<Principal> principalManager, UserManager<Teacher> teacherManager)
        {
            _context = context;
            _studentManager = studentManager;
            _principalManager = principalManager;
            _teacherManager = teacherManager;
        }


        [HttpGet]
        [Route("getLatestResult")]
        //authorise for students
        public async Task<IActionResult> GetLastestResult()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var student = await _studentManager.FindByIdAsync(userID);

            var term = await _context.Sessions
            .Where(i => i.SchoolID == student.SchoolID && (i.Current || i.Completed))
            .Include(t => t.Terms)
            .ThenInclude(t => t.Session)
            .SelectMany(r => r.Terms)
            .Where(x => x.Completed)
            .OrderBy(x => x.TermID)
            .LastOrDefaultAsync();

            var schoolPackages = _context.SchoolPackages
             .Where(x => x.SchoolID == student.SchoolID);

            if (schoolPackages.Any(x => x.Package == Package.Finance))
            {
                var notOwing = _context.Fees
                .Where(d => d.TermID == term.TermID)
                .Include(t => t.StudentFees.Where(d => d.StudentID == student.Id))
                .SelectMany(g => g.StudentFees)
                .All(c => c.AmountOwed <= 0);

                if (notOwing)
                {
                    if (term.Label != TermLabel.ThirdTerm)
                    {
                        List<ResultModel> resModel = new List<ResultModel>();
                        resModel.Add(await ResultMethods.Results(term, student.Id, _context));
                        return Ok(resModel);
                    }
                    else
                    {
                        List<ResultModel> resModel = new List<ResultModel>();
                        resModel.Add(await ResultMethods.Results(term, student.Id, _context));
                        resModel.Add(await ResultMethods.AResults(term, student.Id, _context));

                        return Ok(resModel);
                    }
                }
                else
                {
                    if (term.Label != TermLabel.ThirdTerm)
                    {
                        List<ResultModel> resModel = new List<ResultModel>();
                        resModel.Add(await ResultMethods.Incomplete_Result(term, student, _context));
                        return Ok(resModel);
                    }
                    else
                    {
                        List<ResultModel> resModel = new List<ResultModel>();
                        resModel.Add(await ResultMethods.Incomplete_Result(term, student, _context));
                        resModel.Add(await ResultMethods.Incomplete_AResult(term, student, _context));

                        return Ok(resModel);
                    }
                }

            }
            else
            {
                if (term.Label != TermLabel.ThirdTerm)
                {
                    List<ResultModel> resModel = new List<ResultModel>();
                    resModel.Add(await ResultMethods.Results(term, student.Id, _context));
                    return Ok(resModel);
                }
                else
                {
                    List<ResultModel> resModel = new List<ResultModel>();
                    resModel.Add(await ResultMethods.Results(term, student.Id, _context));
                    resModel.Add(await ResultMethods.AResults(term, student.Id, _context));

                    return Ok(resModel);
                }
            }

        }

        [HttpGet]
        [Route("getAllResults")]
        //authorise for students
        public async Task<IActionResult> GetAllResults()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var student = await _studentManager.FindByIdAsync(userID);

            var terms = _context.Sessions
            .Where(i => i.SchoolID == student.SchoolID && (i.Current || i.Completed))
            .Include(t => t.Terms)
            .ThenInclude(t => t.Session)
            .SelectMany(r => r.Terms)
            .Where(x => x.Completed)
            .OrderBy(x => x.TermID);

            //check if student is owing
            var schoolPackages = _context.SchoolPackages
             .Where(x => x.SchoolID == student.SchoolID);
            List<ResultModel> results = new List<ResultModel>();

            if (schoolPackages.Any(x => x.Package == Package.Finance))
            {
                foreach (var term in terms)
                {
                    var notOwing = _context.Fees
                    .Where(d => d.TermID == term.TermID)
                    .Include(t => t.StudentFees.Where(d => d.StudentID == student.Id))
                    .SelectMany(g => g.StudentFees)
                    .All(c => c.AmountOwed <= 0);

                    if (notOwing)
                    {
                        if (term.Label != TermLabel.ThirdTerm)
                        {
                            results.Add(await ResultMethods.Results(term, student.Id, _context));
                        }
                        else
                        {
                            results.Add(await ResultMethods.Results(term, student.Id, _context));
                            results.Add(await ResultMethods.AResults(term, student.Id, _context));
                        }
                    }
                    else
                    {
                        if (term.Label != TermLabel.ThirdTerm)
                        {

                            results.Add(await ResultMethods.Incomplete_Result(term, student, _context));

                        }
                        else
                        {
                            results.Add(await ResultMethods.Incomplete_Result(term, student, _context));
                            results.Add(await ResultMethods.Incomplete_AResult(term, student, _context));
                        }

                    }

                }

            }
            else
            {
                foreach (var term in terms)
                {
                    if (term.Label != TermLabel.ThirdTerm)
                    {
                        results.Add(await ResultMethods.Results(term, student.Id, _context));
                    }
                    else
                    {
                        results.Add(await ResultMethods.Results(term, student.Id, _context));
                        results.Add(await ResultMethods.AResults(term, student.Id, _context));
                    }


                }
            }

            return Ok(results);
        }

        [HttpGet]
        [Route("getStudentResult")]
        //authorise for principal
        public async Task<IActionResult> GetStudentResult(string Id)
        {
            
            var student = await _studentManager.FindByIdAsync(Id);
            int schoolID = student.SchoolID;

            var term = await _context.Sessions
            .Where(i => i.SchoolID == schoolID && i.Current)
            .Include(t => t.Terms)
            .ThenInclude(f => f.Session)
            .SelectMany(r => r.Terms)
            .Where(x => x.Current)
            .SingleOrDefaultAsync();

            if (term.Label != TermLabel.ThirdTerm)
            {
                List<ResultModel> resModel = new List<ResultModel>();
                resModel.Add(await ResultMethods.Results(term, Id, _context));
                return Ok(resModel);
            }
            else
            {
                List<ResultModel> resModel = new List<ResultModel>();
                resModel.Add(await ResultMethods.Results(term, Id, _context));
                resModel.Add(await ResultMethods.AResults(term, Id, _context));

                return Ok(resModel);
            }


        }

        [HttpPost]
        [Route("updatePComment")]
        //authorise for students
        public async Task<IActionResult> UpdateComment(CommentUpdateModel obj)
        {
           var result = await _context.Results
           .Where(x => x.ResultID == obj.ResultID)
           .SingleOrDefaultAsync();

           result.PrincipalComment = obj.PComment;
           await _context.SaveChangesAsync();
           return Ok();

        }

        [HttpPost]
        [Route("updateTComment")]
        //authorise for students
        public async Task<IActionResult> UpdateTComment(CommentUpdateModel obj)
        {
           var result = await _context.Results
           .Where(x => x.ResultID == obj.ResultID)
           .SingleOrDefaultAsync();

           result.TeacherComment = obj.PComment;
           await _context.SaveChangesAsync();
           return Ok();

        }

        public async Task<int> getTeacherSchoolID(string Id){
            var teacher = await _teacherManager.FindByIdAsync(Id);
            return teacher.SchoolID;
        }


    }

    
}
