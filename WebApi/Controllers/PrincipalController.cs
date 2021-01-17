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
    [Route("api/principal")]
    //authorise for principal only
    public class PrincipalController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Principal> _principalManager;
        private readonly UserManager<Teacher> _teacherManager;
        private readonly UserManager<Student> _studentManager;

        public PrincipalController(SchoolKitContext context, UserManager<Principal> principalManager, UserManager<Teacher> teacherManager,UserManager<Student> studentManager)
        {
            _context = context;
            _principalManager = principalManager;
            _teacherManager = teacherManager;
            _studentManager = studentManager;
        }

        
        [HttpGet]
        [Route("getTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
              var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
             var principal  = await _principalManager.FindByIdAsync(principalId);

             var id = principal.SchoolID;

              var teachers = _teacherManager.Users
            .Where(x => x.SchoolID == id);

            return Ok(teachers);
    }

        [HttpGet]
        [Route("getStudents")]
        public async Task<IActionResult> GetStudents()
        {
              var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
             var principal  = await _principalManager.FindByIdAsync(principalId);

             var id = principal.SchoolID;

              var students = _studentManager.Users
            .Where(x => x.SchoolID == id);

            return Ok(students);
            

        }   
        [HttpDelete]
        [Route("deleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] String studentId)
        {
            var student = await _studentManager.FindByIdAsync(studentId);
            IdentityResult result= await _studentManager.RemoveFromRoleAsync(student, "Student");
            IdentityResult res = await _studentManager.DeleteAsync(student);/////remember to test
           await _context.SaveChangesAsync();
           return Ok("Successfully deleted");
        }

        [HttpDelete]
        [Route("deleteTeacher")]
        public async Task<IActionResult> DeleteTeacher([FromBody] String teacherId)
        {
            var teacher = await _teacherManager.FindByIdAsync(teacherId);
            IdentityResult result= await _teacherManager.RemoveFromRoleAsync(teacher, "Teacher");
            IdentityResult res = await _teacherManager.DeleteAsync(teacher);/////remember to test
           await _context.SaveChangesAsync();
           return Ok("Successfully deleted");
        }

        [HttpPost]
        [Route("assingSubject")]
        //
        public async Task<IActionResult> AssignSubject([FromBody] SubjectAssign model)
        {
           var teacherSubjects = _teacherManager.Users
           .Where(x => x.SchoolID == model.SchoolID)
           .Include(x => x.TeacherSubjects)
           .SelectMany(x => x.TeacherSubjects);

           foreach(var tsub in teacherSubjects)
           {
               if(tsub.ClassSubjectID == model.ClassSubjectID)
               {
                   var teacher = await _teacherManager.FindByIdAsync(tsub.TeacherID);
                   return BadRequest(new {Message = "Class subject is currently assigned to "+ teacher.FirstName + " "+teacher.LastName });
               }
           }
           var teacherSubject = new TeacherSubject
           {
            ClassSubjectID = model.ClassSubjectID,
            TeacherID = model.TeacherID
            };
             await _context.TeacherSubjects.AddAsync(teacherSubject);
             await _context.SaveChangesAsync();
            
            return Ok(new {Message = "Subject assigned Succesfully"});
        }

        [HttpPost]
        [Route("dissociateSubject")]
        //
        public async Task<IActionResult> DissociateSubject([FromBody] SubjectAssign model)
        {
           var tsub = await _context.TeacherSubjects
           .Where(s => s.TeacherID == model.TeacherID && s.ClassSubjectID == model.ClassSubjectID)
           .SingleOrDefaultAsync();
           _context.TeacherSubjects.Remove(tsub);
           await _context.SaveChangesAsync();

           return Ok(new {Message = "Subject Succesfully dissociated from teacher"});
        }

        [HttpPost]
        [Route("studentCode")]
        //authorise for only admin
        public async Task<IActionResult> StudentCode([FromBody] StudentCode model)
        {
           Random random = new Random();
           string t = random.Next(9999,100000).ToString();
           model.Date = DateTime.UtcNow.AddHours(1);
           model.Code = t;
           await _context.StudentCodes.AddAsync(model);
           await _context.SaveChangesAsync();
           
           return Ok(t);
        }

        [HttpPost]
        [Route("TeacherCode")]
        //authorise for only admin
        public async Task<IActionResult> TeacherCode([FromBody] TeacherCode model)
        {
           Random random = new Random();
           string t = random.Next(9999,100000).ToString();
           model.Date = DateTime.UtcNow.AddHours(1);
           model.Code = t;
           await _context.TeacherCodes.AddAsync(model);
           await _context.SaveChangesAsync();
           
           return Ok(t);
        }

        [HttpDelete]
        [Route("deletePrincipal")]
        public async Task<IActionResult> DeletePrincipal([FromBody] String principalId)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if(role == "Proprietor") 
            {
                var principal = await _principalManager.FindByIdAsync(principalId);
                await _principalManager.RemoveFromRoleAsync(principal, "Principal");
                await _principalManager.DeleteAsync(principal);
                await _context.SaveChangesAsync();
                return Ok("Successfully deleted");
            }
           /////remember to test
           return Unauthorized();
        }

        [HttpPost]
        [Route("AddPrincipal")]
        public async Task<IActionResult> AddPrincipal([FromBody] Principal principal)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if(role == "Proprietor") 
            {
                EMethod eMethod = new EMethod(); 
                var result = await eMethod.Principal(principal,_context,_principalManager, "Principal");
                return Ok("Successfully deleted");
            }
           /////remember to test
           return Unauthorized();
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student model)
        {
              var regCount = _context.Schools
                .Where(x => x.SchoolID == model.SchoolID)
               .Select(x => x.RegNoCount).Single();
             var append = _context.Schools
              .Where(x => x.SchoolID == model.SchoolID)
             .Select(x => x.Append).Single();

             string s = append + regCount.ToString().PadLeft(4, '0');

             Student student = new Student{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                ClassArmID = model.ClassArmID,
                SchoolID = model.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                RegNo = s,
                UserName = s
              };
             try{
             
                var result = await _studentManager.CreateAsync(student, model.PasswordHash);
                if (result.Succeeded)
                {
                    await _studentManager.AddToRoleAsync(student,"Student");
                    var regC = _context.Schools
                   .Where(x => x.SchoolID == model.SchoolID)
                   .Single();
                   regC.RegNoCount += 1;
                   regC.StudentCount += 1;

                   await _context.SaveChangesAsync();
                  
                   var term = _context.Terms
                       .Where(x => x.SchoolID == model.SchoolID && x.Current == true)
                       .Single();
                  EMethod eMethod = new EMethod();
                  eMethod.EnrollStudent(student, term, _context); 

                }
                return Ok(result);
             }
             catch(Exception ex)
             {
                throw ex;
             }
        }

        [HttpPost]
        [Route("AddTeacher")]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher model)
        {
           var teacher = new Teacher{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SchoolID = model.SchoolID,
                LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,

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
                         var teacherSubject = new TeacherSubject{
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
    }
}
//proprietor