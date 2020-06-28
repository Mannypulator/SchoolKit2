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
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolKitContext _context;

        public SchoolController(SchoolKitContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(School model)
        {
            _context.Schools.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: School/Details/5
    }
}
