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

            var allterms = _context.Terms////please redo this
            .Include(t => t.Session)
            .ThenInclude(t => t.Terms)
            .SelectMany(o => o.Session.Terms);

            foreach(var classArmID in classArmIDs)
            {
                var students = allstudents
                .Where(c => c.ClassArmID == classArmID);

                var termResultRecord = new ResultRecord{
                    ClassArmID = classArmID,
                    TermID = term.TermID,
                    Type = ResultType.Term
                };
                
               await _context.ResultRecords.AddAsync(termResultRecord);
               await _context.SaveChangesAsync();

                foreach(var student in students)
                {
                    var allEnrollments = _context.Enrollments
                        .Where(i => i.StudentID == student.Id);

                    var enrollments = allEnrollments
                        .Where(u => u.TermID == term.TermID);//get enrollments for current student

                    int total = enrollments.Sum(j => j.Total); //sum of all enrollments total scores for current student
                    int count = enrollments.Count();
                    double average = (double) total/count;

                    var result = new Result{
                        StudentID = student.Id,
                        ResultRecordID = termResultRecord.ResultRecordID,
                        Total = total,
                        Average = average
                    };
                    await _context.Results.AddAsync(result); //add result for current 
                    await _context.SaveChangesAsync();

                    if(term.Label == TermLabel.ThirdTerm)
                    {
                        var annualResultRecord = new ResultRecord{
                            ClassArmID = classArmID,
                            TermID = term.TermID,
                            Type = ResultType.Annual
                            };
                         await _context.ResultRecords.AddAsync(annualResultRecord);

                         var LastTerms = allterms
                        .OrderBy(x => x.TermID)
                        .ToList();
                    
                        var firstTerm = LastTerms[0];
                   
                        var secondTerm = LastTerms[1];
                   
                        var termsEnrollments = allEnrollments
                            .Where(c => c.TermID == firstTerm.TermID || c.TermID == secondTerm.TermID || c.TermID == term.TermID);

                        var EClassSubjectIDs = termsEnrollments
                            .Select(o => o.ClassSubjectID)
                            .ToHashSet();

                        var thisAnnualResult = new List<AnnualEnrollment>();

                        foreach(var subjectID in EClassSubjectIDs)
                        {
                            var firstTermE = termsEnrollments
                            .Where(c => c.ClassSubjectID == subjectID && c.TermID == firstTerm.TermID)
                            .FirstOrDefault(); 

                            var secondTermE = termsEnrollments
                            .Where(c => c.ClassSubjectID == subjectID && c.TermID == secondTerm.TermID)
                            .FirstOrDefault();
                        
                            var thirdTermE = termsEnrollments
                            .Where(c => c.ClassSubjectID == subjectID && c.TermID == term.TermID)
                            .FirstOrDefault();
                            int num = 3;

                            if(thirdTermE == null || secondTermE == null || firstTermE == null)// incase we have a subject that was introduced midway or was removed
                            {
                                List<Enrollment> E = new List<Enrollment>{firstTermE, secondTermE,thirdTermE};
                                num = E.Where(e => e != null).Count();
                            }
                        
                            var aTotal = (int)Math.Round((double)(firstTermE.Total + secondTermE.Total + thirdTermE.Total)/num);
                            var Grade = new Grade();

                            if(aTotal >= 70){
                                Grade = Grade.A;
                            }
                            else if(aTotal >= 60){
                                Grade = Grade.B;
                            }
                            else if(aTotal >= 50){
                                Grade = Grade.C;
                            }
                            else if(aTotal >= 45){
                                Grade = Grade.D;
                            }
                            else{
                                Grade = Grade.F;
                            }
                            
                            var AnnualEnrollment = new AnnualEnrollment{
                                StudentID = student.Id,
                                ClassSubjectID = subjectID,
                                FirstTerm = firstTermE.Total,
                                SecondTerm = secondTermE.Total, 
                                ThirdTerm = thirdTermE.Total,
                                Total = aTotal,
                                Grade = Grade,
                            };
                            await _context.AnnualEnrollments.AddAsync(AnnualEnrollment);
                            thisAnnualResult.Add(AnnualEnrollment);
                        }
                        

                        var annualcount = thisAnnualResult.Count;
                        var annualTotal = thisAnnualResult.Sum(j => j.Total);
                        var annualAverage = (double)annualTotal/annualcount;

                        var annualresult = new Result{
                            StudentID = student.Id,
                            ResultRecordID = annualResultRecord.ResultRecordID,
                            Total = annualTotal,
                            Average = annualAverage
                        };

                        await _context.Results.AddAsync(annualresult);
                        await _context.SaveChangesAsync();

                        if(student.ClassArm.Class == Class.Primary6 || student.ClassArm.Class == Class.SSS3)
                        {

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
                var resultRec = _context.ResultRecords
                    .Where(i => i.TermID == term.TermID 
                    && i.ClassArmID == classArmID
                    && i.Type == ResultType.Term)
                    .Include(r => r.Results)
                    .SingleOrDefault();
                 
                 var classResults = resultRec.Results
                    .OrderBy(x => x.Average);


                 var classtotal = classResults.Sum(f => f.Total);

                int resCount = 1;
                foreach(var result in classResults)
                {
                    result.ClassPosition = resCount;
                    
                    _context.Update(result);
                    
                    resCount++;  
                }
                resultRec.ClassAverage = classtotal/classResults.Count();
                _context.Update(resultRec);
                await _context.SaveChangesAsync();

                if(term.Label == TermLabel.ThirdTerm)
                {
                    var annualResultRec = _context.ResultRecords
                        .Where(i => i.TermID == term.TermID 
                        && i.ClassArmID == classArmID
                        && i.Type == ResultType.Annual)
                        .Include(r => r.Results)
                        .SingleOrDefault();

                    var annualResults = annualResultRec.Results
                    .OrderBy(x => x.Average);

                    var annualClasstotal = annualResults.Sum(f => f.Total);

                    int aCount = 1;
                    foreach(var result in annualResults)
                    {
                        result.ClassPosition = aCount;
                        _context.Update(result);
                        aCount++;  
                    }
                    
                    annualResultRec.ClassAverage = annualClasstotal/annualResults.Count();
                    _context.Update(annualResultRec);
                    await _context.SaveChangesAsync();
                }
                
                 
            }
        }

        public static async Task<ResultModel> AResults(Term term, Student student, SchoolKitContext _context){

            var result = await _context.ResultRecords
                    .Where(d => d.TermID == term.TermID && d.Type == ResultType.Annual)
                    .Include(x => x.Results.Where(s => s.StudentID == student.Id))
                    .ThenInclude(x => x.ResultRecord)
                    .ThenInclude(x => x.ClassArm)
                    .SelectMany(x => x.Results)
                    
                    .SingleOrDefaultAsync();

                var classArm = result.ResultRecord.ClassArm;

                var annualEnrollments = await _context.AnnualEnrollments
                            .Include(x => x.ClassSubject)
                            .ThenInclude(f => f.Subject)
                            .Where(d => d.StudentID == student.Id && d.TermID == term.TermID)
                            .Select(d => new AnnualEnrollmentModel{
                                SubjectName = d.ClassSubject.Subject.Title,
                                FirstTerm = d.FirstTerm,
                                SecondTerm = d.SecondTerm,
                                ThirdTerm = d.ThirdTerm,
                                Total = d.Total,
                                Grade =d.Grade,
                                }).ToListAsync();

                        var annualResult = new ResultModel
                        {
                            SessionName = term.Session.SessionName,
                            TermName = Enum.GetName(typeof(TermLabel), term.Label),
                            ResultType = Enum.GetName(typeof(ResultType), result.ResultRecord.Type),
                            ClassName = Enum.GetName(typeof(Class), classArm.Class) + Enum.GetName(typeof(Arms), classArm.Arm),
                            ClassAverage = result.ResultRecord.ClassAverage,
                            ClassPosition= result.ClassPosition,
                            Total = result.Total,
                            Average = result.Total,   
                            AnnualEnrollments = annualEnrollments

                        };
               

                return annualResult;
        }

        public static async Task<ResultModel> Results(Term term, Student student, SchoolKitContext _context)
        {
            var result = await _context.ResultRecords
                    .Where(d => d.TermID == term.TermID && d.Type == ResultType.Term)
                    .Include(x => x.Results.Where(s => s.StudentID == student.Id))
                    .ThenInclude(x => x.ResultRecord)
                    .ThenInclude(x => x.ClassArm)
                    .SelectMany(x => x.Results)
                    
                    .SingleOrDefaultAsync();

                var classArm = result.ResultRecord.ClassArm;

                var enrollments = await _context.Enrollments
                    .Include(x=> x.ClassSubject)
                    .ThenInclude(f => f.Subject)
                    .Where(d => d.StudentID == student.Id && d.TermID == term.TermID)
                    .Select(d => new EnrollmentModel
                    {
                        SubjectName = d.ClassSubject.Subject.Title,
                        CA = d.CA,
                        Exam = d.Exam,
                         Grade =d.Grade,
                    }).ToListAsync();

                var stResult = new ResultModel
                {
                    SessionName = term.Session.SessionName,
                    TermName = Enum.GetName(typeof(TermLabel), term.Label),
                    ResultType = Enum.GetName(typeof(ResultType), result.ResultRecord.Type),
                    ClassName = Enum.GetName(typeof(Class), classArm.Class) + Enum.GetName(typeof(Arms), classArm.Arm),
                    ClassAverage = result.ResultRecord.ClassAverage,
                    ClassPosition= result.ClassPosition,
                    Total = result.Total,
                    Average = result.Total,        

                    Enrollments = enrollments

                };

                return stResult;
        }

        public static async Task<ResultModel> Incomplete_AResult(Term term, Student student, SchoolKitContext _context){

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