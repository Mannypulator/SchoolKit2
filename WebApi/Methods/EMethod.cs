using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Methods
{
    [NotMapped]
    public class EMethod
    {
        public async void EnrollStudent(Student model, Term term, SchoolKitContext _context)
        {
            
                var Class = await _context.ClassArms
                  .Where(x => x.ClassArmID == model.ClassArmID)
                  .Select(x => x.Class).SingleOrDefaultAsync();  

                var className = Enum.GetName(typeof(Class), Class);
                  if(className.Contains("SSS"))
                  {
                    var sub = _context.SSCompulsories
                    .Where(c => c.SchoolID == model.SchoolID)
                    .Select(j => j.SubjectID)
                    .ToList();
                    
                    var Asub = _context.Subjects
                    .Where(x => x.Range == ClassRange.All)
                    .Select(j => j.SubjectID)
                    .ToList();

                    sub.AddRange(Asub);
                    foreach(var subId in sub)
                    {
                       var classSubject = _context.ClassSubjects
                       .Where(x => x.ClassArmID == model.ClassArmID && x.SubjectID == subId)
                       .FirstOrDefault();
                       
                       var enrollment = new Enrollment{
                           StudentID = model.Id,
                           ClassSubjectID = classSubject.ClassSubjectID,
                           TermID = term.TermID              
                       };
                       await _context.Enrollments.AddAsync(enrollment);
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var dropped = _context.SSDrops
                    .Where(d => d.SchoolID == model.SchoolID);
                    var classSubject = _context.ClassSubjects
                    .Where(x => x.ClassArmID == model.ClassArmID);

                    var selected = classSubject
                    .Where(e => dropped.All(r => r.SubjectID != e.SubjectID));

                    foreach(var select in selected)
                    {
                         var enrollment = new Enrollment{
                           StudentID = model.Id,
                           ClassSubjectID = select.ClassSubjectID,
                           TermID = term.TermID              
                       };
                       await _context.Enrollments.AddAsync(enrollment);
                    }
                    await _context.SaveChangesAsync();
                }
        }


        public async Task<IdentityResult> Principal(Principal model, SchoolKitContext _context, UserManager<Principal> _userManager, string role)
        {
            var principal = new Principal{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Email = model.Email,
                SchoolID = model.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,

            };

            try{
                 var result = await _userManager.CreateAsync(principal, model.PasswordHash);
                 if(result.Succeeded)
                 {
                     await _userManager.AddToRoleAsync(principal,role);

                     if(model.PrincipalQualifications != null)
                     {
                         foreach(var sub in model.PrincipalQualifications)
                       {
                         var principalQualification = new PrincipalQualification{
                             Qlf = sub.Qlf,
                             PrincipalID = principal.Id
                         };
                         await _context.PrincipalQualifications.AddAsync(principalQualification);
                       }
                     await _context.SaveChangesAsync();
                     }
                     return result;
                 }
                 else{
                     return result;
                 }
            }
            catch(Exception ex){
                throw ex;
            }
           
        }

        public async void EnrollStudent(Student model, Term term, SchoolKitContext _context, Term prevTerm)
        {
            
                var Class = await _context.ClassArms
                  .Where(x => x.ClassArmID == model.ClassArmID)
                  .Select(x => x.Class).SingleOrDefaultAsync();  

                var className = Enum.GetName(typeof(Class), Class);
                                
                if(className.Contains("SSS"))
                {
                    if(term.Label == TermLabel.FirstTerm)
                    {
                        var sub = _context.SSCompulsories
                        .Where(c => c.SchoolID == model.SchoolID)
                        .Select(j => j.SubjectID)
                        .ToList();
                    
                        var Asub = _context.Subjects
                        .Where(x => x.Range == ClassRange.All)
                        .Select(j => j.SubjectID)
                        .ToList();

                        sub.AddRange(Asub);
                        foreach(var subId in sub)
                        {
                           var classSubject = _context.ClassSubjects
                           .Where(x => x.ClassArmID == model.ClassArmID && x.SubjectID == subId)
                           .FirstOrDefault();
                       
                           var enrollment = new Enrollment{
                           StudentID = model.Id,
                           ClassSubjectID = classSubject.ClassSubjectID,
                           TermID = term.TermID              
                           };
                           await _context.Enrollments.AddAsync(enrollment);
                        }
                        await _context.SaveChangesAsync();
                    }
                    else{
                        var prevEnrollments = _context.Enrollments
                        .Where(x => x.StudentID == model.Id && x.TermID == prevTerm.TermID);

                        foreach(var prevEnrollment in prevEnrollments)
                        {
                            var enrollment = new Enrollment{
                                StudentID = model.Id,
                                ClassSubjectID = prevEnrollment.ClassSubjectID,
                                TermID = term.TermID
                            };
                            await _context.Enrollments.AddAsync(enrollment);
                        }
                        await _context.SaveChangesAsync();
                    }
                    
                }
                else
                {
                    var dropped = _context.SSDrops
                    .Where(d => d.SchoolID == model.SchoolID);
                    var classSubject = _context.ClassSubjects
                    .Where(x => x.ClassArmID == model.ClassArmID);

                    var selected = classSubject
                    .Where(e => dropped.All(r => r.SubjectID != e.SubjectID));

                    foreach(var select in selected)
                    {
                         var enrollment = new Enrollment{
                           StudentID = model.Id,
                           ClassSubjectID = select.ClassSubjectID,
                           TermID = term.TermID              
                       };
                       await _context.Enrollments.AddAsync(enrollment);
                    }
                    await _context.SaveChangesAsync();
                }
        }

        public async void CompileResults(int schoolId, SchoolKitContext _context, UserManager<Student> _studentManager)
        {
            var students = _studentManager.Users
            .Where(x => x.SchoolID == schoolId && x.HasGraduated == false)
            .Include(x => x.ClassArm);

            var allterms = _context.Terms
            .Where(d => d.SchoolID == schoolId);

            var term = allterms
            .Where(r => r.Current == true)
            .SingleOrDefault();

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
                   Type = ResultType.Term,
                   StudentID = student.Id,
                   TermID = term.TermID,
                   Total = total,
                   Average = average
               };
               await _context.Results.AddAsync(result); //add result for current 

               if(term.Label == TermLabel.ThirdTerm)
               {
                   var LastTerms = allterms
                   .OrderBy(x => x.TermID)
                   .Skip(3)
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
                    await _context.SaveChangesAsync();

                    var annualcount = thisAnnualResult.Count;
                    var annualTotal = thisAnnualResult.Sum(j => j.Total);
                    var annualAverage = (double)annualTotal/annualcount;

                    var annualresult = new Result{
                        Type = ResultType.Annual,
                        StudentID = student.Id,
                        TermID = term.TermID,
                        Total = annualTotal,
                        Average = annualAverage
                    };


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

            await _context.SaveChangesAsync();

            var classArms = _studentManager.Users
                .Where(u => u.SchoolID == schoolId)
                .Select(r => r.ClassArmID).ToHashSet();//get classArms id for students in current school

           foreach(var classArm in classArms)
           {
                var classResults = _context.Results.Include(x => x.Student)
                 .Where(i => i.Student.SchoolID == schoolId 
                 && i.TermID == term.TermID 
                 && i.Student.ClassArmID == classArm
                 && i.Type == ResultType.Term)
                 .OrderBy(x => x.Average);

                 var classtotal = classResults.Sum(f => f.Total);

                int count = 1;
                foreach(var result in classResults)
                {
                    result.ClassPosition = count;
                    result.ClassAverage = classtotal/classResults.Count();
                    _context.Update(result);
                    count++;  
                }

                if(term.Label == TermLabel.ThirdTerm)
                {
                    var annualResults = _context.Results.Include(x => x.Student)
                    .Where(i => i.Student.SchoolID == schoolId 
                    && i.TermID == term.TermID 
                    && i.Student.ClassArmID == classArm
                    && i.Type == ResultType.Annual)
                    .OrderBy(x => x.Average);

                    var annualClasstotal = annualResults.Sum(f => f.Total);

                    int aCount = 1;
                    foreach(var result in annualResults)
                    {
                        result.ClassPosition = aCount;
                        result.ClassAverage = annualClasstotal/annualResults.Count();
                        _context.Update(result);
                        count++;  
                    }
                }
                
                 await _context.SaveChangesAsync();// if positions don't show, remember to test this
           }
           
            
        }


    }
}