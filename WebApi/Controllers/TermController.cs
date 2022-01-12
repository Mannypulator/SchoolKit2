using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Methods;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/term")]
    [Authorize(Roles = "Admin,Principal,Proprietor")]
    public class TermController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<Principal> _principalManager;

        public TermController(SchoolKitContext context, UserManager<Student> studentManager, UserManager<Principal> principalManager)
        {
            _context = context;
            _studentManager = studentManager;
            _principalManager = principalManager;
        }

        [HttpPost]
        [Route("newTerm")]
        //authorize for principal
        public async Task<IActionResult> StartTerm(Term term)
        {
            //confirm that there are fees for a term before starting term
            //or add a method for confirmation
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var principal = await _principalManager.FindByIdAsync(userId);

            if(term.Current || term.Completed){
                return BadRequest( new {Message = "This Term has already began or has been completed"});
            }

            var terms = _context.Sessions
            .Where(x => x.SchoolID == principal.SchoolID)
            .Include(f => f.Terms)
            .SelectMany(t => t.Terms);

            var termIDs = terms
            .Select(x => x.TermID)
            .OrderBy(x => x)
            .ToList();
            
            var i = termIDs.IndexOf(term.TermID);

            var students = _studentManager.Users
            .Where(x => x.SchoolID == principal.SchoolID && !x.HasGraduated);

            if(i != 0)
            {
                var prevTermID = termIDs[i-1];

                var prevTerm = terms
                .Where(c => c.TermID == prevTermID)
                .FirstOrDefault();

                if(prevTerm.Current)
                {
                    return BadRequest( new {Message = "Please end current term before starting a new one"});
                }

                EMethod eMethod = new EMethod(); 
                 var schoolPackages = _context.SchoolPackages
                .Where(x => x.SchoolID == principal.SchoolID);

                if(schoolPackages.Any(x => x.Package == Package.Finance))
                {
                    foreach(var student in students)
                    {
                        eMethod.EnrollStudent(student, term, _context, prevTerm);
                        /*var payments = _context.Fees
                        .Where(t => t.TermID == term.TermID)
                        .Include(c => c.FeePayments)
                        .SelectMany(c => c.FeePayments)
                        .Where(x => x.StudentID == student.Id);*/ //on hold

                        var studentFees = _context.Fees
                        .Where(x => x.TermID == term.TermID)
                        .Include(c => c.StudentFees)
                        .ThenInclude(c => c.Fee)
                        .SelectMany(m => m.StudentFees)
                        .Where(f => f.StudentID == student.Id);
                        
                        var total = studentFees.Sum(f => f.Fee.Amount);
                        student.Balance = student.Balance - total;
                        await _studentManager.UpdateAsync(student);
                    }
                }
                else
                {
                    foreach(var student in students)
                    {
                        eMethod.EnrollStudent(student, term, _context, prevTerm);
                    }
                }
                

                FinanceMethods fMethod = new FinanceMethods();
                //fMethod.AssignFees(term, principal.SchoolID, prevTerm, _studentManager, _context);
                
            }
            else{

                EMethod eMethod = new EMethod(); 

                var schoolPackages = _context.SchoolPackages
                .Where(x => x.SchoolID == principal.SchoolID);

                if(schoolPackages.Any(x => x.Package == Package.Finance))
                {
                    foreach(var student in students)
                    {
                        eMethod.EnrollStudent(student, term, _context);
                        /*var payments = _context.Fees
                        .Where(t => t.TermID == term.TermID)
                        .Include(c => c.FeePayments)
                        .SelectMany(c => c.FeePayments)
                        .Where(x => x.StudentID == student.Id);*/ //on hold

                        var studentFees = _context.Fees
                        .Where(x => x.TermID == term.TermID)
                        .Include(c => c.StudentFees)
                        .ThenInclude(c => c.Fee)
                        .SelectMany(m => m.StudentFees)
                        .Where(f => f.StudentID == student.Id);
                        
                        var total = studentFees.Sum(f => f.Fee.Amount);
                        student.Balance = student.Balance - total;
                        await _studentManager.UpdateAsync(student);
                    }
                }
                else{
                    foreach(var student in students)
                    {
                        eMethod.EnrollStudent(student, term, _context);
                    }
                }
                
                FinanceMethods fMethod = new FinanceMethods();
                //fMethod.AssignFees(term, principal.SchoolID, _studentManager, _context);
            }
            if(term.Label == TermLabel.FirstTerm){
                term.Current = true;
                term.Completed = false;
                var session = term.Session;
                session.Current = true;
                session.Completed = false;
                _context.Terms.Update(term);
                _context.Sessions.Update(session);
                await _context.SaveChangesAsync();
            }
            else{
                term.Current = true;
                term.Completed = false;
                _context.Terms.Update(term);
                await _context.SaveChangesAsync();
            }
           
            
            return Ok();  
        }

        [HttpPost]
        [Route("endTerm")]
        //authorize for principals
        public async Task<IActionResult> EndTerm(Term term)//remember to pass the session along
        {
            if (term.Current){
                
            /// check to make sure there's an active term
                ResultMethods.CompileResults(term.Session.SchoolID, _context, _studentManager, term);
                if( term.Label == TermLabel.ThirdTerm)
                {
                    term.Current = false;
                    term.Completed = true;
                    var session = term.Session;
                    session.Current = false;
                    session.Completed = true;
                    _context.Terms.Update(term);
                    _context.Sessions.Update(session);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else{
                    term.Current = false;
                    term.Completed = true;
                    _context.Terms.Update(term);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                

            }
            else{
                return BadRequest(new {Message = "This term has already been ended"});
            }
        }

        [HttpPost]
        [Route("createSession")]
        //authorize for principals
        public async Task<IActionResult> CreateSession(SessionModel session)
        {
            var sessions = _context.Sessions
            .Where(x => x.SchoolID == session.SchoolID && !x.Completed);

            int sessionsCount = sessions.Count();
            if(sessionsCount < 2)
            {
            
                    if(session.SessionName != null)
                    {
                        var sessionModel = new Session{
                            SchoolID = session.SchoolID,
                            SessionName = session.SessionName
                            
                        };
                        await _context.Sessions.AddAsync(sessionModel);
                        await _context.SaveChangesAsync();

                        var schoolPackages = _context.SchoolPackages
                        .Where(x => x.SchoolID == sessionModel.SchoolID);

                        if(schoolPackages.Any(x => x.Package == Package.Finance))
                        {
                            if(sessionsCount == 0)
                            {
                                var lastTerm = _context.Sessions
                                .Where(x => x.SchoolID == sessionModel.SchoolID)
                                .Include(x => x.Terms)
                                .ThenInclude(x => x.Fees)
                                .ThenInclude(x => x.StudentFees)
                                .ThenInclude(y => y.Student)
                                .SelectMany(x => x.Terms)
                                .OrderBy(x => x.TermID )
                                .LastOrDefault();

                                var firstTerm = new Term{
                                Label = TermLabel.FirstTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                                .Select(x => new Fee{
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                                .Where(g => !g.Student.HasGraduated)
                                .Select(y => new StudentFee{
                                StudentID = y.StudentID, 
                                AmountOwed  = x.Amount,
                                }).ToList()
                                }).ToList()
                                };

                                var secondTerm = new Term{
                                Label = TermLabel.SecondTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                                .Select(x => new Fee{
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                                .Where(g => !g.Student.HasGraduated)
                                .Select(y => new StudentFee{
                                StudentID = y.StudentID, 
                                AmountOwed  = x.Amount,
                                }).ToList()
                                }).ToList()
                                };

                                var thirdTerm = new Term{
                                Label = TermLabel.ThirdTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                                .Select(x => new Fee{
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                                .Where(g => !g.Student.HasGraduated)
                                .Select(y => new StudentFee{
                                StudentID = y.StudentID, 
                                AmountOwed  = x.Amount,
                                }).ToList()
                                }).ToList()
                                };
                                await _context.AddAsync(firstTerm);
                                await _context.AddAsync(secondTerm);
                                await _context.AddAsync(thirdTerm);
                                await _context.SaveChangesAsync();

                            }
                            else if(sessionsCount == 1)
                            {
                                var lastTerm = _context.Sessions
                                .Where(x => x.SchoolID == sessionModel.SchoolID)
                                .Include(x => x.Terms)
                                .ThenInclude(x => x.Fees)
                                .ThenInclude(x => x.StudentFees)
                                .ThenInclude(y => y.Student)
                                .ThenInclude(g => g.ClassArm)
                                .SelectMany(x => x.Terms)
                                .OrderBy(x => x.TermID )
                                .LastOrDefault();

                                var firstTerm = new Term{
                                Label = TermLabel.FirstTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                                .Select(x => new Fee{
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                                .Where(g => !g.Student.HasGraduated && !(g.Student.ClassArm.Class == Class.Primary6 || g.Student.ClassArm.Class == Class.SSS3))
                                .Select(y => new StudentFee{
                                StudentID = y.StudentID, 
                                AmountOwed  = x.Amount,
                                }).ToList()
                                }).ToList()
                                };

                                var secondTerm = new Term{
                                Label = TermLabel.SecondTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                                .Select(x => new Fee{
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                                .Where(g => !g.Student.HasGraduated && !(g.Student.ClassArm.Class == Class.Primary6 || g.Student.ClassArm.Class == Class.SSS3))
                                .Select(y => new StudentFee{
                                StudentID = y.StudentID, 
                                AmountOwed  = x.Amount,
                                }).ToList()
                                }).ToList()
                                };

                                var thirdTerm = new Term{
                                Label = TermLabel.ThirdTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                                .Select(x => new Fee{
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                                .Where(g => !g.Student.HasGraduated && !(g.Student.ClassArm.Class == Class.Primary6 || g.Student.ClassArm.Class == Class.SSS3))
                                .Select(y => new StudentFee{
                                StudentID = y.StudentID, 
                                AmountOwed  = x.Amount,
                                }).ToList()
                                }).ToList()
                                };
                                await _context.AddAsync(firstTerm);
                                await _context.AddAsync(secondTerm);
                                await _context.AddAsync(thirdTerm);
                                await _context.SaveChangesAsync();
                            }
                            
                        }
                        else{
                            var firstTerm = new Term{
                            Label = TermLabel.FirstTerm,
                            SessionID = sessionModel.SessionID
                            };
                            var secondTerm = new Term{
                            Label = TermLabel.SecondTerm,
                            SessionID = sessionModel.SessionID
                            };
                            var thirdTerm = new Term{
                            Label = TermLabel.ThirdTerm,
                            SessionID = sessionModel.SessionID
                            };

                            await _context.AddAsync(firstTerm);
                            await _context.AddAsync(secondTerm);
                            await _context.AddAsync(thirdTerm);
                            await _context.SaveChangesAsync();

                        }
                
                
                    }
                    return Ok();

            }
            else{
                return BadRequest(new {Message = "Unable to create more sessions as there are still two sessions yet to be completed"});
            }
        }

        [HttpPost]
        [Route("getSessions")]
        public async Task<IActionResult> GetSessions([FromBody] ClassId i)
        {
            var schoolId = 0;

            if (i.schoolID != 0)
            {
                schoolId = i.schoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                schoolId = principal.SchoolID;

            }

            var sessions = await _context.Sessions
            .Where(x => x.SchoolID == schoolId)
            .Include(x => x.Terms)
            .ToListAsync();

            return Ok(sessions);

        }

    }
}
