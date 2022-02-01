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
    //[Authorize(Roles = "Admin,Principal,Proprietor")]
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
        [Route("startTerm")]
        //authorize for principal
        public async Task<IActionResult> StartTerm(ReturnTerm returnTerm)
        {
            //confirm that there are fees for a term before starting term
            //or add a method for confirmation
            var schoolId = 0;

            if (returnTerm.SchoolID != 0)
            {
                schoolId = returnTerm.SchoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                schoolId = principal.SchoolID;

            }

            var term = await _context.Terms
            .Where(x => x.TermID == returnTerm.TermID)
            .FirstOrDefaultAsync();

            if (term.Current || term.Completed)
            {
                return BadRequest(new { Message = "This Term has already began or has been completed" });
            }

            var sessions = await _context.Sessions
            .Where(x => x.SchoolID == schoolId)
            .Include(f => f.Terms)
            .ToListAsync();

            var currentSession = sessions
            .Where(x => x.Current)
            .FirstOrDefault();

            var terms = sessions
            .SelectMany(t => t.Terms);

            var termIDs = terms
            .Select(x => x.TermID)
            .OrderBy(x => x)
            .ToList();

            var i = termIDs.IndexOf(term.TermID);

            var students = _studentManager.Users
            .Where(x => x.SchoolID == schoolId && !x.HasGraduated);

            if (i != 0)
            {
                var prevTermID = termIDs[i - 1];

                var prevTerm = terms
                .Where(c => c.TermID == prevTermID)
                .FirstOrDefault();

                if (terms.Any(x => x.Current))// check if there are any other running terms
                {
                    return BadRequest(new { Message = "Please end current term before starting a new one" });
                }

                if (currentSession != null && !currentSession.Terms.Any(x => x.TermID == returnTerm.TermID))
                {
                    return BadRequest(new { Message = "Please end current Session before starting a term in another" });
                }

                EMethod eMethod = new EMethod();
                var schoolPackages = _context.SchoolPackages
               .Where(x => x.SchoolID == schoolId);

                if (schoolPackages.Any(x => x.Package == Package.Finance))
                {
                    foreach (var student in students)
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
                    foreach (var student in students)
                    {
                        eMethod.EnrollStudent(student, term, _context, prevTerm);
                    }
                }


                FinanceMethods fMethod = new FinanceMethods();
                //fMethod.AssignFees(term, principal.SchoolID, prevTerm, _studentManager, _context);

            }
            else
            {

                EMethod eMethod = new EMethod();

                var schoolPackages = _context.SchoolPackages
                .Where(x => x.SchoolID == schoolId);

                if (schoolPackages.Any(x => x.Package == Package.Finance))
                {
                    foreach (var student in students)
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
                else
                {
                    foreach (var student in students)
                    {
                        eMethod.EnrollStudent(student, term, _context);
                    }
                }

                FinanceMethods fMethod = new FinanceMethods();
                //fMethod.AssignFees(term, principal.SchoolID, _studentManager, _context);
            }


            term.Current = true;
            term.Completed = false;
            term.TermStart = DateTime.UtcNow.AddHours(1);
            var session = await _context.Sessions
            .Where(x => x.SessionID == term.SessionID)
            .FirstOrDefaultAsync();
            session.Current = true;
            session.Completed = false;
            session.SessionStart = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();


            return Ok();
        }

        [HttpPost]
        [Route("endTerm")]
        //authorize for principals
        public async Task<IActionResult> EndTerm(TermID termID)//remember to pass the session along
        {
            var term = await _context.Terms
            .Where(x => x.TermID == termID.Id)
            .Include(x => x.Session)
            .FirstOrDefaultAsync();

            if (term.Current)
            {

                /// check to make sure there's an active term
                ResultMethods.CompileResults(term.Session.SchoolID, _context, _studentManager, term);
                if (term.Label == TermLabel.ThirdTerm)
                {
                    term.Current = false;
                    term.Completed = true;
                    term.TermEnd = DateTime.UtcNow.AddHours(1);
                    var session = term.Session;
                    session.Current = false;
                    session.Completed = true;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    term.Current = false;
                    term.Completed = true;
                    term.TermEnd = DateTime.UtcNow.AddHours(1);
                    await _context.SaveChangesAsync();
                    return Ok();
                }


            }
            else
            {
                return BadRequest(new { Message = "This term has already been ended" });
            }
        }

        [HttpPost]
        [Route("compileResult")]
        //authorize for principals
        public async Task<IActionResult> CompileResult(TermID termID)//remember to pass the session along
        {
            var term = await _context.Terms
            .Where(x => x.TermID == termID.Id)
            .Include(x => x.Session)
            .FirstOrDefaultAsync();

            if (ResultRecordExits(term))
            {
                return BadRequest(new { Message = "Result already compiled for this term" });
            }

            if (term.Current)
            {

                /// check to make sure there's an active term
                ResultMethods.CompileResults(term.Session.SchoolID, _context, _studentManager, term);
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "Result for this term cannot be compiled" });
            }
        }

        public bool ResultRecordExits(Term term)
        {
            var record = _context.ResultRecords
            .Where(x => x.TermID == term.TermID)
            .FirstOrDefault();

            return record == null ? false : true;
        }

        [HttpPost]
        [Route("createSession")]
        //authorize for principals
        public async Task<IActionResult> CreateSession(SessionModel session)
        {
            var sessions = _context.Sessions
            .Where(x => x.SchoolID == session.SchoolID && !x.Completed);

            int sessionsCount = sessions.Count();
            if (sessionsCount < 2)
            {

                if (session.SessionName != null)
                {
                    var sessionModel = new Session
                    {
                        SchoolID = session.SchoolID,
                        SessionName = session.SessionName

                    };
                    await _context.Sessions.AddAsync(sessionModel);
                    await _context.SaveChangesAsync();

                    var schoolPackages = _context.SchoolPackages
                    .Where(x => x.SchoolID == sessionModel.SchoolID);

                    if (schoolPackages.Any(x => x.Package == Package.Finance))
                    {
                        if (sessionsCount == 0)
                        {
                            var lastTerm = _context.Sessions
                            .Where(x => x.SchoolID == sessionModel.SchoolID)
                            .Include(x => x.Terms)
                            .ThenInclude(x => x.Fees)
                            .ThenInclude(x => x.StudentFees)
                            .ThenInclude(y => y.Student)
                            .SelectMany(x => x.Terms)
                            .OrderBy(x => x.TermID)
                            .LastOrDefault();

                            var firstTerm = new Term
                            {
                                Label = TermLabel.FirstTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                            .Select(x => new Fee
                            {
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                            .Where(g => !g.Student.HasGraduated)
                            .Select(y => new StudentFee
                            {
                                StudentID = y.StudentID,
                                AmountOwed = x.Amount,
                            }).ToList()
                            }).ToList()
                            };

                            var secondTerm = new Term
                            {
                                Label = TermLabel.SecondTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                            .Select(x => new Fee
                            {
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                            .Where(g => !g.Student.HasGraduated)
                            .Select(y => new StudentFee
                            {
                                StudentID = y.StudentID,
                                AmountOwed = x.Amount,
                            }).ToList()
                            }).ToList()
                            };

                            var thirdTerm = new Term
                            {
                                Label = TermLabel.ThirdTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                            .Select(x => new Fee
                            {
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                            .Where(g => !g.Student.HasGraduated)
                            .Select(y => new StudentFee
                            {
                                StudentID = y.StudentID,
                                AmountOwed = x.Amount,
                            }).ToList()
                            }).ToList()
                            };
                            await _context.AddAsync(firstTerm);
                            await _context.AddAsync(secondTerm);
                            await _context.AddAsync(thirdTerm);
                            await _context.SaveChangesAsync();

                        }
                        else if (sessionsCount == 1)
                        {
                            var lastTerm = _context.Sessions
                            .Where(x => x.SchoolID == sessionModel.SchoolID)
                            .Include(x => x.Terms)
                            .ThenInclude(x => x.Fees)
                            .ThenInclude(x => x.StudentFees)
                            .ThenInclude(y => y.Student)
                            .ThenInclude(g => g.ClassArm)
                            .SelectMany(x => x.Terms)
                            .OrderBy(x => x.TermID)
                            .LastOrDefault();

                            var firstTerm = new Term
                            {
                                Label = TermLabel.FirstTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                            .Select(x => new Fee
                            {
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                            .Where(g => !g.Student.HasGraduated && !(g.Student.ClassArm.Class == Class.Primary6 || g.Student.ClassArm.Class == Class.SSS3))
                            .Select(y => new StudentFee
                            {
                                StudentID = y.StudentID,
                                AmountOwed = x.Amount,
                            }).ToList()
                            }).ToList()
                            };

                            var secondTerm = new Term
                            {
                                Label = TermLabel.SecondTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                            .Select(x => new Fee
                            {
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                            .Where(g => !g.Student.HasGraduated && !(g.Student.ClassArm.Class == Class.Primary6 || g.Student.ClassArm.Class == Class.SSS3))
                            .Select(y => new StudentFee
                            {
                                StudentID = y.StudentID,
                                AmountOwed = x.Amount,
                            }).ToList()
                            }).ToList()
                            };

                            var thirdTerm = new Term
                            {
                                Label = TermLabel.ThirdTerm,
                                SessionID = sessionModel.SessionID,
                                Fees = lastTerm.Fees
                            .Select(x => new Fee
                            {
                                FeeName = x.FeeName,
                                FeeType = x.FeeType,
                                Amount = x.Amount,
                                TotalAmountOwed = x.Amount * x.StudentFees.Count(),
                                TotalAmountExpeced = x.Amount * x.StudentFees.Count(),
                                StudentFees = x.StudentFees
                            .Where(g => !g.Student.HasGraduated && !(g.Student.ClassArm.Class == Class.Primary6 || g.Student.ClassArm.Class == Class.SSS3))
                            .Select(y => new StudentFee
                            {
                                StudentID = y.StudentID,
                                AmountOwed = x.Amount,
                            }).ToList()
                            }).ToList()
                            };
                            await _context.AddAsync(firstTerm);
                            await _context.AddAsync(secondTerm);
                            await _context.AddAsync(thirdTerm);
                            await _context.SaveChangesAsync();
                        }

                    }
                    else
                    {
                        var firstTerm = new Term
                        {
                            Label = TermLabel.FirstTerm,
                            SessionID = sessionModel.SessionID
                        };
                        var secondTerm = new Term
                        {
                            Label = TermLabel.SecondTerm,
                            SessionID = sessionModel.SessionID
                        };
                        var thirdTerm = new Term
                        {
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
            else
            {
                return BadRequest(new { Message = "Unable to create more sessions as there are still two sessions yet to be completed" });
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

        [HttpPost]
        [Route("currentSession")]
        public async Task<IActionResult> CurrentSession([FromBody] SO i)
        {
            var schoolId = 0;

            if (i.SchoolID != 0)
            {
                schoolId = i.SchoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                schoolId = principal.SchoolID;

            }

            var session = await _context.Sessions
            .Where(x => x.SchoolID == schoolId && x.Current)
            .Include(x => x.Terms)
            .FirstOrDefaultAsync();

            if (session != null)
            {
                var terms = session.Terms.ToList();

                for (int j = terms.Count - 1; j >= 0; j--)
                {
                    if (!terms[j].Current)
                    {
                        terms.RemoveAt(j); //remove terms that are not current
                    }
                }
                session.Terms = terms;
            }
            return Ok(session);

        }

        [HttpPost]
        [Route("delete")]
        //authorize for principals
        public async void delete()//remember to pass the session along
        {
            var sessions = _context.Sessions.Include(x => x.Terms).ToList();
            _context.RemoveRange(sessions);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("getStudents")]
        public async Task<IActionResult> GetStudents([FromBody] ClassId i)
        {
            //  try
            // {
            var id = 0;

            if (i.schoolID != 0)
            {
                id = i.schoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                id = principal.SchoolID;

            }

            var currentTerm = await _context.Sessions
            .Where(x => x.SchoolID == id && x.Current)
            .Include(x => x.Terms)
            .SelectMany(x => x.Terms)
            .Where(x => x.Current)
            .SingleOrDefaultAsync();

            if (currentTerm != null)
            {
                var students = _context.ResultRecords
            .Where(x => x.TermID == currentTerm.TermID)
            .Include(x => x.Results)
            .ThenInclude(x => x.Student)
            .SelectMany(x => x.Results)
            .Where(x => x.PrincipalComment == "")
          .Select(x => new ReturnStudent
          {
              Id = x.Student.Id,
              FirstName = x.Student.FirstName,
              LastName = x.Student.LastName,
          })
          .ToHashSet();

                return Ok(students);
            }

            return Ok();


            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }


        }

        [HttpPost]
        [Route("filterStudents")]
        public async Task<IActionResult> FilterStudents([FromBody] SClassId i)
        {
            try
            {
                var id = 0;

                if (i.schoolID != 0)
                {
                    id = i.schoolID;
                }
                else
                {
                    var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                    var principal = await _principalManager.FindByIdAsync(principalId);
                    id = principal.SchoolID;

                }

                var currentTerm = await _context.Sessions
                 .Where(x => x.SchoolID == id && x.Current == true)
                 .Include(x => x.Terms)
                 .SelectMany(x => x.Terms)
                 .Where(x => x.Current == true)
                 .SingleOrDefaultAsync();

                if (currentTerm != null)
                {
                    var students = _context.ResultRecords
                .Where(x => x.TermID == currentTerm.TermID && x.ClassArmID == i.ClassArmID)
                .Include(x => x.Results)
                .ThenInclude(x => x.Student)
                .SelectMany(x => x.Results)
                .Where(x => x.PrincipalComment == "")
              .Select(x => new ReturnStudent
              {
                  Id = x.Student.Id,
                  FirstName = x.Student.FirstName,
                  LastName = x.Student.LastName,
              })
              .ToHashSet();

                    return Ok(students);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
