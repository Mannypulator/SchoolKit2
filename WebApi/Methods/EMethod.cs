using System;
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
            
                var className = await _context.ClassArms
                  .Include(x => x.Class)
                  .Where(x => x.ClassArmID == model.ClassArmID)
                  .Select(x => x.Class.ClassName).SingleAsync();                  

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
            
                var className = await _context.ClassArms
                  .Include(x => x.Class)
                  .Where(x => x.ClassArmID == model.ClassArmID)
                  .Select(x => x.Class.ClassName).SingleAsync();                  

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

    }
}