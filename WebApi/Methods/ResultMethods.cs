using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Methods
{
    public static class ResultMethods
    {
        public static async void CompileResults(int schoolId, SchoolKitContext _context, UserManager<Student> _studentManager, Term term)
        {
            var allstudents = _studentManager.Users
            .Where(x => x.SchoolID == schoolId && x.HasGraduated == false)
            .Include(x => x.ClassArm);

            var classArmIDs = allstudents
                .Select(r => r.ClassArmID).ToHashSet();

            var allterms = _context.Sessions
            .Where(x => x.SchoolID == schoolId)
            .Include(t => t.Terms)
            .SelectMany(o => o.Terms);

            var scoreScheme = await _context.ScoreSchemes.Where(x => x.SchoolID == schoolId)
            .SingleOrDefaultAsync();

            foreach (var classArmID in classArmIDs)
            {
                var students = allstudents
                .Where(c => c.ClassArmID == classArmID);

                var termResultRecord = new ResultRecord
                {
                    ClassArmID = classArmID,
                    TermID = term.TermID,
                    Type = ResultType.Term,
                    ClassCount = students.Count()

                };

                await _context.ResultRecords.AddAsync(termResultRecord);
                await _context.SaveChangesAsync();

                var annualResRec = new ResultRecord(); // holds the result record that's added below if it is a third term

                if (term.Label == TermLabel.ThirdTerm)
                {
                    var annualResultRecord = new ResultRecord
                    {
                        ClassArmID = classArmID,
                        TermID = term.TermID,
                        Type = ResultType.Annual,
                        ClassCount = students.Count()
                    };
                    await _context.ResultRecords.AddAsync(annualResultRecord);
                    await _context.SaveChangesAsync();
                    annualResRec = annualResultRecord;
                }


                foreach (var student in students)
                {
                    var allEnrollments = _context.Enrollments
                        .Where(i => i.StudentID == student.Id);

                    var enrollments = allEnrollments
                        .Where(u => u.TermID == term.TermID);//get enrollments for current term

                    int total = enrollments.Sum(j => j.Total); //sum of all enrollments total scores for current student
                    int count = enrollments.Count();
                    double average = count != 0 ?(double)(total / count): 0;
                    

                    var result = new Result
                    {
                        StudentID = student.Id,
                        ResultRecordID = termResultRecord.ResultRecordID,
                        Total = total,
                        Average = average
                    };
                    await _context.Results.AddAsync(result); //add result for current 
                    await _context.SaveChangesAsync();

                    if (term.Label == TermLabel.ThirdTerm)
                    {
                        //get first and second term and respective enrollments for all three terms
                        var LastTerms = allterms
                       .Where(x => x.SessionID == term.SessionID)
                       .ToList();

                        var firstTerm = LastTerms.Where(x => x.Label == TermLabel.FirstTerm).SingleOrDefault();

                        var secondTerm = LastTerms.Where(x => x.Label == TermLabel.SecondTerm).SingleOrDefault(); ;

                        var termsEnrollments = allEnrollments
                            .Where(c => c.TermID == firstTerm.TermID || c.TermID == secondTerm.TermID || c.TermID == term.TermID);

                        var EClassSubjectIDs = termsEnrollments
                            .Select(o => o.ClassSubjectID)
                            .ToHashSet();

                        var thisAnnualResult = new List<AnnualEnrollment>();

                        //pick each of the subjects and check if the student enrolled in them for all 
                        //three terms
                        foreach (var subjectID in EClassSubjectIDs)
                        {
                            var firstTermE = termsEnrollments
                            .Where(c => c.ClassSubjectID == subjectID && c.TermID == firstTerm.TermID)
                            .SingleOrDefault();

                            var secondTermE = termsEnrollments
                            .Where(c => c.ClassSubjectID == subjectID && c.TermID == secondTerm.TermID)
                            .SingleOrDefault();

                            var thirdTermE = termsEnrollments
                            .Where(c => c.ClassSubjectID == subjectID && c.TermID == term.TermID)
                            .SingleOrDefault();
                            int num = 3;

                            List<Enrollment> E = new List<Enrollment> { firstTermE, secondTermE, thirdTermE };
                            
                            var newEnrollmentList = E.Where(e=> e != null);
                            num = newEnrollmentList.Count();


                            var aTotal = (int)Math.Round((double)(newEnrollmentList.Sum(e => e.Total)) / num);
                            var Grade = new Grade();

                            if (aTotal >= scoreScheme.MinA && aTotal <= scoreScheme.MaxA)
                            {
                                Grade = Grade.A;
                            }
                            else if (aTotal >= scoreScheme.MinB && aTotal <= scoreScheme.MaxB)
                            {
                                Grade = Grade.B;
                            }
                            else if (aTotal >= scoreScheme.MinC && aTotal <= scoreScheme.MaxC)
                            {
                                Grade = Grade.C;
                            }
                            else if (aTotal >= scoreScheme.MinD && aTotal <= scoreScheme.MaxD)
                            {
                                Grade = Grade.D;
                            }
                            else if (aTotal >= scoreScheme.MinE && aTotal <= scoreScheme.MaxE)
                            {
                                Grade = Grade.E;
                            }
                            else if (aTotal >= scoreScheme.MinP && aTotal <= scoreScheme.MaxP)
                            {
                                Grade = Grade.P;
                            }
                            else if (aTotal >= scoreScheme.MinF && aTotal <= scoreScheme.MaxF)
                            {
                                Grade = Grade.F;
                            }

                            var AnnualEnrollment = new AnnualEnrollment
                            {
                                StudentID = student.Id,
                                ClassSubjectID = subjectID,
                                FirstTerm = firstTermE != null ? firstTermE.Total: 0,//if the terms are null equal the them to zero
                                SecondTerm = secondTermE != null ? secondTermE.Total: 0,
                                ThirdTerm = thirdTermE != null? thirdTermE.Total: 0,
                                Total = aTotal,
                                Grade = Grade,
                                TermID = term.TermID
                            };
                            await _context.AnnualEnrollments.AddAsync(AnnualEnrollment);
                            thisAnnualResult.Add(AnnualEnrollment);
                        }

                        await _context.SaveChangesAsync();

                        var annualcount = thisAnnualResult.Count;
                        var annualTotal = thisAnnualResult.Sum(j => j.Total);
                        var annualAverage = annualcount != 0 ? (double)(annualTotal / annualcount): 0;

                        var annualresult = new Result
                        {
                            StudentID = student.Id,
                            ResultRecordID = annualResRec.ResultRecordID,
                            Total = annualTotal,
                            Average = annualAverage
                        };

                        await _context.Results.AddAsync(annualresult);
                        await _context.SaveChangesAsync();

                        if (student.ClassArm.Class == Class.Primary6 || student.ClassArm.Class == Class.SSS3)
                        {
                            student.HasGraduated = true;
                            await _studentManager.UpdateAsync(student);
                        }
                        else
                        {
                            var totalMarks = count * 100;
                            var minimumPassMark = totalMarks * 0.4;
                            if (annualTotal >= minimumPassMark)
                            {
                                var nextClass = (Class)((int)student.ClassArm.Class + 1);
                                var classArm = _context.ClassArms
                                .Where(f => f.Class == nextClass && f.Arm == student.ClassArm.Arm)
                                .FirstOrDefault();

                                student.ClassArmID = classArm.ClassArmID;
                                await _studentManager.UpdateAsync(student);
                            }
                        }
                    }

                }
                //here
                var resultRec = await _context.ResultRecords
                    .Where(i => i.TermID == term.TermID
                    && i.ClassArmID == classArmID
                    && i.Type == ResultType.Term)
                    .Include(r => r.Results)
                    .FirstOrDefaultAsync();

                var classResults = resultRec.Results
                   .OrderByDescending(x => x.Average);


                var classtotal = classResults.Sum(f => f.Total);

                int resCount = 1;
                foreach (var result in classResults)
                {
                    result.ClassPosition = resCount;

                    _context.Update(result);

                    resCount++;
                }
                resultRec.ClassAverage = classtotal / classResults.Count();
                _context.Update(resultRec);
                await _context.SaveChangesAsync();

                if (term.Label == TermLabel.ThirdTerm)
                {
                    var annualResultRec = _context.ResultRecords
                        .Where(i => i.TermID == term.TermID
                        && i.ClassArmID == classArmID
                        && i.Type == ResultType.Annual)
                        .Include(r => r.Results)
                        .SingleOrDefault();

                    var annualResults = annualResultRec.Results
                    .OrderByDescending(x => x.Average);

                    var annualClasstotal = annualResults.Sum(f => f.Total);

                    int aCount = 1;
                    foreach (var result in annualResults)
                    {
                        result.ClassPosition = aCount;
                        _context.Update(result);
                        aCount++;
                    }

                    annualResultRec.ClassAverage = annualClasstotal / annualResults.Count();
                    _context.Update(annualResultRec);
                    await _context.SaveChangesAsync();
                }


            }
        }

        public static async Task<ResultModel> AResults(Term term, string studentId, SchoolKitContext _context)
        {

            var result = await _context.ResultRecords
                    .Where(d => d.TermID == term.TermID && d.Type == ResultType.Annual)
                    .Include(x => x.Results)
                    .ThenInclude(x => x.ResultRecord)
                    .ThenInclude(x => x.ClassArm)
                    .SelectMany(x => x.Results)
                    .Where(s => s.StudentID == studentId)
                    .SingleOrDefaultAsync();

            var classArm = result.ResultRecord.ClassArm;

            var annualEnrollments = await _context.AnnualEnrollments
                        .Include(x => x.ClassSubject)
                        .ThenInclude(f => f.Subject)
                        .Where(d => d.StudentID == studentId && d.TermID == term.TermID)
                        .Select(d => new AnnualEnrollmentModel
                        {
                            SubjectName = d.ClassSubject.Subject.Title,
                            FirstTerm = d.FirstTerm,
                            SecondTerm = d.SecondTerm,
                            ThirdTerm = d.ThirdTerm,
                            Total = d.Total,
                            Grade = d.Grade,
                        }).ToListAsync();

            var annualResult = new ResultModel
            {
                SessionName = term.Session.SessionName,
                ResultID = result.ResultID,
                TermName = Enum.GetName(typeof(TermLabel), term.Label),
                ResultType = Enum.GetName(typeof(ResultType), result.ResultRecord.Type),
                ClassName = Enum.GetName(typeof(Class), classArm.Class) + Enum.GetName(typeof(Arms), classArm.Arm),
                ClassAverage = result.ResultRecord.ClassAverage,
                ClassPosition = result.ClassPosition,
                StudentName = result.Student.LastName + " " + result.Student.FirstName,
                ClassCount = result.ResultRecord.ClassCount,
                Total = result.Total,
                Average = result.Total,
                AnnualEnrollments = annualEnrollments

            };


            return annualResult;
        }

        public static async Task<ResultModel> Results(Term term, string studentId, SchoolKitContext _context)
        {
            var result = await _context.ResultRecords
                    .Where(d => d.TermID == term.TermID && d.Type == ResultType.Term)
                    .Include(x => x.Results)
                    .ThenInclude(x => x.ResultRecord)
                    .ThenInclude(x => x.ClassArm)
                    .SelectMany(x => x.Results)
                    .Include(x => x.Student)
                    .Where(s => s.StudentID == studentId)
                    .SingleOrDefaultAsync();

            var classArm = result.ResultRecord.ClassArm;

            var enrollments = await _context.Enrollments
                .Include(x => x.ClassSubject)
                .ThenInclude(f => f.Subject)
                .Where(d => d.StudentID == studentId && d.TermID == term.TermID)
                .Select(d => new EnrollmentModel
                {
                    SubjectName = d.ClassSubject.Subject.Title,
                    CA = d.CA,
                    Exam = d.Exam,
                    Grade = d.Grade,
                    Total = d.Total
                }).ToListAsync();

            var stResult = new ResultModel
            {
                SessionName = term.Session.SessionName,
                ResultID = result.ResultID,
                TermName = Enum.GetName(typeof(TermLabel), term.Label),
                ResultType = Enum.GetName(typeof(ResultType), result.ResultRecord.Type),
                ClassName = Enum.GetName(typeof(Class), classArm.Class) + Enum.GetName(typeof(Arms), classArm.Arm),
                ClassAverage = result.ResultRecord.ClassAverage,
                ClassPosition = result.ClassPosition,
                Total = result.Total,
                Average = result.Total,
                StudentName = result.Student.LastName + " " + result.Student.FirstName,
                ClassCount = result.ResultRecord.ClassCount,
                Enrollments = enrollments

            };

            return stResult;
        }

        public static async Task<ResultModel> Incomplete_AResult(Term term, Student student, SchoolKitContext _context)
        {

            var result = await _context.ResultRecords
                    .Where(d => d.TermID == term.TermID && d.Type == ResultType.Annual)
                    .Include(x => x.Results.Where(s => s.StudentID == student.Id))
                    .ThenInclude(x => x.ResultRecord)
                    .ThenInclude(x => x.ClassArm)
                    .SelectMany(x => x.Results)
                    .SingleOrDefaultAsync();

            var classArm = result.ResultRecord.ClassArm;

            var annualResult = new ResultModel
            {
                SessionName = term.Session.SessionName,
                TermName = Enum.GetName(typeof(TermLabel), term.Label),
                ResultType = Enum.GetName(typeof(ResultType), result.ResultRecord.Type),
                ClassName = Enum.GetName(typeof(Class), classArm.Class) + Enum.GetName(typeof(Arms), classArm.Arm),
                Message = "You can't view ths result until you complete your fees for the term"
            };
            return annualResult;
        }

        public static async Task<ResultModel> Incomplete_Result(Term term, Student student, SchoolKitContext _context)
        {
            var result = await _context.ResultRecords
                    .Where(d => d.TermID == term.TermID && d.Type == ResultType.Term)
                    .Include(x => x.Results.Where(s => s.StudentID == student.Id))
                    .ThenInclude(x => x.ResultRecord)
                    .ThenInclude(x => x.ClassArm)
                    .SelectMany(x => x.Results)

                    .SingleOrDefaultAsync();

            var classArm = result.ResultRecord.ClassArm;


            var stResult = new ResultModel
            {
                SessionName = term.Session.SessionName,
                TermName = Enum.GetName(typeof(TermLabel), term.Label),
                ResultType = Enum.GetName(typeof(ResultType), result.ResultRecord.Type),
                ClassName = Enum.GetName(typeof(Class), classArm.Class) + Enum.GetName(typeof(Arms), classArm.Arm),
                Message = "You can't view ths result until you complete your fees for the term"
            };

            return stResult;
        }


    }
}