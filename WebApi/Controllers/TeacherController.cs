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
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Teacher> _teacherManager;

        public TeacherController(SchoolKitContext context, UserManager<Teacher> teacherManager) 
        {
            _context = context;
            _teacherManager = teacherManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Teacher model)
        {
            var code = _context.TeacherCodes
            .Where(x => x.Code == model.Code.Code)
            .FirstOrDefault();
            var time_check = (DateTime.UtcNow.AddHours(1) - code.Date).TotalDays;
            if(code != null && time_check < 2)
            {
                var teacher = new Teacher{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SchoolID = model.Code.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber

               };

             try{
                 var result = await _teacherManager.CreateAsync(teacher, model.PasswordHash);
                 if(result.Succeeded)
                 {
                     await _teacherManager.AddToRoleAsync(teacher,"Teacher");
                     if(model.TeacherSubjects != null)
                     {
                        foreach(var sub in model.TeacherSubjects)
                       {
                         var teacherSubject = new TeacherSubject
                         {
                             ClassSubjectID = sub.ClassSubjectID,
                             TeacherID = teacher.Id
                         };
                         await _context.TeacherSubjects.AddAsync(teacherSubject);
                       }
                        await _context.SaveChangesAsync();
                     }

                     if(model.TeacherQualifications != null)
                     {
                         foreach(var sub in model.TeacherQualifications)
                       {
                         var teacherQualification = new TeacherQualification{
                             Qlf = sub.Qlf,
                             TeacherID = teacher.Id
                         };
                         await _context.TeacherQualifications.AddAsync(teacherQualification);
                       }
                     await _context.SaveChangesAsync();
                     }
                     
                 }
                 return Ok(result);
                }
              catch(Exception ex){
                throw ex;
              }
            }
            else
            {
                return BadRequest(new {Message = "Invalid code"});
            }
        }

        [HttpGet]
        [Route("subjects")]
        public async Task<IActionResult> Subject()
        {   
            var teacherId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
           
            try{
                var subjects = _context.TeacherSubjects
                .Include(x => x.ClassSubject)
                .ThenInclude(x => x.ClassArm)
                .Include(x => x.ClassSubject.Subject)
                .Where(x => x.TeacherID == teacherId)
               .Select(c => new TeacherSubjectModel{
                ClassSubjectID = c.ClassSubjectID,
                SubjectName = c.ClassSubject.Subject.Title,
                ClassName = Enum.GetName(typeof(Class), c.ClassSubject.ClassArm.Class),
                ClassArm = c.ClassSubject.ClassArm.Arm
            }).ToList();

           return await Task.Run(()=>
            {
                return Ok(subjects);
            });
            }
            catch(Exception ex){
                throw ex;
            }
            
            
        }

        [HttpPost]
        [Route("enrollStudent")]
        public async Task<IActionResult> EnrollStudent([FromBody] Enrollment model)
        {   
            var teacherId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var teacher = await _teacherManager.FindByIdAsync(teacherId);
            var teacherSubjects = _context.TeacherSubjects
            .Where(x => x.TeacherID == teacherId);

            if(teacherSubjects.Any(e =>e.ClassSubjectID == model.ClassSubjectID ))
            {
                var termID =  _context.Terms
                .Where(x => x.SchoolID == teacher.SchoolID && x.Current == true)
                .Select(y => y.TermID)
                .FirstOrDefault();
                var check = _context.Enrollments
                .Any(r => r.StudentID == model.StudentID && r.TermID == termID && r.ClassSubjectID == model.ClassSubjectID);
                
                if(check){
                    return BadRequest(new {Message = "Student already enrolled"});
                }
                model.TermID = termID;
                await _context.Enrollments.AddAsync(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
           else{
                return BadRequest(new {Message = "This subject was not assigned tp you"});
           }
           
        }

        [HttpPost]
        [Route("deListStudent")]
        public async Task<IActionResult> DeListStudent([FromBody] Enrollment model)
        {   
            var teacherId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var teacher = await _teacherManager.FindByIdAsync(teacherId);
            var teacherSubjects = _context.TeacherSubjects
            .Where(x => x.TeacherID == teacherId);

            if(teacherSubjects.Any(e =>e.ClassSubjectID == model.ClassSubjectID ))
            {
                var subject = _context.ClassSubjects
                .Where(c => c.ClassSubjectID == model.ClassSubjectID)
                .Include(u => u.Subject)
                .Select(f => f.Subject)
                .FirstOrDefault();

                 var Class = await _context.ClassSubjects
                  .Where(x => x.ClassSubjectID == model.ClassSubjectID)
                  .Include(x => x.ClassArm)
                  .Select(x => x.ClassArm.Class)
                  .SingleOrDefaultAsync();           

                var className = Enum.GetName(typeof(Class), Class);       

                  if(className.Contains("SSS"))
                  {
                    var check = _context.SSCompulsories
                    .Where(c => c.SchoolID == teacher.SchoolID);
                    if(check.Any(p => p.SubjectID == subject.SubjectID )){
                        return BadRequest(new {Message = "Student cannot be registered or delisted because the subject is compulsory"});
                    }
                    else
                    {
                        _context.Enrollments.Remove(model);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                  }
                 else{
                    return BadRequest(new {Message = "Student cannot be registered or delisted because the subject is compulsory"});
                 }
               
            }
           else{
                return BadRequest(new {Message = "This subject was not assigned to you"});
           }
           
        }

    }
}
