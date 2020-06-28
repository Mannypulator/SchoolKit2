using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Methods
{
    [NotMapped]
    public static class EMethod
    {
        public static async void EnrollStudent(Student model, Term term, SchoolKitContext _context)
        {
                var className = await _context.ClassArms
                  .Include(x => x.Class)
                  .Where(x => x.ClassArmID == model.ClassArmID)
                  .Select(x => x.Class.ClassName).SingleAsync();                  

                  if(className.Contains("SSS"))
                  {
                    var sub = _context.SSCompulsories
                    .Where(c => c.SchoolID == model.SchoolID)
                    .Select(j => j.SubjectID);
                    foreach(var subId in sub)
                    {
                       var classSubject = _context.ClassSubjects
                       .Where(x => x.ClassArmID == model.ClassArmID && x.SubjectID == subId)
                       .Single();
                       
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
    }
}