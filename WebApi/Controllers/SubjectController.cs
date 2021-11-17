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
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {
        private readonly SchoolKitContext _context;
        private readonly UserManager<Principal> _principalManager;
        private readonly UserManager<Student> _studentManager;

        public SubjectController(SchoolKitContext context, UserManager<Principal> principalManager, UserManager<Student> studentManager)
        {
            _context = context;
            _principalManager = principalManager;
            _studentManager = studentManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] SubjectModel subject)
        {
            var sub = new Subject
            {
                Title = subject.Title,
                Range = (ClassRange)subject.Range,
                SchoolSpecific = subject.SchoolSpecific
            };
            await _context.Subjects.AddAsync(sub);
            await _context.SaveChangesAsync();

            var arms = _context.ClassArms;

            if (subject.Range == ClassRange.All)
            {
                foreach (var arm in arms)
                {
                    var classSub = new ClassSubject
                    {
                        ClassArmID = arm.ClassArmID,
                        SubjectID = sub.SubjectID,

                    };
                    await _context.ClassSubjects.AddAsync(classSub);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                foreach (var arm in arms)
                {
                    if (Enum.GetName(typeof(Class), arm.Class).Contains(Enum.GetName(typeof(ClassRange), subject.Range)))
                    //get string representation of class enum and compare it with string representation pf class range enum
                    {
                        var classSub = new ClassSubject
                        {
                            ClassArmID = arm.ClassArmID,
                            SubjectID = sub.SubjectID,
                        };
                        await _context.ClassSubjects.AddAsync(classSub);
                    }
                    await _context.SaveChangesAsync();
                }
            }



            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getPrimary")]
        public async Task<IList<Subject>> GetPrimarySubjects()
        {
            var subjects = await _context.Subjects
            .Where(x => x.Range == ClassRange.Nursery || x.Range == ClassRange.Primary || x.Range == ClassRange.All)
            .ToListAsync();
            return subjects;
        }

        [HttpGet]
        [Route("getSecondary")]
        public async Task<IList<Subject>> GetSecondarySubjects()
        {
            var subjects = await _context.Subjects
           .Where(x => x.Range > ClassRange.Primary)
           .ToListAsync();
            return subjects;
        }

        [HttpPost]
        [Route("getClassSubjects")]
        public async Task<IActionResult> GetClassSubjects(ClassId i)
        {
            var id = 0;

            if (i.schoolID != 0)
            {
                id = i.schoolID;
            }
            else
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
            }
            var school = await _context.Schools
            .Where(x => x.SchoolID == id)
            .FirstOrDefaultAsync();

            var dropped = await _context.SSDrops
            .Where(x => x.SchoolID == id)
            .Select(x => x.SubjectID)
            .ToListAsync();

            var classArms = _studentManager.Users
           .Where(x => x.SchoolID == id && x.HasGraduated == false)
           .Select(r => r.ClassArmID).ToHashSet();

            if (school.Type == SchoolType.Primary)
            {

                var subjects = await _context.Subjects
                .Where(e => dropped.All(r => r != e.SubjectID))
                 .Where(x => x.Range == ClassRange.Nursery || x.Range == ClassRange.Primary || x.Range == ClassRange.All)
                 .Include(z => z.ClassSubject)
                 .ThenInclude(n => n.ClassArm)

                .ToListAsync();

                var returnSubject = new List<ReturnSubject>();

                foreach (var subject in subjects)
                {
                    foreach (var classSubject in subject.ClassSubject)
                    {
                        if (classArms.Any(x => x == classSubject.ClassArmID))
                        {
                            var selected = new ReturnSubject
                            {
                                CSID = classSubject.ClassSubjectID,
                                Title = subject.Title,
                                CN = Enum.GetName(typeof(Class), classSubject.ClassArm.Class),
                                CAN = Enum.GetName(typeof(Arms), classSubject.ClassArm.Arm),
                            };
                            returnSubject.Add(selected);
                        }

                    }
                }

                return Ok(returnSubject);
            }
            else
            {
                var subjects = await _context.Subjects
                .Where(e => dropped.All(r => r != e.SubjectID))
            .Where(x => x.Range > ClassRange.Primary)
           .Include(z => z.ClassSubject)
             .ThenInclude(n => n.ClassArm)
            .ToListAsync();

                var returnSubject = new List<ReturnSubject>();

                foreach (var subject in subjects)
                {
                    foreach (var classSubject in subject.ClassSubject)
                    {
                        if (classArms.Any(x => x == classSubject.ClassArmID))
                        {
                            var selected = new ReturnSubject
                            {
                                CSID = classSubject.ClassSubjectID,
                                Title = subject.Title,
                                CN = Enum.GetName(typeof(Class), classSubject.ClassArm.Class),
                                CAN = Enum.GetName(typeof(Arms), classSubject.ClassArm.Arm),
                            };
                            returnSubject.Add(selected);
                        }

                    }
                }


                return Ok(returnSubject);
            }

        }

        [HttpPost]
        [Route("getSubjects")]
        public async Task<IActionResult> GetSubjects(ClassId i)
        {
            var id = 0;

            if (i.schoolID != 0)
            {
                id = i.schoolID;
            }
            else
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
            }
            var school = await _context.Schools
            .Where(x => x.SchoolID == id)
            .FirstOrDefaultAsync();

            var dropped = await _context.SSDrops
            .Where(x => x.SchoolID == id)
            .Select(x => x.SubjectID)
            .ToListAsync();

            var classArms = _studentManager.Users
           .Where(x => x.SchoolID == id && x.HasGraduated == false)
           .Select(r => r.ClassArmID).ToHashSet();

            if (school.Type == SchoolType.Primary)
            {

                var subjects = _context.Subjects
                .Where(e => dropped.All(r => r != e.SubjectID))
                 .Where(x => x.Range == ClassRange.Nursery || x.Range == ClassRange.Primary || x.Range == ClassRange.All)
                 .ToHashSet();

                return Ok(subjects);
            }
            else
            {
                var subjects = _context.Subjects
                .Where(e => dropped.All(r => r != e.SubjectID))
            .Where(x => x.Range > ClassRange.Primary)
          .ToHashSet();

                return Ok(subjects);
            }
        }

        [HttpPost]
        [Route("getSubjectClasses")]
        public async Task<IActionResult> GetSubjectsClasses(SID i)
        {
            var id = 0;

            if (i.schoolID != 0)
            {
                id = i.schoolID;
            }
            else
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var principal = await _principalManager.FindByIdAsync(userID);
                id = principal.SchoolID;
            }

            var classArms = _studentManager.Users
            .Where(x => x.SchoolID == id)
            .Select(x=> x.ClassArmID)
            .ToHashSet();

            var classSubject = await _context.ClassSubjects
            .Where(x=> x.SubjectID == i.SubjectID)
            .Include(x => x.ClassArm)
            .ToListAsync();

            var selected = classSubject
            .Where(x=> classArms.Any(y => x.ClassArmID == y))
            .Select(x => new ReturnClassSubject{
                ClassSubjectID = x.ClassSubjectID,
                Class = x.ClassArm.Class,
                Arm = x.ClassArm.Arm,
                Selected = false
            });

            return Ok(selected);
            
        }

    }
    public class ReturnSubject
    {
        public int CSID { get; set; } //classSubjectID
        public string Title { get; set; } //
        public string CN { get; set; } //ClassName
        public string CAN { get; set; } //ClassArmName
    }

    public class ReturnClassSubject{
        public int ClassSubjectID { get; set; }
        public Class Class { get; set; }
        public Arms Arm { get; set; }
        public bool Selected { get; set; }
               
    }

}