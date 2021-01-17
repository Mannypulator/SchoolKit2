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
    public class FinanceController : ControllerBase
    {
        private readonly SchoolKitContext _context;

        public FinanceController(SchoolKitContext context)
        {
            _context = context;
        }

        // GET: Finance
        public async Task<IActionResult> Index()
        {
            var schoolKitContext = _context.Teachers.Include(t => t.LGA).Include(t => t.School);
            return View(await schoolKitContext.ToListAsync());
        }

        // GET: Finance/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.LGA)
                .Include(t => t.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Finance/Create
        public IActionResult Create()
        {
            ViewData["LgaID"] = new SelectList(_context.LGAs, "LgaID", "LgaID");
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SchoolID");
            return View();
        }

        // POST: Finance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolID,FirstName,LastName,MiddleName,Address,Gender,LgaID,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LgaID"] = new SelectList(_context.LGAs, "LgaID", "LgaID", teacher.LgaID);
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SchoolID", teacher.SchoolID);
            return View(teacher);
        }

        // GET: Finance/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["LgaID"] = new SelectList(_context.LGAs, "LgaID", "LgaID", teacher.LgaID);
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SchoolID", teacher.SchoolID);
            return View(teacher);
        }

        // POST: Finance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SchoolID,FirstName,LastName,MiddleName,Address,Gender,LgaID,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LgaID"] = new SelectList(_context.LGAs, "LgaID", "LgaID", teacher.LgaID);
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SchoolID", teacher.SchoolID);
            return View(teacher);
        }

        // GET: Finance/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.LGA)
                .Include(t => t.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Finance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(string id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
