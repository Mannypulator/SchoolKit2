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
        public async Task<List<ClassSubject>> EnrollStudent(Student model, Term term, SchoolKitContext _context)
        {

            var Class = await _context.ClassArms
              .Where(x => x.ClassArmID == model.ClassArmID)
              .Select(x => x.Class).SingleOrDefaultAsync();

            var className = Enum.GetName(typeof(Class), Class);
            if (className.Contains("SSS"))
            {
                var classSubjects = _context.ClassSubjects //get class subjects for a particular class
             .Where(x => x.ClassArmID == model.ClassArmID);

                //get subject ids for all the compulsory 
                //...subjects in that school for senior class
                var sub = _context.SSCompulsories
                .Where(c => c.SchoolID == model.SchoolID)
                .Select(j => j.SubjectID)
                .ToList();

                foreach (var subId in sub)
                {
                    var classSubject = _context.ClassSubjects
                    .Where(x => x.ClassArmID == model.ClassArmID && x.SubjectID == subId)
                    .FirstOrDefault();

                    var enrollment = new Enrollment
                    {
                        StudentID = model.Id,
                        ClassSubjectID = classSubject.ClassSubjectID,
                        TermID = term.TermID,
                        Grade = Grade.F
                    };
                    if (await CheckEnrollment(enrollment.StudentID, enrollment.TermID, enrollment.ClassSubjectID, _context))
                    {
                        await _context.Enrollments.AddAsync(enrollment);
                    }

                }
                AddPADomain(model, term, _context);

                await _context.SaveChangesAsync();
                return await classSubjects.ToListAsync();
            }
            else
            {
                var dropped = _context.SSDrops
                .Where(d => d.SchoolID == model.SchoolID);
                var classSubject = _context.ClassSubjects
                .Where(x => x.ClassArmID == model.ClassArmID);

                var selected = await classSubject
                .Where(e => dropped.All(r => r.SubjectID != e.SubjectID)).ToListAsync();

                foreach (var select in selected)
                {
                    var enrollment = new Enrollment
                    {
                        StudentID = model.Id,
                        ClassSubjectID = select.ClassSubjectID,
                        TermID = term.TermID,
                        Grade = Grade.F
                    };
                    if (await CheckEnrollment(enrollment.StudentID, enrollment.TermID, enrollment.ClassSubjectID, _context))
                    {
                        await _context.Enrollments.AddAsync(enrollment);
                    }
                }
                AddPADomain(model, term, _context);
                await _context.SaveChangesAsync();
                return selected;
            }
        }



        public async void EnrollStudent(Student model, Term term, SchoolKitContext _context, Term prevTerm)
        {

            var Class = await _context.ClassArms
              .Where(x => x.ClassArmID == model.ClassArmID)
              .Select(x => x.Class).SingleOrDefaultAsync();

            var className = Enum.GetName(typeof(Class), Class);

            if (className.Contains("SSS"))
            {
                if (term.Label == TermLabel.FirstTerm)
                {
                    var sub = _context.SSCompulsories
                    .Where(c => c.SchoolID == model.SchoolID)
                    .Select(j => j.SubjectID)
                    .ToList();


                    foreach (var subId in sub)
                    {
                        var classSubject = _context.ClassSubjects
                        .Where(x => x.ClassArmID == model.ClassArmID && x.SubjectID == subId)
                        .FirstOrDefault();

                        var enrollment = new Enrollment
                        {
                            StudentID = model.Id,
                            ClassSubjectID = classSubject.ClassSubjectID,
                            TermID = term.TermID,
                            Grade = Grade.F
                        };
                        if (await CheckEnrollment(enrollment.StudentID, enrollment.TermID, enrollment.ClassSubjectID, _context))
                        {
                            await _context.Enrollments.AddAsync(enrollment);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var prevEnrollments = _context.Enrollments
                    .Where(x => x.StudentID == model.Id && x.TermID == prevTerm.TermID);

                    foreach (var prevEnrollment in prevEnrollments)//loop through and repeat previous enrollments for the new term
                    {
                        var enrollment = new Enrollment
                        {
                            StudentID = model.Id,
                            ClassSubjectID = prevEnrollment.ClassSubjectID,
                            TermID = term.TermID,
                            Grade = Grade.F
                        };
                        if (await CheckEnrollment(enrollment.StudentID, enrollment.TermID, enrollment.ClassSubjectID, _context))
                        {
                            await _context.Enrollments.AddAsync(enrollment);
                        }
                    }

                    //add compulsory enrollments if there are no previous enrollments
                    if (!prevEnrollments.Any())
                    {
                        var sub = _context.SSCompulsories
                        .Where(c => c.SchoolID == model.SchoolID)
                        .Select(j => j.SubjectID)
                        .ToList();


                        foreach (var subId in sub)
                        {
                            var classSubject = _context.ClassSubjects
                            .Where(x => x.ClassArmID == model.ClassArmID && x.SubjectID == subId)
                            .FirstOrDefault();

                            var enrollment = new Enrollment
                            {
                                StudentID = model.Id,
                                ClassSubjectID = classSubject.ClassSubjectID,
                                TermID = term.TermID,
                                Grade = Grade.F
                            };
                            if (await CheckEnrollment(enrollment.StudentID, enrollment.TermID, enrollment.ClassSubjectID, _context))
                            {
                                await _context.Enrollments.AddAsync(enrollment);
                            }
                        }
                        
                    }
                    AddPADomain(model, term, _context);
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

                foreach (var select in selected)
                {
                    var enrollment = new Enrollment
                    {
                        StudentID = model.Id,
                        ClassSubjectID = select.ClassSubjectID,
                        TermID = term.TermID,
                        Grade = Grade.F
                    };
                    if (await CheckEnrollment(enrollment.StudentID, enrollment.TermID, enrollment.ClassSubjectID, _context))
                    {
                        await _context.Enrollments.AddAsync(enrollment);
                    }

                }
                AddPADomain(model, term, _context);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckEnrollment(string studentID, int termID, int classSubjectID, SchoolKitContext _context)
        {
            var enrollments = await _context.Enrollments
            .Where(x => x.StudentID == studentID && x.TermID == termID && x.ClassSubjectID == classSubjectID)
            .AnyAsync();
            if (enrollments)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async void AddPADomain(Student model, Term term, SchoolKitContext _context){
            if(term.Label == TermLabel.ThirdTerm){
                var affectiveDomain = new AffectiveDomain{
                    StudentID = model.Id,
                    TermID = term.TermID,
                    Type = ResultType.Term
                };
                 var psychomotorDomain = new PsychomotorDomain{
                    StudentID = model.Id,
                    TermID = term.TermID,
                    Type = ResultType.Term
                };
                 var annualAffectiveDomain = new AffectiveDomain{
                    StudentID = model.Id,
                    TermID = term.TermID,
                    Type = ResultType.Annual
                };
                 var annualPsychomotorDomain = new PsychomotorDomain{
                    StudentID = model.Id,
                    TermID = term.TermID,
                    Type = ResultType.Annual
                };
                await _context.AffectiveDomains.AddAsync(affectiveDomain);
                await _context.PsychomotorDomains.AddAsync(psychomotorDomain);
                await _context.AffectiveDomains.AddAsync(annualAffectiveDomain);
                await _context.PsychomotorDomains.AddAsync(annualPsychomotorDomain);
            }
            else{
                 var affectiveDomain = new AffectiveDomain{
                    StudentID = model.Id,
                    TermID = term.TermID,
                    Type = ResultType.Term
                };
                 var psychomotorDomain = new PsychomotorDomain{
                    StudentID = model.Id,
                    TermID = term.TermID,
                    Type = ResultType.Term
                };
                await _context.AffectiveDomains.AddAsync(affectiveDomain);
                await _context.PsychomotorDomains.AddAsync(psychomotorDomain);
            }
           

        }

    }
}