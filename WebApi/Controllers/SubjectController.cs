using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public SubjectController(SchoolKitContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]SubjectModel subject)
        {
            var sub = new Subject
            {
                Title = subject.Title,
                Range = (ClassRange)Enum.Parse(typeof(ClassRange), subject.Range),
                SchoolSpecific = subject.SchoolSpecific
            };
            await _context.Subjects.AddAsync(sub);
            await _context.SaveChangesAsync();

            var arms = _context.ClassArms;

            if (subject.Range == "All")
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
                    if (Enum.GetName(typeof(Class), arm.Class).Contains(subject.Range))
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
    }
}
