using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = "Admin,Principal,Proprietor")]
    [EnableCors("SiteCorsPolicy")]
    //authorise for principal only
    public class PrincipalController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Principal> _principalManager;
        private readonly UserManager<Teacher> _teacherManager;
        private readonly UserManager<Student> _studentManager;

        public PrincipalController(SchoolKitContext context, UserManager<Principal> principalManager, UserManager<Teacher> teacherManager, UserManager<Student> studentManager)
        {
            _context = context;
            _principalManager = principalManager;
            _teacherManager = teacherManager;
            _studentManager = studentManager;
        }
        //authorize for principals
        [HttpPost]
        [Route("getTeachers")]
        public async Task<IActionResult> GetTeachers([FromBody] ClassId i)
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

            var teachers = _teacherManager.Users
            .Where(x => x.SchoolID == id)
            
            
            .Include(x => x.TeacherSubjects)
            .ThenInclude(x => x.ClassSubject)
            .ThenInclude(x => x.Subject)
            .Include(x => x.TeacherSubjects)
            .ThenInclude(x => x.ClassSubject)
            .ThenInclude(x => x.ClassArm)
            .Include(x => x.TeacherClasses)
            .ThenInclude(x => x.ClassArm);


            var selection = new List<ReturnTeacher>();

            foreach (var teacher in teachers)
            {
                var TS = new ReturnTeacher
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Email = teacher.Email,
                    SchoolID = teacher.SchoolID,
                    //LgaID = teacher.LgaID,
                    Gender = teacher.Gender,
                    PhoneNumber = teacher.PhoneNumber,
                    TeacherSubjects = new List<ReturnTeacherSubject>()
                };
                foreach (var subject in teacher.TeacherSubjects)
                {
                    var RTS = new ReturnTeacherSubject
                    {
                        TeacherSubjectID = subject.TeacherSubjectID,
                        Title = subject.ClassSubject.Subject.Title,
                        Range = subject.ClassSubject.Subject.Range, // an update would be to get a hashset for the subject Ids so we can have
                        //listings like: Junior English, Junior maths, for secondary school teachers 
                        Class = subject.ClassSubject.ClassArm.Class,
                        Arm = subject.ClassSubject.ClassArm.Arm
                    };
                    TS.TeacherSubjects.Add(RTS);

                }
                var t = teacher.TeacherClasses.SingleOrDefault();
                TS.Class = t != null ? (int)t.ClassArm.Class : -1;
                TS.Arm = t != null ? (int)t.ClassArm.Arm : -1;

                selection.Add(TS);
            }

            return Ok(selection);
        }

        [HttpPost]
        [Route("getStudents")]
        public async Task<IActionResult> GetStudents([FromBody] ClassId i)
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

                var students = await _studentManager.Users
              .Where(x => x.SchoolID == id && x.HasGraduated == false)
              .Include(x => x.ClassArm)
              .Select(x => new ReturnStudent
              {
                  Id = x.Id,
                  FirstName = x.FirstName,
                  LastName = x.LastName,
                  ClassArmID = x.ClassArm.ClassArmID,
                  Class = x.ClassArm.Class,
                  Arm = x.ClassArm.Arm,

                  //LgaID = x.LgaID,
                  Gender = (UserGender)x.Gender,
                  RegNo = x.RegNo,
              })
              .ToListAsync();

                return Ok(students);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpDelete]
        [Route("deleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] String studentId)
        {
            var student = await _studentManager.FindByIdAsync(studentId);
            IdentityResult result = await _studentManager.RemoveFromRoleAsync(student, "Student");
            IdentityResult res = await _studentManager.DeleteAsync(student);/////remember to test
            await _context.SaveChangesAsync();
            return Ok("Successfully deleted");
        }

        [HttpPost]
        [Route("deleteTeacher")]
        public async Task<IActionResult> DeleteTeacher([FromBody] TID i)
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

            var teacher = await _teacherManager.FindByIdAsync(i.Id);

            if (teacher.SchoolID == id)
            {
                try
                {
                    IdentityResult result = await _teacherManager.RemoveFromRoleAsync(teacher, "Teacher");
                    IdentityResult res = await _teacherManager.DeleteAsync(teacher);/////remember to test
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("deleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] TID i)
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

            var student = await _studentManager.FindByIdAsync(i.Id);

            if (student.SchoolID == id)
            {
                try
                {
                    IdentityResult result = await _studentManager.RemoveFromRoleAsync(student, "Student");
                    IdentityResult res = await _studentManager.DeleteAsync(student);/////remember to test
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("assingSubject")]
        //
        public async Task<IActionResult> AssignSubject([FromBody] SubjectAssign model)
        {
            /* var teacherSubjects = _teacherManager.Users
             .Where(x => x.SchoolID == model.SchoolID)
             .Include(x => x.TeacherSubjects)
             .SelectMany(x => x.TeacherSubjects);

             foreach (var tsub in teacherSubjects)
             {
                 if (tsub.ClassSubjectID == model.ClassSubjectID)
                 {
                     var teacher = await _teacherManager.FindByIdAsync(tsub.TeacherID);
                     return BadRequest(new { Message = "Class subject is currently assigned to " + teacher.FirstName + " " + teacher.LastName });
                 }
             }*/

            foreach (var CSID in model.ClassSubjectIDs)
            {
                var teacherSubjects = await _context.TeacherSubjects
                .Where(x => x.ClassSubjectID == CSID && x.TeacherID == model.TeacherID)
                .AnyAsync();

                if (!teacherSubjects)
                {
                    var teacherSubject = new TeacherSubject
                    {
                        ClassSubjectID = CSID,
                        TeacherID = model.TeacherID
                    };
                    await _context.TeacherSubjects.AddAsync(teacherSubject);
                }
                else
                {
                    return BadRequest(new { Message = "Subject already asigned" });
                }

            }

            await _context.SaveChangesAsync();

            var returnSubjects = await _context.TeacherSubjects
            .Where(x => x.TeacherID == model.TeacherID)
            .Include(x => x.ClassSubject)
            .ThenInclude(x => x.ClassArm)
            .Include(x => x.ClassSubject)
            .ThenInclude(x => x.Subject)
            .ToListAsync();

            var selected = new List<ReturnTeacherSubject>();
            foreach (var returnSubject in returnSubjects)
            {
                var RTS = new ReturnTeacherSubject
                {
                    TeacherSubjectID = returnSubject.TeacherSubjectID,
                    Title = returnSubject.ClassSubject.Subject.Title,
                    Range = returnSubject.ClassSubject.Subject.Range, // an update would be to get a hashset for the subject Ids so we can have
                                                                      //listings like: Junior English, Junior maths, for secondary school teachers 
                    Class = returnSubject.ClassSubject.ClassArm.Class,
                    Arm = returnSubject.ClassSubject.ClassArm.Arm
                };
                selected.Add(RTS);
            }

            return Ok(selected);
        }

        [HttpGet]
        [Route("dissociateSubject")]
        //
        public async Task<IActionResult> DissociateSubject(int id)
        {
            var tsub = await _context.TeacherSubjects
            .Where(s => s.TeacherSubjectID == id)
            .SingleOrDefaultAsync();
            _context.TeacherSubjects.Remove(tsub);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Subject Succesfully dissociated from teacher" });

        }

        [HttpPost]
        [Route("studentCode")]
        //authorise for only admin
        public async Task<IActionResult> StudentCode([FromBody] StudentCode model)
        {
            Random random = new Random();
            string t = random.Next(9999, 100000).ToString();
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
            string t = random.Next(9999, 100000).ToString();
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
            if (role == "Proprietor")
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
        [Route("addPrincipal")]
        [Authorize(Roles = "Admin,Proprietor")]
        public async Task<IActionResult> AddPrincipal([FromBody] ReceivedPrincipalModel model)
        {

            var principal = new Principal
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                SchoolID = model.SchoolID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };

            PrincipalMethods pMethod = new PrincipalMethods();
            var result = await pMethod.Principal(principal, _context, _principalManager, "Principal");
            if (!result.Succeeded)
            {
                // await _context.SchoolRegCodes.AddAsync(new SchoolRegCode { Code = model.school.Code }); code is for non super admin
                await _context.SaveChangesAsync();
                return BadRequest(result);
            }
            else
            {
                return Ok();
            }

        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] ReceivedStudentModel model)
        {
            var id = 0;

            if (model.SchoolID != 0)
            {
                id = model.SchoolID;
            }
            else
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
            }
            var regCount = _context.Schools
              .Where(x => x.SchoolID == id)
             .Select(x => x.RegNoCount).Single();
            var append = _context.Schools
             .Where(x => x.SchoolID == id)
            .Select(x => x.Append).Single();

            string s = append + regCount.ToString().PadLeft(3, '0');

            Student student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                ClassArmID = model.ClassArmID,
                SchoolID = id,
                //LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                RegNo = s,
                UserName = s
            };
            try
            {

                var result = await _studentManager.CreateAsync(student, "password");
                if (result.Succeeded)
                {
                    await _studentManager.AddToRoleAsync(student, "Student");
                    var regC = _context.Schools
                   .Where(x => x.SchoolID == id)
                   .Single();
                    regC.RegNoCount = regC.RegNoCount + 1;
                    regC.StudentCount = regC.StudentCount + 1;
                    _context.Schools.Update(regC);

                    await _context.SaveChangesAsync();

                    var term = _context.Sessions
                        .Where(x => x.SchoolID == id && x.Current)
                        .Include(x => x.Terms)
                        .SelectMany(x => x.Terms)
                        .Where(x => x.Current)
                        .FirstOrDefault();

                    EMethod eMethod = new EMethod();
                    await eMethod.EnrollStudent(student, term, _context);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("EditStudent")]
        public async Task<IActionResult> EditStudent([FromBody] ReceivedStudentModel model)
        {
            //update this to check if prncipal or propreitress is from same school as student before being able to perform these operations

            var user = await _studentManager.FindByIdAsync(model.Id);

            user.Id = model.Id;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.ClassArmID = model.ClassArmID;
            //LgaID = model.LgaID;
            user.Gender = (UserGender)model.Gender;
            await _context.SaveChangesAsync();
            return Ok();
            Student student = new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                ClassArmID = model.ClassArmID,
                //LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                RegNo = model.RegNo,
                UserName = model.RegNo

            };
            try
            {

                var result = await _studentManager.UpdateAsync(student);
                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("AddTeacher")]
        public async Task<IActionResult> AddTeacher([FromBody] ReceivedTeacher model)
        {
            var id = 0;

            if (model.SchoolID != 0)
            {
                id = model.SchoolID;
            }
            else
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
            }

            var teacher = new Teacher
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SchoolID = model.SchoolID,
                //LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,

            };

            try
            {
                var result = await _teacherManager.CreateAsync(teacher, "password");
                if (result.Succeeded)
                {
                    await _teacherManager.AddToRoleAsync(teacher, "Teacher");
                    if (model.TeacherSubjects.Any())
                    {
                        foreach (var sub in model.TeacherSubjects)
                        {
                            var teacherSubject = new TeacherSubject
                            {
                                ClassSubjectID = sub,
                                TeacherID = teacher.Id
                            };
                            await _context.TeacherSubjects.AddAsync(teacherSubject);
                        }
                        await _context.SaveChangesAsync();
                    }

                    /*  if(model.TeacherQualifications != null)
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
                      }*/

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                var students = await _studentManager.Users
              .Where(x => x.SchoolID == id && x.HasGraduated == false && x.ClassArmID == i.ClassArmID)
              .Include(x => x.ClassArm)
              .Select(x => new ReturnStudent
              {
                  FirstName = x.FirstName,
                  LastName = x.LastName,

                  Class = x.ClassArm.Class,
                  Arm = x.ClassArm.Arm,

                  //LgaID = x.LgaID,
                  Gender = (UserGender)x.Gender,
                  RegNo = x.RegNo,
              })
              .ToListAsync();

                return Ok(students);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        [Route("findStudents")]
        public async Task<IActionResult> FindStudents([FromBody] FSID i)
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

                var students = await _studentManager.Users
              .Where(x => x.SchoolID == id && x.HasGraduated == false &&
               (x.FirstName.ToUpper().Contains(i.searchQuery.ToUpper()) || x.LastName.ToUpper().Contains(i.searchQuery.ToUpper()) || x.RegNo.ToUpper().Contains(i.searchQuery.ToUpper()) || x.MiddleName.ToUpper().Contains(i.searchQuery.ToUpper())))
              .Include(x => x.ClassArm)
              .Select(x => new ReturnStudent
              {
                  FirstName = x.FirstName,
                  LastName = x.LastName,

                  Class = x.ClassArm.Class,
                  Arm = x.ClassArm.Arm,

                  //LgaID = x.LgaID,
                  Gender = (UserGender)x.Gender,
                  RegNo = x.RegNo,
              })
              .ToListAsync();

                return Ok(students);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        [Route("findTeachers")]
        public async Task<IActionResult> FindTeachers([FromBody] FSID i)
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

                var teachers = _teacherManager.Users
               .Where(x => x.SchoolID == id &&
               (x.FirstName.ToUpper().Contains(i.searchQuery.ToUpper()) || x.LastName.ToUpper().Contains(i.searchQuery.ToUpper()) || x.MiddleName.ToUpper().Contains(i.searchQuery.ToUpper())))
               .Include(x => x.TeacherSubjects)
               .Select(x => new Teacher
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   Email = x.Email,
                   SchoolID = x.SchoolID,
                   //LgaID = x.LgaID,
                   Gender = x.Gender,
                   UserName = x.Email,
                   PhoneNumber = x.PhoneNumber,
                   TeacherSubjects = x.TeacherSubjects
               });

                return Ok(teachers);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        [Route("saveTestScheme")]
        public async Task<IActionResult> SaveTestScheme([FromBody] TestScheme Scheme)
        {
            var SchoolID = 0;

            if (Scheme.SchoolID != 0)
            {
                SchoolID = Scheme.SchoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                SchoolID = principal.SchoolID;

            }
            var ScoreScheme = await _context.ScoreSchemes
            .Where(x => x.SchoolID == SchoolID)
            .SingleOrDefaultAsync();

            if (ScoreScheme == null)
            {
                var NewScheme = new ScoreScheme
                {
                    Test1 = Scheme.Test1,
                    Test2 = Scheme.Test2,
                    Test3 = Scheme.Test3,
                    Exam = Scheme.Exam,
                    SchoolID = SchoolID
                };

                await _context.ScoreSchemes.AddAsync(NewScheme);
                await _context.SaveChangesAsync();

                return Ok();

            }
            else
            {
                ScoreScheme.Test1 = Scheme.Test1;
                ScoreScheme.Test2 = Scheme.Test2;
                ScoreScheme.Test3 = Scheme.Test3;
                ScoreScheme.Exam = Scheme.Exam;
                ScoreScheme.SchoolID = SchoolID;

                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost]
        [Route("saveGradeScheme")]
        public async Task<IActionResult> SaveGradeScheme([FromBody] GradeScheme Scheme)
        {
            var SchoolID = 0;

            if (Scheme.SchoolID != 0)
            {
                SchoolID = Scheme.SchoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                SchoolID = principal.SchoolID;

            }
            var ScoreScheme = await _context.ScoreSchemes
            .Where(x => x.SchoolID == SchoolID)
            .SingleOrDefaultAsync();

            if (ScoreScheme == null)
            {
                var NewScheme = new ScoreScheme
                {
                    MinA = Scheme.MinA,
                    MaxA = Scheme.MaxA,
                    MinB = Scheme.MinB,
                    MaxB = Scheme.MaxB,
                    MinC = Scheme.MinC,
                    MaxC = Scheme.MaxC,
                    MinD = Scheme.MinD,
                    MaxD = Scheme.MaxD,
                    MinE = Scheme.MinE,
                    MaxE = Scheme.MaxE,
                    MinP = Scheme.MinP,
                    MaxP = Scheme.MaxP,
                    MinF = Scheme.MinF,
                    MaxF = Scheme.MaxF,
                    SchoolID = SchoolID
                };

                await _context.ScoreSchemes.AddAsync(NewScheme);
                await _context.SaveChangesAsync();

                return Ok();

            }
            else
            {
                ScoreScheme.MinA = Scheme.MinA;
                ScoreScheme.MaxA = Scheme.MaxA;
                ScoreScheme.MinB = Scheme.MinB;
                ScoreScheme.MaxB = Scheme.MaxB;
                ScoreScheme.MinC = Scheme.MinC;
                ScoreScheme.MaxC = Scheme.MaxC;
                ScoreScheme.MinD = Scheme.MinD;
                ScoreScheme.MaxD = Scheme.MaxD;
                ScoreScheme.MinE = Scheme.MinE;
                ScoreScheme.MaxE = Scheme.MaxE;
                ScoreScheme.MinP = Scheme.MinP;
                ScoreScheme.MaxP = Scheme.MaxP;
                ScoreScheme.MinF = Scheme.MinF;
                ScoreScheme.MaxF = Scheme.MaxF;
                ScoreScheme.SchoolID = SchoolID;

                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpGet]
        [Route("getScoreScheme")]
        public async Task<IActionResult> GetScoreScheme(int SchoolID)
        {
            var ID = 0;

            if (SchoolID != 0)
            {
                ID = SchoolID;
            }
            else
            {
                var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(principalId);
                ID = principal.SchoolID;

            }
            var ScoreScheme = await _context.ScoreSchemes
            .Where(x => x.SchoolID == ID)
            .SingleOrDefaultAsync();

            var GradeScheme = new GradeScheme
            {
                MinA = ScoreScheme.MinA,
                MaxA = ScoreScheme.MaxA,
                MinB = ScoreScheme.MinB,
                MaxB = ScoreScheme.MaxB,
                MinC = ScoreScheme.MinC,
                MaxC = ScoreScheme.MaxC,
                MinD = ScoreScheme.MinD,
                MaxD = ScoreScheme.MaxD,
                MinE = ScoreScheme.MinE,
                MaxE = ScoreScheme.MaxE,
                MinP = ScoreScheme.MinP,
                MaxP = ScoreScheme.MaxP,
                MinF = ScoreScheme.MinF,
                MaxF = ScoreScheme.MaxF,
            };

            var TestScheme = new TestScheme
            {
                Test1 = ScoreScheme.Test1,
                Test2 = ScoreScheme.Test2,
                Test3 = ScoreScheme.Test3,
                Exam = ScoreScheme.Exam
            };

            var NewScoreScheme = new TestGradeScheme
            {
                Tests = TestScheme,
                Grades = GradeScheme
            };



            return Ok(NewScoreScheme);

        }

        [HttpPost]
        [Route("uploadLogo")]
        public async Task<IActionResult> UploadLogo([FromForm] IFormCollection data)
        {
            var ID = 0;

            /* if (SchoolID != 0)
             {
                 ID = SchoolID;
             }
             else
             {
                 var principalId = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                 var principal = await _principalManager.FindByIdAsync(principalId);
                 ID = principal.SchoolID;

             }*/

            var name = data["ID"];



            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Logos");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = name;
                    var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                    var fullPath = Path.Combine(pathToSave, name += extension);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        [Route("assignTeacherClass")]
        public async Task<IActionResult> assignTeacherClass(RequestTeacherClass teacherClass)
        {
            var existingEntry = await _context.TeacherClasses
            .Where(x => x.TeacherID == teacherClass.TeacherID)
            .Include(x => x.ClassArm)
            .SingleOrDefaultAsync();

            if (existingEntry == null)
            {
                var tc = new TeacherClass();
                tc.ClassArmID = teacherClass.ClassArmID;
                tc.TeacherID = teacherClass.TeacherID;

                await _context.TeacherClasses.AddAsync(tc);

                var teacher = await _teacherManager.FindByIdAsync(teacherClass.TeacherID);
                var result = await _teacherManager.AddToRoleAsync(teacher, "FormTeacher");
                await _context.SaveChangesAsync();

                var Entry = await _context.TeacherClasses
                .Where(x => x.TeacherID == teacherClass.TeacherID)
                .Include(x => x.ClassArm)
                .SingleOrDefaultAsync();

                return Ok(Entry.ClassArm);
            }
            else
            {
                existingEntry.ClassArmID = teacherClass.ClassArmID;
                existingEntry.TeacherID = teacherClass.TeacherID;
                await _context.SaveChangesAsync();

                var Entry = await _context.TeacherClasses
                .Where(x => x.TeacherID == teacherClass.TeacherID)
                .Include(x => x.ClassArm)
                .SingleOrDefaultAsync();
                return Ok(Entry.ClassArm);
            }


        }


    }

    public class ReturnStudent
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SchoolID { get; set; }
        public int ClassArmID { get; set; }
        public UserGender Gender { get; set; }
        public string RegNo { get; set; }
        public Class Class { get; set; }
        public Arms Arm { get; set; }
    }

    public class ReturnTeacher
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public int SchoolID { get; set; }
        public UserGender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int? Class { get; set; }
        public int? Arm { get; set; }

        public ICollection<ReturnTeacherSubject> TeacherSubjects { get; set; }
    }

    public class ReturnTeacherSubject
    {
        public int TeacherSubjectID { get; set; }
        public int ClassSubjectID { get; set; }
        public string Title { get; set; }
        public ClassRange Range { get; set; }
        public Class Class { get; set; }
        public Arms Arm { get; set; }
    }


}
